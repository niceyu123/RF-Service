﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace WindowsFormsApp1.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://172.16.11.19:1915/", ConfigurationName="ServiceReference1.NewOAWebServiceSoap")]
    public interface NewOAWebServiceSoap {
        
        // CODEGEN: 命名空间 http://172.16.11.19:1915/ 的元素名称 time 以后生成的消息协定未标记为 nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://172.16.11.19:1915/GetLeaveTime", ReplyAction="*")]
        WindowsFormsApp1.ServiceReference1.GetLeaveTimeResponse GetLeaveTime(WindowsFormsApp1.ServiceReference1.GetLeaveTimeRequest request);
        
        // CODEGEN: 命名空间 http://172.16.11.19:1915/ 的元素名称 time 以后生成的消息协定未标记为 nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://172.16.11.19:1915/GetIsPB", ReplyAction="*")]
        WindowsFormsApp1.ServiceReference1.GetIsPBResponse GetIsPB(WindowsFormsApp1.ServiceReference1.GetIsPBRequest request);
        
        // CODEGEN: 命名空间 http://172.16.11.19:1915/ 的元素名称 time 以后生成的消息协定未标记为 nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://172.16.11.19:1915/SaveLeaveTime", ReplyAction="*")]
        WindowsFormsApp1.ServiceReference1.SaveLeaveTimeResponse SaveLeaveTime(WindowsFormsApp1.ServiceReference1.SaveLeaveTimeRequest request);
        
        // CODEGEN: 命名空间 http://172.16.11.19:1915/ 的元素名称 time 以后生成的消息协定未标记为 nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://172.16.11.19:1915/SaveTXTime", ReplyAction="*")]
        WindowsFormsApp1.ServiceReference1.SaveTXTimeResponse SaveTXTime(WindowsFormsApp1.ServiceReference1.SaveTXTimeRequest request);
        
        // CODEGEN: 命名空间 http://172.16.11.19:1915/ 的元素名称 time 以后生成的消息协定未标记为 nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://172.16.11.19:1915/SaveCCTime", ReplyAction="*")]
        WindowsFormsApp1.ServiceReference1.SaveCCTimeResponse SaveCCTime(WindowsFormsApp1.ServiceReference1.SaveCCTimeRequest request);
        
        // CODEGEN: 命名空间 http://172.16.11.19:1915/ 的元素名称 time 以后生成的消息协定未标记为 nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://172.16.11.19:1915/SaveOTTime", ReplyAction="*")]
        WindowsFormsApp1.ServiceReference1.SaveOTTimeResponse SaveOTTime(WindowsFormsApp1.ServiceReference1.SaveOTTimeRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetLeaveTimeRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetLeaveTime", Namespace="http://172.16.11.19:1915/", Order=0)]
        public WindowsFormsApp1.ServiceReference1.GetLeaveTimeRequestBody Body;
        
        public GetLeaveTimeRequest() {
        }
        
        public GetLeaveTimeRequest(WindowsFormsApp1.ServiceReference1.GetLeaveTimeRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://172.16.11.19:1915/")]
    public partial class GetLeaveTimeRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string time;
        
        public GetLeaveTimeRequestBody() {
        }
        
        public GetLeaveTimeRequestBody(string time) {
            this.time = time;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetLeaveTimeResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetLeaveTimeResponse", Namespace="http://172.16.11.19:1915/", Order=0)]
        public WindowsFormsApp1.ServiceReference1.GetLeaveTimeResponseBody Body;
        
        public GetLeaveTimeResponse() {
        }
        
        public GetLeaveTimeResponse(WindowsFormsApp1.ServiceReference1.GetLeaveTimeResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://172.16.11.19:1915/")]
    public partial class GetLeaveTimeResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetLeaveTimeResult;
        
        public GetLeaveTimeResponseBody() {
        }
        
        public GetLeaveTimeResponseBody(string GetLeaveTimeResult) {
            this.GetLeaveTimeResult = GetLeaveTimeResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetIsPBRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetIsPB", Namespace="http://172.16.11.19:1915/", Order=0)]
        public WindowsFormsApp1.ServiceReference1.GetIsPBRequestBody Body;
        
        public GetIsPBRequest() {
        }
        
        public GetIsPBRequest(WindowsFormsApp1.ServiceReference1.GetIsPBRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://172.16.11.19:1915/")]
    public partial class GetIsPBRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string time;
        
        public GetIsPBRequestBody() {
        }
        
        public GetIsPBRequestBody(string time) {
            this.time = time;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetIsPBResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetIsPBResponse", Namespace="http://172.16.11.19:1915/", Order=0)]
        public WindowsFormsApp1.ServiceReference1.GetIsPBResponseBody Body;
        
        public GetIsPBResponse() {
        }
        
        public GetIsPBResponse(WindowsFormsApp1.ServiceReference1.GetIsPBResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://172.16.11.19:1915/")]
    public partial class GetIsPBResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetIsPBResult;
        
        public GetIsPBResponseBody() {
        }
        
        public GetIsPBResponseBody(string GetIsPBResult) {
            this.GetIsPBResult = GetIsPBResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SaveLeaveTimeRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="SaveLeaveTime", Namespace="http://172.16.11.19:1915/", Order=0)]
        public WindowsFormsApp1.ServiceReference1.SaveLeaveTimeRequestBody Body;
        
        public SaveLeaveTimeRequest() {
        }
        
        public SaveLeaveTimeRequest(WindowsFormsApp1.ServiceReference1.SaveLeaveTimeRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://172.16.11.19:1915/")]
    public partial class SaveLeaveTimeRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string time;
        
        public SaveLeaveTimeRequestBody() {
        }
        
        public SaveLeaveTimeRequestBody(string time) {
            this.time = time;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SaveLeaveTimeResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="SaveLeaveTimeResponse", Namespace="http://172.16.11.19:1915/", Order=0)]
        public WindowsFormsApp1.ServiceReference1.SaveLeaveTimeResponseBody Body;
        
        public SaveLeaveTimeResponse() {
        }
        
        public SaveLeaveTimeResponse(WindowsFormsApp1.ServiceReference1.SaveLeaveTimeResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://172.16.11.19:1915/")]
    public partial class SaveLeaveTimeResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string SaveLeaveTimeResult;
        
        public SaveLeaveTimeResponseBody() {
        }
        
        public SaveLeaveTimeResponseBody(string SaveLeaveTimeResult) {
            this.SaveLeaveTimeResult = SaveLeaveTimeResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SaveTXTimeRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="SaveTXTime", Namespace="http://172.16.11.19:1915/", Order=0)]
        public WindowsFormsApp1.ServiceReference1.SaveTXTimeRequestBody Body;
        
        public SaveTXTimeRequest() {
        }
        
        public SaveTXTimeRequest(WindowsFormsApp1.ServiceReference1.SaveTXTimeRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://172.16.11.19:1915/")]
    public partial class SaveTXTimeRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string time;
        
        public SaveTXTimeRequestBody() {
        }
        
        public SaveTXTimeRequestBody(string time) {
            this.time = time;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SaveTXTimeResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="SaveTXTimeResponse", Namespace="http://172.16.11.19:1915/", Order=0)]
        public WindowsFormsApp1.ServiceReference1.SaveTXTimeResponseBody Body;
        
        public SaveTXTimeResponse() {
        }
        
        public SaveTXTimeResponse(WindowsFormsApp1.ServiceReference1.SaveTXTimeResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://172.16.11.19:1915/")]
    public partial class SaveTXTimeResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string SaveTXTimeResult;
        
        public SaveTXTimeResponseBody() {
        }
        
        public SaveTXTimeResponseBody(string SaveTXTimeResult) {
            this.SaveTXTimeResult = SaveTXTimeResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SaveCCTimeRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="SaveCCTime", Namespace="http://172.16.11.19:1915/", Order=0)]
        public WindowsFormsApp1.ServiceReference1.SaveCCTimeRequestBody Body;
        
        public SaveCCTimeRequest() {
        }
        
        public SaveCCTimeRequest(WindowsFormsApp1.ServiceReference1.SaveCCTimeRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://172.16.11.19:1915/")]
    public partial class SaveCCTimeRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string time;
        
        public SaveCCTimeRequestBody() {
        }
        
        public SaveCCTimeRequestBody(string time) {
            this.time = time;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SaveCCTimeResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="SaveCCTimeResponse", Namespace="http://172.16.11.19:1915/", Order=0)]
        public WindowsFormsApp1.ServiceReference1.SaveCCTimeResponseBody Body;
        
        public SaveCCTimeResponse() {
        }
        
        public SaveCCTimeResponse(WindowsFormsApp1.ServiceReference1.SaveCCTimeResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://172.16.11.19:1915/")]
    public partial class SaveCCTimeResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string SaveCCTimeResult;
        
        public SaveCCTimeResponseBody() {
        }
        
        public SaveCCTimeResponseBody(string SaveCCTimeResult) {
            this.SaveCCTimeResult = SaveCCTimeResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SaveOTTimeRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="SaveOTTime", Namespace="http://172.16.11.19:1915/", Order=0)]
        public WindowsFormsApp1.ServiceReference1.SaveOTTimeRequestBody Body;
        
        public SaveOTTimeRequest() {
        }
        
        public SaveOTTimeRequest(WindowsFormsApp1.ServiceReference1.SaveOTTimeRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://172.16.11.19:1915/")]
    public partial class SaveOTTimeRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string time;
        
        public SaveOTTimeRequestBody() {
        }
        
        public SaveOTTimeRequestBody(string time) {
            this.time = time;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SaveOTTimeResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="SaveOTTimeResponse", Namespace="http://172.16.11.19:1915/", Order=0)]
        public WindowsFormsApp1.ServiceReference1.SaveOTTimeResponseBody Body;
        
        public SaveOTTimeResponse() {
        }
        
        public SaveOTTimeResponse(WindowsFormsApp1.ServiceReference1.SaveOTTimeResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://172.16.11.19:1915/")]
    public partial class SaveOTTimeResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string SaveOTTimeResult;
        
        public SaveOTTimeResponseBody() {
        }
        
        public SaveOTTimeResponseBody(string SaveOTTimeResult) {
            this.SaveOTTimeResult = SaveOTTimeResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface NewOAWebServiceSoapChannel : WindowsFormsApp1.ServiceReference1.NewOAWebServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class NewOAWebServiceSoapClient : System.ServiceModel.ClientBase<WindowsFormsApp1.ServiceReference1.NewOAWebServiceSoap>, WindowsFormsApp1.ServiceReference1.NewOAWebServiceSoap {
        
        public NewOAWebServiceSoapClient() {
        }
        
        public NewOAWebServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public NewOAWebServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public NewOAWebServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public NewOAWebServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        WindowsFormsApp1.ServiceReference1.GetLeaveTimeResponse WindowsFormsApp1.ServiceReference1.NewOAWebServiceSoap.GetLeaveTime(WindowsFormsApp1.ServiceReference1.GetLeaveTimeRequest request) {
            return base.Channel.GetLeaveTime(request);
        }
        
        public string GetLeaveTime(string time) {
            WindowsFormsApp1.ServiceReference1.GetLeaveTimeRequest inValue = new WindowsFormsApp1.ServiceReference1.GetLeaveTimeRequest();
            inValue.Body = new WindowsFormsApp1.ServiceReference1.GetLeaveTimeRequestBody();
            inValue.Body.time = time;
            WindowsFormsApp1.ServiceReference1.GetLeaveTimeResponse retVal = ((WindowsFormsApp1.ServiceReference1.NewOAWebServiceSoap)(this)).GetLeaveTime(inValue);
            return retVal.Body.GetLeaveTimeResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        WindowsFormsApp1.ServiceReference1.GetIsPBResponse WindowsFormsApp1.ServiceReference1.NewOAWebServiceSoap.GetIsPB(WindowsFormsApp1.ServiceReference1.GetIsPBRequest request) {
            return base.Channel.GetIsPB(request);
        }
        
        public string GetIsPB(string time) {
            WindowsFormsApp1.ServiceReference1.GetIsPBRequest inValue = new WindowsFormsApp1.ServiceReference1.GetIsPBRequest();
            inValue.Body = new WindowsFormsApp1.ServiceReference1.GetIsPBRequestBody();
            inValue.Body.time = time;
            WindowsFormsApp1.ServiceReference1.GetIsPBResponse retVal = ((WindowsFormsApp1.ServiceReference1.NewOAWebServiceSoap)(this)).GetIsPB(inValue);
            return retVal.Body.GetIsPBResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        WindowsFormsApp1.ServiceReference1.SaveLeaveTimeResponse WindowsFormsApp1.ServiceReference1.NewOAWebServiceSoap.SaveLeaveTime(WindowsFormsApp1.ServiceReference1.SaveLeaveTimeRequest request) {
            return base.Channel.SaveLeaveTime(request);
        }
        
        public string SaveLeaveTime(string time) {
            WindowsFormsApp1.ServiceReference1.SaveLeaveTimeRequest inValue = new WindowsFormsApp1.ServiceReference1.SaveLeaveTimeRequest();
            inValue.Body = new WindowsFormsApp1.ServiceReference1.SaveLeaveTimeRequestBody();
            inValue.Body.time = time;
            WindowsFormsApp1.ServiceReference1.SaveLeaveTimeResponse retVal = ((WindowsFormsApp1.ServiceReference1.NewOAWebServiceSoap)(this)).SaveLeaveTime(inValue);
            return retVal.Body.SaveLeaveTimeResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        WindowsFormsApp1.ServiceReference1.SaveTXTimeResponse WindowsFormsApp1.ServiceReference1.NewOAWebServiceSoap.SaveTXTime(WindowsFormsApp1.ServiceReference1.SaveTXTimeRequest request) {
            return base.Channel.SaveTXTime(request);
        }
        
        public string SaveTXTime(string time) {
            WindowsFormsApp1.ServiceReference1.SaveTXTimeRequest inValue = new WindowsFormsApp1.ServiceReference1.SaveTXTimeRequest();
            inValue.Body = new WindowsFormsApp1.ServiceReference1.SaveTXTimeRequestBody();
            inValue.Body.time = time;
            WindowsFormsApp1.ServiceReference1.SaveTXTimeResponse retVal = ((WindowsFormsApp1.ServiceReference1.NewOAWebServiceSoap)(this)).SaveTXTime(inValue);
            return retVal.Body.SaveTXTimeResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        WindowsFormsApp1.ServiceReference1.SaveCCTimeResponse WindowsFormsApp1.ServiceReference1.NewOAWebServiceSoap.SaveCCTime(WindowsFormsApp1.ServiceReference1.SaveCCTimeRequest request) {
            return base.Channel.SaveCCTime(request);
        }
        
        public string SaveCCTime(string time) {
            WindowsFormsApp1.ServiceReference1.SaveCCTimeRequest inValue = new WindowsFormsApp1.ServiceReference1.SaveCCTimeRequest();
            inValue.Body = new WindowsFormsApp1.ServiceReference1.SaveCCTimeRequestBody();
            inValue.Body.time = time;
            WindowsFormsApp1.ServiceReference1.SaveCCTimeResponse retVal = ((WindowsFormsApp1.ServiceReference1.NewOAWebServiceSoap)(this)).SaveCCTime(inValue);
            return retVal.Body.SaveCCTimeResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        WindowsFormsApp1.ServiceReference1.SaveOTTimeResponse WindowsFormsApp1.ServiceReference1.NewOAWebServiceSoap.SaveOTTime(WindowsFormsApp1.ServiceReference1.SaveOTTimeRequest request) {
            return base.Channel.SaveOTTime(request);
        }
        
        public string SaveOTTime(string time) {
            WindowsFormsApp1.ServiceReference1.SaveOTTimeRequest inValue = new WindowsFormsApp1.ServiceReference1.SaveOTTimeRequest();
            inValue.Body = new WindowsFormsApp1.ServiceReference1.SaveOTTimeRequestBody();
            inValue.Body.time = time;
            WindowsFormsApp1.ServiceReference1.SaveOTTimeResponse retVal = ((WindowsFormsApp1.ServiceReference1.NewOAWebServiceSoap)(this)).SaveOTTime(inValue);
            return retVal.Body.SaveOTTimeResult;
        }
    }
}
