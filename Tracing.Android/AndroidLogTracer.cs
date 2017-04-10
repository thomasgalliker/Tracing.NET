using System;

using Android.Util;

using Tracing.Extensions;

namespace Tracing
{
    public class AndroidLogTracer : TracerBase
    {
        private readonly string name;

        public AndroidLogTracer(string name)
        {
            Guard.ArgumentNotNullOrEmpty(nameof(name), name);

            this.name = name;
        }

        protected override void WriteCore(TraceEntry entry)
        {
            var logPriority = ConvertCategoryToLogPriority(entry.Category);

            var traceString = entry.ToTraceString(this.name);
            Log.WriteLine(logPriority, this.name, traceString);
        }

        public override bool IsCategoryEnabled(Category category)
        {
            return true;
        }

        private static LogPriority ConvertCategoryToLogPriority(Category category)
        {
            LogPriority level;

            switch (category)
            {
                case Category.Fatal:
                    level = LogPriority.Error;
                    break;

                case Category.Error:
                    level = LogPriority.Error;
                    break;

                case Category.Information:
                    level = LogPriority.Info;
                    break;

                case Category.Warning:
                    level = LogPriority.Warn;
                    break;

                case Category.Debug:
                    level = LogPriority.Debug;
                    break;

                default:
                    throw new InvalidOperationException($"ConvertCategoryToLogPriority could not map Category {category} to LogPriority enum.");
            }

            return level;
        }
    }
}