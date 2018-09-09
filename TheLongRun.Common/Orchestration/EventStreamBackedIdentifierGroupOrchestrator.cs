﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLongRun.Common.Orchestration
{
    /// <summary>
    /// A base class for any IDENTIFIER GROUP orchestrator functions
    /// </summary>
    /// <remarks>
    /// This is a business meaninful collection of entities that have been selected
    /// by either a classifier or some rule over a projection
    /// </remarks>
    public abstract class EventStreamBackedIdentifierGroupOrchestrator
        : EventStreamBackedOrchestratorBase,
        IEventStreamBackedOrchestrator
    {
        public abstract bool IsComplete { get; }

        /// <summary>
        /// Orchestrator classification type is a GROUP
        /// </summary>
        public string ClassificationTypeName
        {
            get
            {
                return @"GROUP";
            }
        }


        public abstract string ClassificationInstanceName { get; }

        private readonly Guid _uniqueIdentifier;
        public Guid UniqueIdentifier
        {
            get
            {
                return _uniqueIdentifier;
            }
        }
        public abstract IEventStreamBackedOrchestratorContext Context { get; set; }

        /// <summary>
        /// The identity by which any called orchestrations can call back with the 
        /// results (a return address style identity)
        /// </summary>
        public OrchestrationCallbackIdentity CallbackIdentity
        {
            get
            {
                return OrchestrationCallbackIdentity.Create(
                    OrchestrationCallbackIdentity.OrchestrationClassifications.IdentifierGroup ,
                    ClassificationInstanceName,
                    UniqueIdentifier);
            }
        }

        public abstract void RunNextStep();

        protected internal EventStreamBackedIdentifierGroupOrchestrator(Guid uniqueIdentifier)
        {
            if (uniqueIdentifier.Equals(Guid.Empty))
            {
                _uniqueIdentifier = Guid.NewGuid();
            }
            else
            {
                _uniqueIdentifier = uniqueIdentifier;
            }
        }
    }
}