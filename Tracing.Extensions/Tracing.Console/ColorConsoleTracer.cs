using System;
using System.Collections.Generic;

namespace Tracing.Console
{
    public class ColorConsoleTracer : ConsoleTracer
    {
        private readonly Dictionary<Category, ConsoleColorCombination> consoleColorMappings = new Dictionary<Category, ConsoleColorCombination>
        {
            {
                Category.Debug, new ConsoleColorCombination
                {
                    ForegroundColor = ConsoleColor.DarkGray,
                    BackgroundColor = ConsoleColor.Black
                }
            },
            {
                Category.Information, new ConsoleColorCombination
                {
                    ForegroundColor = ConsoleColor.Gray,
                    BackgroundColor = ConsoleColor.Black
                }
            },
            {
                Category.Warning, new ConsoleColorCombination
                {
                    ForegroundColor = ConsoleColor.Yellow,
                    BackgroundColor = ConsoleColor.DarkYellow
                }
            },
            {
                Category.Error, new ConsoleColorCombination
                {
                    ForegroundColor = ConsoleColor.Red,
                    BackgroundColor = ConsoleColor.Black
                }
            },
            {
                Category.Fatal, new ConsoleColorCombination
                {
                    ForegroundColor = ConsoleColor.White,
                    BackgroundColor = ConsoleColor.Red
                }
            }
        };

        private readonly ConsoleColorCombination originalColorCombination;

        public ColorConsoleTracer(string name) : base(name)
        {
            this.originalColorCombination = new ConsoleColorCombination { ForegroundColor = System.Console.ForegroundColor, BackgroundColor = System.Console.BackgroundColor };
        }

        private ConsoleColorCombination GetEventColor(Category category, ConsoleColorCombination defaultColor)
        {
            if (!this.consoleColorMappings.ContainsKey(category))
            {
                return defaultColor;
            }

            return this.consoleColorMappings[category];
        }

        protected override void WriteCore(TraceEntry entry)
        {
            var categoryColorCombination = this.GetEventColor(entry.Category, this.originalColorCombination);

            System.Console.ForegroundColor = categoryColorCombination.ForegroundColor;
            System.Console.BackgroundColor = categoryColorCombination.BackgroundColor;

            base.WriteCore(entry);

            System.Console.ForegroundColor = this.originalColorCombination.ForegroundColor;
            System.Console.BackgroundColor = this.originalColorCombination.BackgroundColor;
        }

        private struct ConsoleColorCombination
        {
            public ConsoleColor ForegroundColor { get; set; }

            public ConsoleColor BackgroundColor { get; set; }
        }
    }
}