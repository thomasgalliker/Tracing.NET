﻿using System;

namespace Tracing
{
    /// <summary>
    /// ActionTracer can be used to intercept trace writes.
    /// The defined forwardingAction is called whenever a trace is written to the ITracer interface.
    /// </summary>
    public class ActionTracer : TracerBase
    {
        private readonly Action<string, TraceEntry> forwardingAction;
        private readonly string name;

        public ActionTracer(object target, Action<string, TraceEntry> forwardingAction)
            : this(target.GetType().Name, forwardingAction)
        {
        }

        public ActionTracer(string name, Action<string, TraceEntry> forwardingAction)
        {
            Guard.ArgumentNotNullOrEmpty(nameof(name), name);
            Guard.ArgumentNotNull(nameof(forwardingAction), forwardingAction);

            this.name = name;
            this.forwardingAction = forwardingAction;
        }

        protected override void WriteCore(TraceEntry entry)
        {
            this.forwardingAction(this.name, entry);
        }

        public override bool IsCategoryEnabled(Category category)
        {
            return true;
        }
    }
}
