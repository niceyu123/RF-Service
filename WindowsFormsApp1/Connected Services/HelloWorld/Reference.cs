﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace WindowsFormsApp1.HelloWorld {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://example", ConfigurationName="HelloWorld.HelloWorld")]
    public interface HelloWorld {
        
        // CODEGEN: 操作 add 以后生成的消息协定不是 RPC，也不是换行文档。
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        WindowsFormsApp1.HelloWorld.addResponse add(WindowsFormsApp1.HelloWorld.addRequest request);
        
        // CODEGEN: 操作 sayHelloWorldFrom 以后生成的消息协定不是 RPC，也不是换行文档。
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        WindowsFormsApp1.HelloWorld.sayHelloWorldFromResponse sayHelloWorldFrom(WindowsFormsApp1.HelloWorld.sayHelloWorldFromRequest request);
        
        // CODEGEN: 操作 testFile 以后生成的消息协定不是 RPC，也不是换行文档。
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        WindowsFormsApp1.HelloWorld.testFileResponse testFile(WindowsFormsApp1.HelloWorld.testFileRequest request);
        
        // CODEGEN: 操作 testIntAndByte 以后生成的消息协定不是 RPC，也不是换行文档。
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        WindowsFormsApp1.HelloWorld.testIntAndByteResponse testIntAndByte(WindowsFormsApp1.HelloWorld.testIntAndByteRequest request);
        
        // CODEGEN: 操作 testCode 以后生成的消息协定不是 RPC，也不是换行文档。
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        WindowsFormsApp1.HelloWorld.testCodeResponse testCode(WindowsFormsApp1.HelloWorld.testCodeRequest request);
        
        // CODEGEN: 操作 testToServer 以后生成的消息协定不是 RPC，也不是换行文档。
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        WindowsFormsApp1.HelloWorld.testToServerResponse testToServer(WindowsFormsApp1.HelloWorld.testToServerRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class addRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://example", Order=0)]
        public double a;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://example", Order=1)]
        public double b;
        
        public addRequest() {
        }
        
        public addRequest(double a, double b) {
            this.a = a;
            this.b = b;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class addResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://example", Order=0)]
        public double addReturn;
        
        public addResponse() {
        }
        
        public addResponse(double addReturn) {
            this.addReturn = addReturn;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class sayHelloWorldFromRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://example", Order=0)]
        public string from;
        
        public sayHelloWorldFromRequest() {
        }
        
        public sayHelloWorldFromRequest(string from) {
            this.from = from;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class sayHelloWorldFromResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://example", Order=0)]
        public string sayHelloWorldFromReturn;
        
        public sayHelloWorldFromResponse() {
        }
        
        public sayHelloWorldFromResponse(string sayHelloWorldFromReturn) {
            this.sayHelloWorldFromReturn = sayHelloWorldFromReturn;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class testFileRequest {
        
        public testFileRequest() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class testFileResponse {
        
        public testFileResponse() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class testIntAndByteRequest {
        
        public testIntAndByteRequest() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class testIntAndByteResponse {
        
        public testIntAndByteResponse() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class testCodeRequest {
        
        public testCodeRequest() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class testCodeResponse {
        
        public testCodeResponse() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class testToServerRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://example", Order=0)]
        public string xml;
        
        public testToServerRequest() {
        }
        
        public testToServerRequest(string xml) {
            this.xml = xml;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class testToServerResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://example", Order=0)]
        public string testToServerReturn;
        
        public testToServerResponse() {
        }
        
        public testToServerResponse(string testToServerReturn) {
            this.testToServerReturn = testToServerReturn;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface HelloWorldChannel : WindowsFormsApp1.HelloWorld.HelloWorld, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class HelloWorldClient : System.ServiceModel.ClientBase<WindowsFormsApp1.HelloWorld.HelloWorld>, WindowsFormsApp1.HelloWorld.HelloWorld {
        
        public HelloWorldClient() {
        }
        
        public HelloWorldClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public HelloWorldClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public HelloWorldClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public HelloWorldClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        WindowsFormsApp1.HelloWorld.addResponse WindowsFormsApp1.HelloWorld.HelloWorld.add(WindowsFormsApp1.HelloWorld.addRequest request) {
            return base.Channel.add(request);
        }
        
        public double add(double a, double b) {
            WindowsFormsApp1.HelloWorld.addRequest inValue = new WindowsFormsApp1.HelloWorld.addRequest();
            inValue.a = a;
            inValue.b = b;
            WindowsFormsApp1.HelloWorld.addResponse retVal = ((WindowsFormsApp1.HelloWorld.HelloWorld)(this)).add(inValue);
            return retVal.addReturn;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        WindowsFormsApp1.HelloWorld.sayHelloWorldFromResponse WindowsFormsApp1.HelloWorld.HelloWorld.sayHelloWorldFrom(WindowsFormsApp1.HelloWorld.sayHelloWorldFromRequest request) {
            return base.Channel.sayHelloWorldFrom(request);
        }
        
        public string sayHelloWorldFrom(string from) {
            WindowsFormsApp1.HelloWorld.sayHelloWorldFromRequest inValue = new WindowsFormsApp1.HelloWorld.sayHelloWorldFromRequest();
            inValue.from = from;
            WindowsFormsApp1.HelloWorld.sayHelloWorldFromResponse retVal = ((WindowsFormsApp1.HelloWorld.HelloWorld)(this)).sayHelloWorldFrom(inValue);
            return retVal.sayHelloWorldFromReturn;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        WindowsFormsApp1.HelloWorld.testFileResponse WindowsFormsApp1.HelloWorld.HelloWorld.testFile(WindowsFormsApp1.HelloWorld.testFileRequest request) {
            return base.Channel.testFile(request);
        }
        
        public void testFile() {
            WindowsFormsApp1.HelloWorld.testFileRequest inValue = new WindowsFormsApp1.HelloWorld.testFileRequest();
            WindowsFormsApp1.HelloWorld.testFileResponse retVal = ((WindowsFormsApp1.HelloWorld.HelloWorld)(this)).testFile(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        WindowsFormsApp1.HelloWorld.testIntAndByteResponse WindowsFormsApp1.HelloWorld.HelloWorld.testIntAndByte(WindowsFormsApp1.HelloWorld.testIntAndByteRequest request) {
            return base.Channel.testIntAndByte(request);
        }
        
        public void testIntAndByte() {
            WindowsFormsApp1.HelloWorld.testIntAndByteRequest inValue = new WindowsFormsApp1.HelloWorld.testIntAndByteRequest();
            WindowsFormsApp1.HelloWorld.testIntAndByteResponse retVal = ((WindowsFormsApp1.HelloWorld.HelloWorld)(this)).testIntAndByte(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        WindowsFormsApp1.HelloWorld.testCodeResponse WindowsFormsApp1.HelloWorld.HelloWorld.testCode(WindowsFormsApp1.HelloWorld.testCodeRequest request) {
            return base.Channel.testCode(request);
        }
        
        public void testCode() {
            WindowsFormsApp1.HelloWorld.testCodeRequest inValue = new WindowsFormsApp1.HelloWorld.testCodeRequest();
            WindowsFormsApp1.HelloWorld.testCodeResponse retVal = ((WindowsFormsApp1.HelloWorld.HelloWorld)(this)).testCode(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        WindowsFormsApp1.HelloWorld.testToServerResponse WindowsFormsApp1.HelloWorld.HelloWorld.testToServer(WindowsFormsApp1.HelloWorld.testToServerRequest request) {
            return base.Channel.testToServer(request);
        }
        
        public string testToServer(string xml) {
            WindowsFormsApp1.HelloWorld.testToServerRequest inValue = new WindowsFormsApp1.HelloWorld.testToServerRequest();
            inValue.xml = xml;
            WindowsFormsApp1.HelloWorld.testToServerResponse retVal = ((WindowsFormsApp1.HelloWorld.HelloWorld)(this)).testToServer(inValue);
            return retVal.testToServerReturn;
        }
    }
}
