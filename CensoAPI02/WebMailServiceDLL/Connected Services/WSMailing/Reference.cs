//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebMailServiceDLL.WSMailing {
    using System.Runtime.Serialization;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TypeOfMail", Namespace="http://schemas.datacontract.org/2004/07/MailingWebService")]
    public enum TypeOfMail : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        HTML = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Text = 2,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WSMailing.IMailingWebService")]
    public interface IMailingWebService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMailingWebService/SendEmailWithSimpleFileWValidity", ReplyAction="http://tempuri.org/IMailingWebService/SendEmailWithSimpleFileWValidityResponse")]
        string SendEmailWithSimpleFileWValidity(string Application, string To, string CC, string Subject, string BodyMessage, WebMailServiceDLL.WSMailing.TypeOfMail Type, int DaysValidity, string strFilePath);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMailingWebService/SendEmailWithSimpleFileWValidity", ReplyAction="http://tempuri.org/IMailingWebService/SendEmailWithSimpleFileWValidityResponse")]
        System.Threading.Tasks.Task<string> SendEmailWithSimpleFileWValidityAsync(string Application, string To, string CC, string Subject, string BodyMessage, WebMailServiceDLL.WSMailing.TypeOfMail Type, int DaysValidity, string strFilePath);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMailingWebService/SendEmailWValidity", ReplyAction="http://tempuri.org/IMailingWebService/SendEmailWValidityResponse")]
        string SendEmailWValidity(string Application, string To, string CC, string Subject, string BodyMessage, WebMailServiceDLL.WSMailing.TypeOfMail Type, int DaysValidity);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMailingWebService/SendEmailWValidity", ReplyAction="http://tempuri.org/IMailingWebService/SendEmailWValidityResponse")]
        System.Threading.Tasks.Task<string> SendEmailWValidityAsync(string Application, string To, string CC, string Subject, string BodyMessage, WebMailServiceDLL.WSMailing.TypeOfMail Type, int DaysValidity);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMailingWebService/SendEmailWithSimpleFile", ReplyAction="http://tempuri.org/IMailingWebService/SendEmailWithSimpleFileResponse")]
        string SendEmailWithSimpleFile(string Application, string To, string CC, string Subject, string BodyMessage, WebMailServiceDLL.WSMailing.TypeOfMail Type, string strFilePath);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMailingWebService/SendEmailWithSimpleFile", ReplyAction="http://tempuri.org/IMailingWebService/SendEmailWithSimpleFileResponse")]
        System.Threading.Tasks.Task<string> SendEmailWithSimpleFileAsync(string Application, string To, string CC, string Subject, string BodyMessage, WebMailServiceDLL.WSMailing.TypeOfMail Type, string strFilePath);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMailingWebService/SendEmail", ReplyAction="http://tempuri.org/IMailingWebService/SendEmailResponse")]
        string SendEmail(string Application, string To, string CC, string Subject, string BodyMessage, WebMailServiceDLL.WSMailing.TypeOfMail Type);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMailingWebService/SendEmail", ReplyAction="http://tempuri.org/IMailingWebService/SendEmailResponse")]
        System.Threading.Tasks.Task<string> SendEmailAsync(string Application, string To, string CC, string Subject, string BodyMessage, WebMailServiceDLL.WSMailing.TypeOfMail Type);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMailingWebService/SendEmailWithMultipleFiles", ReplyAction="http://tempuri.org/IMailingWebService/SendEmailWithMultipleFilesResponse")]
        string SendEmailWithMultipleFiles(string Application, string To, string CC, string Subject, string BodyMessage, WebMailServiceDLL.WSMailing.TypeOfMail Type, int DaysValidity, string[] strFilePaths);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMailingWebService/SendEmailWithMultipleFiles", ReplyAction="http://tempuri.org/IMailingWebService/SendEmailWithMultipleFilesResponse")]
        System.Threading.Tasks.Task<string> SendEmailWithMultipleFilesAsync(string Application, string To, string CC, string Subject, string BodyMessage, WebMailServiceDLL.WSMailing.TypeOfMail Type, int DaysValidity, string[] strFilePaths);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMailingWebService/SendEmailWithMultipleFilesWValidity", ReplyAction="http://tempuri.org/IMailingWebService/SendEmailWithMultipleFilesWValidityResponse" +
            "")]
        string SendEmailWithMultipleFilesWValidity(string Application, string To, string CC, string Subject, string BodyMessage, WebMailServiceDLL.WSMailing.TypeOfMail Type, int DaysValidity, string[] strFilePaths);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMailingWebService/SendEmailWithMultipleFilesWValidity", ReplyAction="http://tempuri.org/IMailingWebService/SendEmailWithMultipleFilesWValidityResponse" +
            "")]
        System.Threading.Tasks.Task<string> SendEmailWithMultipleFilesWValidityAsync(string Application, string To, string CC, string Subject, string BodyMessage, WebMailServiceDLL.WSMailing.TypeOfMail Type, int DaysValidity, string[] strFilePaths);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IMailingWebServiceChannel : WebMailServiceDLL.WSMailing.IMailingWebService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class MailingWebServiceClient : System.ServiceModel.ClientBase<WebMailServiceDLL.WSMailing.IMailingWebService>, WebMailServiceDLL.WSMailing.IMailingWebService {
        
        public MailingWebServiceClient() {
        }
        
        public MailingWebServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public MailingWebServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MailingWebServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MailingWebServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string SendEmailWithSimpleFileWValidity(string Application, string To, string CC, string Subject, string BodyMessage, WebMailServiceDLL.WSMailing.TypeOfMail Type, int DaysValidity, string strFilePath) {
            return base.Channel.SendEmailWithSimpleFileWValidity(Application, To, CC, Subject, BodyMessage, Type, DaysValidity, strFilePath);
        }
        
        public System.Threading.Tasks.Task<string> SendEmailWithSimpleFileWValidityAsync(string Application, string To, string CC, string Subject, string BodyMessage, WebMailServiceDLL.WSMailing.TypeOfMail Type, int DaysValidity, string strFilePath) {
            return base.Channel.SendEmailWithSimpleFileWValidityAsync(Application, To, CC, Subject, BodyMessage, Type, DaysValidity, strFilePath);
        }
        
        public string SendEmailWValidity(string Application, string To, string CC, string Subject, string BodyMessage, WebMailServiceDLL.WSMailing.TypeOfMail Type, int DaysValidity) {
            return base.Channel.SendEmailWValidity(Application, To, CC, Subject, BodyMessage, Type, DaysValidity);
        }
        
        public System.Threading.Tasks.Task<string> SendEmailWValidityAsync(string Application, string To, string CC, string Subject, string BodyMessage, WebMailServiceDLL.WSMailing.TypeOfMail Type, int DaysValidity) {
            return base.Channel.SendEmailWValidityAsync(Application, To, CC, Subject, BodyMessage, Type, DaysValidity);
        }
        
        public string SendEmailWithSimpleFile(string Application, string To, string CC, string Subject, string BodyMessage, WebMailServiceDLL.WSMailing.TypeOfMail Type, string strFilePath) {
            return base.Channel.SendEmailWithSimpleFile(Application, To, CC, Subject, BodyMessage, Type, strFilePath);
        }
        
        public System.Threading.Tasks.Task<string> SendEmailWithSimpleFileAsync(string Application, string To, string CC, string Subject, string BodyMessage, WebMailServiceDLL.WSMailing.TypeOfMail Type, string strFilePath) {
            return base.Channel.SendEmailWithSimpleFileAsync(Application, To, CC, Subject, BodyMessage, Type, strFilePath);
        }
        
        public string SendEmail(string Application, string To, string CC, string Subject, string BodyMessage, WebMailServiceDLL.WSMailing.TypeOfMail Type) {
            return base.Channel.SendEmail(Application, To, CC, Subject, BodyMessage, Type);
        }
        
        public System.Threading.Tasks.Task<string> SendEmailAsync(string Application, string To, string CC, string Subject, string BodyMessage, WebMailServiceDLL.WSMailing.TypeOfMail Type) {
            return base.Channel.SendEmailAsync(Application, To, CC, Subject, BodyMessage, Type);
        }
        
        public string SendEmailWithMultipleFiles(string Application, string To, string CC, string Subject, string BodyMessage, WebMailServiceDLL.WSMailing.TypeOfMail Type, int DaysValidity, string[] strFilePaths) {
            return base.Channel.SendEmailWithMultipleFiles(Application, To, CC, Subject, BodyMessage, Type, DaysValidity, strFilePaths);
        }
        
        public System.Threading.Tasks.Task<string> SendEmailWithMultipleFilesAsync(string Application, string To, string CC, string Subject, string BodyMessage, WebMailServiceDLL.WSMailing.TypeOfMail Type, int DaysValidity, string[] strFilePaths) {
            return base.Channel.SendEmailWithMultipleFilesAsync(Application, To, CC, Subject, BodyMessage, Type, DaysValidity, strFilePaths);
        }
        
        public string SendEmailWithMultipleFilesWValidity(string Application, string To, string CC, string Subject, string BodyMessage, WebMailServiceDLL.WSMailing.TypeOfMail Type, int DaysValidity, string[] strFilePaths) {
            return base.Channel.SendEmailWithMultipleFilesWValidity(Application, To, CC, Subject, BodyMessage, Type, DaysValidity, strFilePaths);
        }
        
        public System.Threading.Tasks.Task<string> SendEmailWithMultipleFilesWValidityAsync(string Application, string To, string CC, string Subject, string BodyMessage, WebMailServiceDLL.WSMailing.TypeOfMail Type, int DaysValidity, string[] strFilePaths) {
            return base.Channel.SendEmailWithMultipleFilesWValidityAsync(Application, To, CC, Subject, BodyMessage, Type, DaysValidity, strFilePaths);
        }
    }
}
