﻿namespace DoorsAndLevelsRef
{
    public interface IInputOutput
    {
        string ReadInput();
        char ReadKey();
        void WriteOutput(string dataToOutput);
        void printArray(int[] array);
    }
}