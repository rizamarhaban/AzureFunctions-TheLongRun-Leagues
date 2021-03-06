﻿using System;
using System.Collections;
using System.Collections.Generic;

using CQRSAzure.EventSourcing;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using TheLongRun.Common.Events.Query;

namespace TheLongRun.Common.Events.Query.Projections
{

    /// <summary>
    /// A projection to get the set of defined outputs to which a query result should be returned
    /// </summary>
    /// <remarks>
    /// This projection does not care about the status of the query
    /// </remarks>
    public class Query_Outputs_Projection
        : CQRSAzure.EventSourcing.ProjectionBaseUntyped, 
        CQRSAzure.EventSourcing.IHandleEvent<OutputLocationSet>,
        IProjectionUntyped
    {

        #region Private members
        private ILogger log = null;
        private Dictionary<string, QueryLogRecord.QueryReturnTarget> targets = new Dictionary<string, QueryLogRecord.QueryReturnTarget>();
        #endregion

        public Dictionary<string , QueryLogRecord.QueryReturnTarget > Targets
        {
            get
            {
                return targets;
            }
        }

        /// <summary>
        /// There is no value in storing snapshots for query summaries
        /// </summary>
        public override bool SupportsSnapshots => false;

        public override void HandleEventJSon(string eventFullName, JObject eventToHandle)
        {
            #region Logging
            if (null != log)
            {
                log.LogDebug($"HandleEventJSon({eventFullName})",
                    nameof(Query_Summary_Projection));
            }
            #endregion

            if (eventFullName == typeof(OutputLocationSet ).FullName)
            {
                HandleEvent<OutputLocationSet>(eventToHandle.ToObject<OutputLocationSet>());
            }


        }

        public override bool HandlesEventType(Type eventType)
        {
            return HandlesEventTypeByName(eventType.FullName);
        }

        public override bool HandlesEventTypeByName(string eventTypeFullName)
        {

            #region Logging
            if (null != log)
            {
                log.LogDebug($"HandlesEventTypeByName({eventTypeFullName})",
                    nameof(Query_Outputs_Projection));
            }
            #endregion


            if (eventTypeFullName == typeof(OutputLocationSet ).FullName)
            {
                return true;
            }


            return false;
        }

        public override void HandleEvent<TEvent>(TEvent eventToHandle)
        {

            #region Logging
            if (null != log)
            {
                log.LogDebug($"HandleEvent<{ typeof(TEvent).FullName  }>())",
                    nameof(Query_Outputs_Projection ));
            }
            #endregion

            if (eventToHandle.GetType() == typeof(OutputLocationSet ))
            {
                HandleEvent(eventToHandle as OutputLocationSet);
            }
        }

        public void HandleEvent(OutputLocationSet  eventHandled)
        {
            #region Logging
            if (null != log)
            {
                log.LogDebug($"HandleEvent( OutputLocationSet )",
                    nameof(Query_Outputs_Projection ));
            }
            #endregion

            if (null != eventHandled)
            {
                // add the location to the internal lists
                #region Logging
                if (null != log)
                {
                    log.LogDebug($"Query output added  {eventHandled.Location  } Type: { eventHandled.TargetType }  ",
                        nameof(Query_Outputs_Projection));
                }
                #endregion
                if (!string.IsNullOrWhiteSpace(eventHandled.Location))
                {
                    if (!this.targets.ContainsKey(eventHandled.Location))
                    {
                        targets.Add(eventHandled.Location, eventHandled.TargetType);
                    }
                }
            }
            else
            {
                #region Logging
                if (null != log)
                {
                    log.LogWarning($"HandleEvent( QueryCreated ) - parameter was null",
                        nameof(Query_Outputs_Projection));
                }
                #endregion
            }
        }

        public Query_Outputs_Projection(ILogger logIn = null)
        {
            if (null != logIn)
            {
                log = logIn;
            }
        }
    }
}
