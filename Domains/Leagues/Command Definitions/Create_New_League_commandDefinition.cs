//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using CQRSAzure.CommandDefinition;
using CQRSAzure.EventSourcing;

/// <remarks>
/// Each league will have an unique name
/// </remarks>
namespace Leagues.League.commandDefinition
{


    /// <summary>
    /// Create a new running league to which teams can belong
    /// </summary>
    [DomainNameAttribute("Leagues")]
    [Category("Administration")]
    public class Create_New_League_Definition : 
        CommandDefinitionBase, ICreate_New_League_Definition
    {
        
        /// <summary>
        /// The unique name of this command
        /// Create New League
        /// </summary>
        public override string CommandName
        {
            get
            {
                return "Create New League";
            }
        }
        
        /// <summary>
        /// The proposed name of the new league
        /// </summary>
        /// <remarks>
        /// The name must be unique or the command should be rejected
        /// </remarks>
        [AggregateKey()]
        public string  LeagueName
        {
            get
            {
                return base.GetParameterValue<string>(nameof(LeagueName ), 0);
            }
            set
            {
                base.SetParameterValue(nameof(LeagueName), 0, ref value);
            }
        }
        
        /// <summary>
        /// The date the league was created
        /// </summary>
        /// <remarks>
        /// This may be before the event date for leagues that are on-boarded
        /// </remarks>
        public System.DateTime Date_Incorporated
        {
            get
            {
                return base.GetParameterValue<System.DateTime>("Date Incorporated", 0);
            }
            set
            {
                base.SetParameterValue("Date Incorporated", 0, ref value);
            }
        }
        
        /// <summary>
        /// Where the league is domiciled
        /// </summary>
        public string Location
        {
            get
            {
                return base.GetParameterValue<string>("Location", 0);
            }
            set
            {
                base.SetParameterValue("Location", 0, ref value);
            }
        }
        
        /// <summary>
        /// The twitter handle of the league organisation
        /// </summary>
        public string Twitter_Handle
        {
            get
            {
                return base.GetParameterValue<string>("Twitter Handle", 0);
            }
            set
            {
                base.SetParameterValue("Twitter Handle", 0, ref value);
            }
        }
        
        /// <summary>
        /// The primary contact address for the league
        /// </summary>
        public string Email_Address
        {
            get
            {
                return base.GetParameterValue<string>("Email Address", 0);
            }
            set
            {
                base.SetParameterValue("Email Address", 0, ref value);
            }
        }
    }
}