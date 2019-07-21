﻿using System;
using System.Collections.Generic;

namespace CreateDoorsAndLevels
{
    class Game
    {
        private readonly Interfaces.IPhraseProvider phraseProvider;
        private readonly Interfaces.IInputOutputDevice inputOutputDevice;
        private readonly Interfaces.IDoorsNumbersGenerator doorsNumbersGenerator;
        private readonly Interfaces.ISettingsProvider settingsProvider;

        private readonly GameSettings gameSettings;

        List<int> numbers;
        List<int> selectedNumbers;

        enum Operation
        {
            NextLevel,
            PrevLevel,
            Print
        }

        public Game(
            Interfaces.IPhraseProvider phraseProvider, 
            Interfaces.IInputOutputDevice inputOutputDevice, 
            Interfaces.IDoorsNumbersGenerator doorsNumbersGenerator,
            Interfaces.ISettingsProvider settingsProvider
            )
        {
            this.phraseProvider = phraseProvider;
            this.inputOutputDevice = inputOutputDevice;
            this.doorsNumbersGenerator = doorsNumbersGenerator;
            this.settingsProvider = settingsProvider;

            this.gameSettings = this.settingsProvider.GetGameSettings();
        }

        public void Run()
        {
            // this.numbers = new List<int>() { 2, 4, 3, 1, 0 }; // simple test
            this.numbers = doorsNumbersGenerator.generateDoorsNumbers(gameSettings.DoorsAmount);
            this.selectedNumbers = new List<int>();

            inputOutputDevice.WriteOutput(phraseProvider.GetPhrase("Welcome"));

            // inputOutputDevice.WriteOutput($"We have numbers: 2, 4, 3, 1, 0");
            inputOutputDevice.WriteOutput(
                StringFromNumbersArr(intro: phraseProvider.GetPhrase("YouHave"), operation: Operation.Print));

            do
            {
                this.selectedNumbers.Add(EnterTheNumber()); // Select one of the current level numbers or type 'exit'

                if (this.selectedNumbers[this.selectedNumbers.Count - 1] == -1) break; // if -1 then break the circle - EXIT

                if (this.selectedNumbers[this.selectedNumbers.Count - 1] != this.gameSettings.ExitDoorNumber)
                {
                    // Create "next level" numbers where numbers values calculate using formula: [number on previous level] * [choosed number on previous level].
                    // "We select number 2 and go to next level: 4 8 6 2 0 (2x2 4x2 3x2 1x2 0x2)"
                    inputOutputDevice.WriteOutput(
                        StringFromNumbersArr(
                            intro: $"{phraseProvider.GetPhrase("YouSelected")}{this.selectedNumbers[this.selectedNumbers.Count - 1]}{phraseProvider.GetPhrase("GoNext")}", 
                            operation: Operation.NextLevel));
                }
                else if (this.selectedNumbers.Count - 1 > 0) // selectedNumber == 0, level > 0
                {
                    this.selectedNumbers.RemoveAt(this.selectedNumbers.Count - 1); // remove 0 from selectedNumbers. Now top is prev.number
                                                                                   // "We select number 0 and go to previous level: 4 8 6 2 0"
                    inputOutputDevice.WriteOutput(
                        StringFromNumbersArr(intro: $"{phraseProvider.GetPhrase("YouSelected")}{gameSettings.ExitDoorNumber}{phraseProvider.GetPhrase("GoPrev")}", operation: Operation.PrevLevel));
                    this.selectedNumbers.RemoveAt(this.selectedNumbers.Count - 1); // remove prev.number from selectedNumbers. Now top is prev2.number
                }
            } while (!(this.selectedNumbers.Count - 1 == 0 && this.selectedNumbers[0] == this.gameSettings.ExitDoorNumber));
            inputOutputDevice.WriteOutput(phraseProvider.GetPhrase("Bye"));
        }

        private int EnterTheNumber()
        {
            int result = -1;

            do
            {
                if (this.selectedNumbers.Count <= this.gameSettings.LevelLimit)
                {
                    inputOutputDevice.WriteSomeOutput($"{phraseProvider.GetPhrase("Select")}{gameSettings.ExitCode}{phraseProvider.GetPhrase("AfterExitCode")}");
                }
                else
                {
                    inputOutputDevice.WriteSomeOutput($"{phraseProvider.GetPhrase("SelectExitDoor")}{gameSettings.ExitDoorNumber}{phraseProvider.GetPhrase("ToContinue")}{gameSettings.ExitCode}{phraseProvider.GetPhrase("AfterExitCode")}");
                }

                try
                {
                    string s = inputOutputDevice.ReadInput();

                    if (String.IsNullOrEmpty(s))
                    {
                        inputOutputDevice.WriteOutput(phraseProvider.GetPhrase("EmptyString"));
                        continue;
                    }

                    if (s.ToLowerInvariant().Equals(gameSettings.ExitCode.ToLowerInvariant())) break; // if 'exit' then break the circle. result will be -1

                    int n = Int32.Parse(s);

                    if (!this.numbers.Contains(n))
                    {
                        inputOutputDevice.WriteOutput(phraseProvider.GetPhrase("WrongNumber"));
                        continue;
                    }

                    if (this.selectedNumbers.Count > this.gameSettings.LevelLimit && n != this.gameSettings.ExitDoorNumber)
                    {
                        inputOutputDevice.WriteOutput($"{phraseProvider.GetPhrase("NotExitDoor")}{gameSettings.ExitDoorNumber}");
                        continue;
                    }

                    result = n;
                }
                catch (OverflowException)
                {
                    inputOutputDevice.WriteOutput(phraseProvider.GetPhrase("Overflow"));
                    continue;
                }
                catch
                {
                    inputOutputDevice.WriteOutput(phraseProvider.GetPhrase("InputError"));
                    continue;
                }
            } while (result == -1);

            return result;
        }

        private string StringFromNumbersArr(string intro, Operation operation)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(intro);
            for (int i = 0; i < numbers.Count; i++)
            {
                switch (operation)
                {
                    case Operation.NextLevel:
                        sb.Append(numbers[i] *= this.selectedNumbers[this.selectedNumbers.Count - 1]);
                        break;
                    case Operation.PrevLevel:
                        sb.Append(numbers[i] /= this.selectedNumbers[this.selectedNumbers.Count - 1]);
                        break;
                    case Operation.Print:
                        sb.Append(numbers[i]);
                        break;
                    default:
                        break;
                }
                sb.Append(i < numbers.Count - 1 ? " " : String.Empty); // add space between them
            }

            if (operation == Operation.NextLevel) // info about next level
            {
                sb.Append(" (");
                for (int i = 0; i < numbers.Count; i++)
                {
                    sb.Append($"{numbers[i] / this.selectedNumbers[this.selectedNumbers.Count - 1]}x{this.selectedNumbers[this.selectedNumbers.Count - 1]}");
                    sb.Append(i < numbers.Count - 1 ? " " : String.Empty); // add space between them
                }
                sb.Append(")");
            }

            return sb.ToString();
        }
    }
}
