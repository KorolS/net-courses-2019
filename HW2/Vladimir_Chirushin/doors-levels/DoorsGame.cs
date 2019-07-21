﻿using System;
using System.Collections.Generic;

namespace doors_levels
{
    public class DoorsGame
    {
        private const Int32 MAX_DOORS = 5;

        private Int32[] doors;
        private Stack<Int32> levelsStack = new Stack<Int32>();

        private readonly IInputOutputDevice inputOutputDevice;
        private readonly IDoorsGenerator doorsGenerator;

        public DoorsGame(IInputOutputDevice inputOutputDevice, IDoorsGenerator doorsGenerator)
        {
            this.inputOutputDevice = inputOutputDevice;
            this.doorsGenerator = doorsGenerator;

            InitiateDoors();
        }


        private void InitiateDoors()
        {
            doors = doorsGenerator.GetDoors(MAX_DOORS);
        }


        private String ShowLevel()
        {
            String levelString = "";
            for (Int32 i = 0; i < doors.Length; i++)
            {
                levelString = levelString + doors[i].ToString() + " ";
            }
            return levelString;
        }

        private void ExecuteTheDoor(Int32 currentDoor)
        {
            if (currentDoor == 0)
            {
                if (levelsStack.Count > 0)
                {
                    Int32 lastDoor = levelsStack.Pop();
                    for (Int32 i = 0; i < doors.Length; i++)
                    {                                   
                        doors[i] /= lastDoor;
                    }
                    inputOutputDevice.WriteOutput("We select number 0 and go to previous level: ");
                    inputOutputDevice.WriteOutput(ShowLevel());
                }
                else
                {
                    inputOutputDevice.WriteOutput("It's first level. Cant get higher.");
                }
            }
            else //if door != 0
            {
                levelsStack.Push(currentDoor);
                for (Int32 i = 0; i < doors.Length; i++)
                {
                    try
                    {
                        checked
                        {
                            doors[i] *= currentDoor;
                        }
                    }
                    catch (OverflowException)
                    {
                        inputOutputDevice.WriteOutput("You get too far! Droped to first level");
                        InitiateDoors();
                        levelsStack.Clear();
                        ShowDoors();
                        return;
                    }
                }
                inputOutputDevice.WriteOutput($"We select number { currentDoor } and go to next level: ");
                inputOutputDevice.WriteOutput(ShowLevel());
            }
        }

        public void EnterTheDoor(Int32 doorToEnter)
        {
            Boolean doorExist = false;
            for (Int32 i = 0; i < doors.Length; i++)
            {
                if (doorToEnter == doors[i])
                {
                    doorExist = true;
                    break;
                }
            }
            if (doorExist)
            {
                ExecuteTheDoor(doorToEnter);
            }
            else
            {
                inputOutputDevice.WriteOutput("Door doesn't exist");
            }

        }

        public void ShowDoors()
        {
            String outputMessage = "We have numbers: ";
            
            for (Int32 i = 0; i < doors.Length; i++)
            {
                outputMessage = outputMessage + doors[i].ToString() + " ";
            }
            inputOutputDevice.WriteOutput(outputMessage);
        }

        public void Run()
        {
            ShowDoors();
            while (true)
            {
                try
                {
                    int door = Convert.ToInt32(inputOutputDevice.ReadInput());
                    EnterTheDoor(door);
                }
                catch
                {
                    inputOutputDevice.WriteOutput("Please enter a number.");
                }
            }
        }
    }
}