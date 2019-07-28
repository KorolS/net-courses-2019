﻿//-----------------------------------------------------------------------
// <copyright file="Board.cs" company="EPAM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace The_draw_game.Components
{
    using System;
    using DataTypes;
    using Interfaces;

    /// <summary>
    /// IBoard component
    /// </summary>
    public class Board : IBoard
    {
        /// <summary>
        /// IInputOutput field
        /// </summary>
        private readonly IInputOutput io;

        /// <summary>
        /// borderline field
        /// </summary>
        private Rectangle borderline;

        /// <summary>
        /// Board stile
        /// </summary>
        private string boardStile;

        /// <summary>
        /// Board width
        /// </summary>
        private int width;

        /// <summary>
        /// Board height
        /// </summary>
        private int height;

        /// <summary>
        /// Initializes a new instance of the <see cref="Board" /> class
        /// </summary>
        /// <param name="io">Input/Output component</param>
        /// <param name="width">Board width</param>
        /// <param name="height">Board height</param>
        /// <param name="boardStile">Board stile provider component</param>
        public Board(IInputOutput io, int width, int height, string boardStile)
        {
            this.io = io;

            this.Width = width;
            this.Height = height;
            this.boardStile = boardStile;
        }

        /// <summary>
        /// Gets or sets board width
        /// </summary>
        public int Width
        {
            get
            {
                return this.width;
            }

            set
            {
                if (value < 30)
                {
                    throw new Exception("Window width is very low");
                }

                this.width = value;
            }
        }

        /// <summary>
        /// Gets or sets board height
        /// </summary>
        public int Height
        {
            get
            {
                return this.height;
            }

            set
            {
                if (value < 15)
                {
                    throw new Exception("Window height is very low");
                }

                this.height = value;
            }
        }

        /// <summary>
        ///  Draw figure
        /// </summary>
        /// <param name="board">Board component</param>
        public void Draw(IBoard board)
        {
            this.io.SetWindowSize(board.Width + 1, board.Height + 10);
            
            if (this.borderline == null)
            {
                this.borderline = new Rectangle(
                    new Point(0, 0),
                    new Point(board.Width, board.Height));
            }

            foreach (var point in this.borderline.Body)
            {
                this.io.Print(point, this.boardStile);
            }
        }
    }
}
