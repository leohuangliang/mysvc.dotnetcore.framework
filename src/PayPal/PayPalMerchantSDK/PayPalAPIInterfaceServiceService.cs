using System;
using System.Collections.Generic;
using System.Xml;
using PayPal;
using PayPal.Authentication;
using PayPal.Util;
using PayPal.Manager;
using PayPal.SOAP;
using PayPal.PayPalAPIInterfaceService.Model;

namespace PayPal.PayPalAPIInterfaceService 
{
	public partial class PayPalAPIInterfaceServiceService : BasePayPalService 
	{

		/// <summary>
		/// Service Version
		/// </summary>
		private const string ServiceVersion = "204.0";

		/// <summary>
		/// Service Name
		/// </summary>
		private const string ServiceName = "PayPalAPIInterfaceService";
		
		/// <summary>
		/// SDK Name
		/// </summary>
		private const string SDKName = "merchant-dotnet-sdk";
	
		/// <summary>
		/// SDK Version
		/// </summary>
		private const string SDKVersion = "2.16.205";

		/// <summary>
		/// Default constructor for loading configuration from *.Config file
		/// </summary>
		public PayPalAPIInterfaceServiceService() : base() {}
		
		/// <summary>
		/// constructor for passing in a dynamic configuration object
		/// </summary>
		public PayPalAPIInterfaceServiceService(Dictionary<string, string> config) : base(config) {}
		
	
		private void setStandardParams(AbstractRequestType request) {
			if (request.Version == null)
			{
				request.Version = ServiceVersion;
			}
			if (request.ErrorLanguage != null && this.config.ContainsKey("languageCode"))
			{
				request.ErrorLanguage = config["languageCode"];
			}
		}

		/// <summary>
		/// 
	 	/// </summary>
		///<param name="refundTransactionReq"></param>
		///<param name="apiUserName">API Username that you want to authenticate this call against. This username and the corresponding 3-token/certificate credentials must be available in Web.Config/App.Config</param>
	 	public RefundTransactionResponseType RefundTransaction(RefundTransactionReq refundTransactionReq, string apiUserName)
	 	{	 		
			setStandardParams(refundTransactionReq.RefundTransactionRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, refundTransactionReq.ToXMLString(null, "RefundTransactionReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, apiUserName, getAccessToken(), getAccessTokenSecret());
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPI";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new RefundTransactionResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='RefundTransactionResponse']")
			);
			
	 	}
	 
	 	/// <summary> 
		/// 
	 	/// </summary>
		///<param name="refundTransactionReq"></param>
	 	
	 	public RefundTransactionResponseType RefundTransaction(RefundTransactionReq refundTransactionReq)
	 	{
	 		return RefundTransaction(refundTransactionReq,(string) null);
	 	}
	 	
	 	/// <summary>
		/// 
	 	/// </summary>
		///<param name="refundTransactionReq"></param>
		///<param name="credential">An explicit ICredential object that you want to authenticate this call against</param> 
	 	public RefundTransactionResponseType RefundTransaction(RefundTransactionReq refundTransactionReq, ICredential credential)
	 	{	 			 		
			setStandardParams(refundTransactionReq.RefundTransactionRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, refundTransactionReq.ToXMLString(null, "RefundTransactionReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, credential);
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPI";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new RefundTransactionResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='RefundTransactionResponse']")
			);
			
	 	}

		/// <summary>
		/// 
	 	/// </summary>
		///<param name="initiateRecoupReq"></param>
		///<param name="apiUserName">API Username that you want to authenticate this call against. This username and the corresponding 3-token/certificate credentials must be available in Web.Config/App.Config</param>
	 	public InitiateRecoupResponseType InitiateRecoup(InitiateRecoupReq initiateRecoupReq, string apiUserName)
	 	{	 		
			setStandardParams(initiateRecoupReq.InitiateRecoupRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, initiateRecoupReq.ToXMLString(null, "InitiateRecoupReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, apiUserName, getAccessToken(), getAccessTokenSecret());
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPI";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new InitiateRecoupResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='InitiateRecoupResponse']")
			);
			
	 	}
	 
	 	/// <summary> 
		/// 
	 	/// </summary>
		///<param name="initiateRecoupReq"></param>
	 	
	 	public InitiateRecoupResponseType InitiateRecoup(InitiateRecoupReq initiateRecoupReq)
	 	{
	 		return InitiateRecoup(initiateRecoupReq,(string) null);
	 	}
	 	
	 	/// <summary>
		/// 
	 	/// </summary>
		///<param name="initiateRecoupReq"></param>
		///<param name="credential">An explicit ICredential object that you want to authenticate this call against</param> 
	 	public InitiateRecoupResponseType InitiateRecoup(InitiateRecoupReq initiateRecoupReq, ICredential credential)
	 	{	 			 		
			setStandardParams(initiateRecoupReq.InitiateRecoupRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, initiateRecoupReq.ToXMLString(null, "InitiateRecoupReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, credential);
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPI";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new InitiateRecoupResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='InitiateRecoupResponse']")
			);
			
	 	}

		/// <summary>
		/// 
	 	/// </summary>
		///<param name="completeRecoupReq"></param>
		///<param name="apiUserName">API Username that you want to authenticate this call against. This username and the corresponding 3-token/certificate credentials must be available in Web.Config/App.Config</param>
	 	public CompleteRecoupResponseType CompleteRecoup(CompleteRecoupReq completeRecoupReq, string apiUserName)
	 	{	 		
			setStandardParams(completeRecoupReq.CompleteRecoupRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, completeRecoupReq.ToXMLString(null, "CompleteRecoupReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, apiUserName, getAccessToken(), getAccessTokenSecret());
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPI";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new CompleteRecoupResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='CompleteRecoupResponse']")
			);
			
	 	}
	 
	 	/// <summary> 
		/// 
	 	/// </summary>
		///<param name="completeRecoupReq"></param>
	 	
	 	public CompleteRecoupResponseType CompleteRecoup(CompleteRecoupReq completeRecoupReq)
	 	{
	 		return CompleteRecoup(completeRecoupReq,(string) null);
	 	}
	 	
	 	/// <summary>
		/// 
	 	/// </summary>
		///<param name="completeRecoupReq"></param>
		///<param name="credential">An explicit ICredential object that you want to authenticate this call against</param> 
	 	public CompleteRecoupResponseType CompleteRecoup(CompleteRecoupReq completeRecoupReq, ICredential credential)
	 	{	 			 		
			setStandardParams(completeRecoupReq.CompleteRecoupRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, completeRecoupReq.ToXMLString(null, "CompleteRecoupReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, credential);
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPI";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new CompleteRecoupResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='CompleteRecoupResponse']")
			);
			
	 	}

		/// <summary>
		/// 
	 	/// </summary>
		///<param name="cancelRecoupReq"></param>
		///<param name="apiUserName">API Username that you want to authenticate this call against. This username and the corresponding 3-token/certificate credentials must be available in Web.Config/App.Config</param>
	 	public CancelRecoupResponseType CancelRecoup(CancelRecoupReq cancelRecoupReq, string apiUserName)
	 	{	 		
			setStandardParams(cancelRecoupReq.CancelRecoupRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, cancelRecoupReq.ToXMLString(null, "CancelRecoupReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, apiUserName, getAccessToken(), getAccessTokenSecret());
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPI";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new CancelRecoupResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='CancelRecoupResponse']")
			);
			
	 	}
	 
	 	/// <summary> 
		/// 
	 	/// </summary>
		///<param name="cancelRecoupReq"></param>
	 	
	 	public CancelRecoupResponseType CancelRecoup(CancelRecoupReq cancelRecoupReq)
	 	{
	 		return CancelRecoup(cancelRecoupReq,(string) null);
	 	}
	 	
	 	/// <summary>
		/// 
	 	/// </summary>
		///<param name="cancelRecoupReq"></param>
		///<param name="credential">An explicit ICredential object that you want to authenticate this call against</param> 
	 	public CancelRecoupResponseType CancelRecoup(CancelRecoupReq cancelRecoupReq, ICredential credential)
	 	{	 			 		
			setStandardParams(cancelRecoupReq.CancelRecoupRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, cancelRecoupReq.ToXMLString(null, "CancelRecoupReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, credential);
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPI";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new CancelRecoupResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='CancelRecoupResponse']")
			);
			
	 	}

		/// <summary>
		/// 
	 	/// </summary>
		///<param name="getTransactionDetailsReq"></param>
		///<param name="apiUserName">API Username that you want to authenticate this call against. This username and the corresponding 3-token/certificate credentials must be available in Web.Config/App.Config</param>
	 	public GetTransactionDetailsResponseType GetTransactionDetails(GetTransactionDetailsReq getTransactionDetailsReq, string apiUserName)
	 	{	 		
			setStandardParams(getTransactionDetailsReq.GetTransactionDetailsRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, getTransactionDetailsReq.ToXMLString(null, "GetTransactionDetailsReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, apiUserName, getAccessToken(), getAccessTokenSecret());
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPI";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new GetTransactionDetailsResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='GetTransactionDetailsResponse']")
			);
			
	 	}
	 
	 	/// <summary> 
		/// 
	 	/// </summary>
		///<param name="getTransactionDetailsReq"></param>
	 	
	 	public GetTransactionDetailsResponseType GetTransactionDetails(GetTransactionDetailsReq getTransactionDetailsReq)
	 	{
	 		return GetTransactionDetails(getTransactionDetailsReq,(string) null);
	 	}
	 	
	 	/// <summary>
		/// 
	 	/// </summary>
		///<param name="getTransactionDetailsReq"></param>
		///<param name="credential">An explicit ICredential object that you want to authenticate this call against</param> 
	 	public GetTransactionDetailsResponseType GetTransactionDetails(GetTransactionDetailsReq getTransactionDetailsReq, ICredential credential)
	 	{	 			 		
			setStandardParams(getTransactionDetailsReq.GetTransactionDetailsRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, getTransactionDetailsReq.ToXMLString(null, "GetTransactionDetailsReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, credential);
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPI";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new GetTransactionDetailsResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='GetTransactionDetailsResponse']")
			);
			
	 	}

		/// <summary>
		/// 
	 	/// </summary>
		///<param name="billUserReq"></param>
		///<param name="apiUserName">API Username that you want to authenticate this call against. This username and the corresponding 3-token/certificate credentials must be available in Web.Config/App.Config</param>
	 	public BillUserResponseType BillUser(BillUserReq billUserReq, string apiUserName)
	 	{	 		
			setStandardParams(billUserReq.BillUserRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, billUserReq.ToXMLString(null, "BillUserReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, apiUserName, getAccessToken(), getAccessTokenSecret());
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPI";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new BillUserResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='BillUserResponse']")
			);
			
	 	}
	 
	 	/// <summary> 
		/// 
	 	/// </summary>
		///<param name="billUserReq"></param>
	 	
	 	public BillUserResponseType BillUser(BillUserReq billUserReq)
	 	{
	 		return BillUser(billUserReq,(string) null);
	 	}
	 	
	 	/// <summary>
		/// 
	 	/// </summary>
		///<param name="billUserReq"></param>
		///<param name="credential">An explicit ICredential object that you want to authenticate this call against</param> 
	 	public BillUserResponseType BillUser(BillUserReq billUserReq, ICredential credential)
	 	{	 			 		
			setStandardParams(billUserReq.BillUserRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, billUserReq.ToXMLString(null, "BillUserReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, credential);
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPI";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new BillUserResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='BillUserResponse']")
			);
			
	 	}

		/// <summary>
		/// 
	 	/// </summary>
		///<param name="transactionSearchReq"></param>
		///<param name="apiUserName">API Username that you want to authenticate this call against. This username and the corresponding 3-token/certificate credentials must be available in Web.Config/App.Config</param>
	 	public TransactionSearchResponseType TransactionSearch(TransactionSearchReq transactionSearchReq, string apiUserName)
	 	{	 		
			setStandardParams(transactionSearchReq.TransactionSearchRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, transactionSearchReq.ToXMLString(null, "TransactionSearchReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, apiUserName, getAccessToken(), getAccessTokenSecret());
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPI";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new TransactionSearchResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='TransactionSearchResponse']")
			);
			
	 	}
	 
	 	/// <summary> 
		/// 
	 	/// </summary>
		///<param name="transactionSearchReq"></param>
	 	
	 	public TransactionSearchResponseType TransactionSearch(TransactionSearchReq transactionSearchReq)
	 	{
	 		return TransactionSearch(transactionSearchReq,(string) null);
	 	}
	 	
	 	/// <summary>
		/// 
	 	/// </summary>
		///<param name="transactionSearchReq"></param>
		///<param name="credential">An explicit ICredential object that you want to authenticate this call against</param> 
	 	public TransactionSearchResponseType TransactionSearch(TransactionSearchReq transactionSearchReq, ICredential credential)
	 	{	 			 		
			setStandardParams(transactionSearchReq.TransactionSearchRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, transactionSearchReq.ToXMLString(null, "TransactionSearchReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, credential);
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPI";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new TransactionSearchResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='TransactionSearchResponse']")
			);
			
	 	}

		/// <summary>
		/// 
	 	/// </summary>
		///<param name="massPayReq"></param>
		///<param name="apiUserName">API Username that you want to authenticate this call against. This username and the corresponding 3-token/certificate credentials must be available in Web.Config/App.Config</param>
	 	public MassPayResponseType MassPay(MassPayReq massPayReq, string apiUserName)
	 	{	 		
			setStandardParams(massPayReq.MassPayRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, massPayReq.ToXMLString(null, "MassPayReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, apiUserName, getAccessToken(), getAccessTokenSecret());
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPI";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new MassPayResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='MassPayResponse']")
			);
			
	 	}
	 
	 	/// <summary> 
		/// 
	 	/// </summary>
		///<param name="massPayReq"></param>
	 	
	 	public MassPayResponseType MassPay(MassPayReq massPayReq)
	 	{
	 		return MassPay(massPayReq,(string) null);
	 	}
	 	
	 	/// <summary>
		/// 
	 	/// </summary>
		///<param name="massPayReq"></param>
		///<param name="credential">An explicit ICredential object that you want to authenticate this call against</param> 
	 	public MassPayResponseType MassPay(MassPayReq massPayReq, ICredential credential)
	 	{	 			 		
			setStandardParams(massPayReq.MassPayRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, massPayReq.ToXMLString(null, "MassPayReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, credential);
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPI";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new MassPayResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='MassPayResponse']")
			);
			
	 	}

		/// <summary>
		/// 
	 	/// </summary>
		///<param name="billAgreementUpdateReq"></param>
		///<param name="apiUserName">API Username that you want to authenticate this call against. This username and the corresponding 3-token/certificate credentials must be available in Web.Config/App.Config</param>
	 	public BAUpdateResponseType BillAgreementUpdate(BillAgreementUpdateReq billAgreementUpdateReq, string apiUserName)
	 	{	 		
			setStandardParams(billAgreementUpdateReq.BAUpdateRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, billAgreementUpdateReq.ToXMLString(null, "BillAgreementUpdateReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, apiUserName, getAccessToken(), getAccessTokenSecret());
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPI";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new BAUpdateResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='BAUpdateResponse']")
			);
			
	 	}
	 
	 	/// <summary> 
		/// 
	 	/// </summary>
		///<param name="billAgreementUpdateReq"></param>
	 	
	 	public BAUpdateResponseType BillAgreementUpdate(BillAgreementUpdateReq billAgreementUpdateReq)
	 	{
	 		return BillAgreementUpdate(billAgreementUpdateReq,(string) null);
	 	}
	 	
	 	/// <summary>
		/// 
	 	/// </summary>
		///<param name="billAgreementUpdateReq"></param>
		///<param name="credential">An explicit ICredential object that you want to authenticate this call against</param> 
	 	public BAUpdateResponseType BillAgreementUpdate(BillAgreementUpdateReq billAgreementUpdateReq, ICredential credential)
	 	{	 			 		
			setStandardParams(billAgreementUpdateReq.BAUpdateRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, billAgreementUpdateReq.ToXMLString(null, "BillAgreementUpdateReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, credential);
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPI";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new BAUpdateResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='BAUpdateResponse']")
			);
			
	 	}

		/// <summary>
		/// 
	 	/// </summary>
		///<param name="addressVerifyReq"></param>
		///<param name="apiUserName">API Username that you want to authenticate this call against. This username and the corresponding 3-token/certificate credentials must be available in Web.Config/App.Config</param>
	 	public AddressVerifyResponseType AddressVerify(AddressVerifyReq addressVerifyReq, string apiUserName)
	 	{	 		
			setStandardParams(addressVerifyReq.AddressVerifyRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, addressVerifyReq.ToXMLString(null, "AddressVerifyReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, apiUserName, getAccessToken(), getAccessTokenSecret());
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPI";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new AddressVerifyResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='AddressVerifyResponse']")
			);
			
	 	}
	 
	 	/// <summary> 
		/// 
	 	/// </summary>
		///<param name="addressVerifyReq"></param>
	 	
	 	public AddressVerifyResponseType AddressVerify(AddressVerifyReq addressVerifyReq)
	 	{
	 		return AddressVerify(addressVerifyReq,(string) null);
	 	}
	 	
	 	/// <summary>
		/// 
	 	/// </summary>
		///<param name="addressVerifyReq"></param>
		///<param name="credential">An explicit ICredential object that you want to authenticate this call against</param> 
	 	public AddressVerifyResponseType AddressVerify(AddressVerifyReq addressVerifyReq, ICredential credential)
	 	{	 			 		
			setStandardParams(addressVerifyReq.AddressVerifyRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, addressVerifyReq.ToXMLString(null, "AddressVerifyReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, credential);
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPI";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new AddressVerifyResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='AddressVerifyResponse']")
			);
			
	 	}

		/// <summary>
		/// 
	 	/// </summary>
		///<param name="enterBoardingReq"></param>
		///<param name="apiUserName">API Username that you want to authenticate this call against. This username and the corresponding 3-token/certificate credentials must be available in Web.Config/App.Config</param>
	 	public EnterBoardingResponseType EnterBoarding(EnterBoardingReq enterBoardingReq, string apiUserName)
	 	{	 		
			setStandardParams(enterBoardingReq.EnterBoardingRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, enterBoardingReq.ToXMLString(null, "EnterBoardingReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, apiUserName, getAccessToken(), getAccessTokenSecret());
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPI";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new EnterBoardingResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='EnterBoardingResponse']")
			);
			
	 	}
	 
	 	/// <summary> 
		/// 
	 	/// </summary>
		///<param name="enterBoardingReq"></param>
	 	
	 	public EnterBoardingResponseType EnterBoarding(EnterBoardingReq enterBoardingReq)
	 	{
	 		return EnterBoarding(enterBoardingReq,(string) null);
	 	}
	 	
	 	/// <summary>
		/// 
	 	/// </summary>
		///<param name="enterBoardingReq"></param>
		///<param name="credential">An explicit ICredential object that you want to authenticate this call against</param> 
	 	public EnterBoardingResponseType EnterBoarding(EnterBoardingReq enterBoardingReq, ICredential credential)
	 	{	 			 		
			setStandardParams(enterBoardingReq.EnterBoardingRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, enterBoardingReq.ToXMLString(null, "EnterBoardingReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, credential);
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPI";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new EnterBoardingResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='EnterBoardingResponse']")
			);
			
	 	}

		/// <summary>
		/// 
	 	/// </summary>
		///<param name="getBoardingDetailsReq"></param>
		///<param name="apiUserName">API Username that you want to authenticate this call against. This username and the corresponding 3-token/certificate credentials must be available in Web.Config/App.Config</param>
	 	public GetBoardingDetailsResponseType GetBoardingDetails(GetBoardingDetailsReq getBoardingDetailsReq, string apiUserName)
	 	{	 		
			setStandardParams(getBoardingDetailsReq.GetBoardingDetailsRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, getBoardingDetailsReq.ToXMLString(null, "GetBoardingDetailsReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, apiUserName, getAccessToken(), getAccessTokenSecret());
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPI";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new GetBoardingDetailsResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='GetBoardingDetailsResponse']")
			);
			
	 	}
	 
	 	/// <summary> 
		/// 
	 	/// </summary>
		///<param name="getBoardingDetailsReq"></param>
	 	
	 	public GetBoardingDetailsResponseType GetBoardingDetails(GetBoardingDetailsReq getBoardingDetailsReq)
	 	{
	 		return GetBoardingDetails(getBoardingDetailsReq,(string) null);
	 	}
	 	
	 	/// <summary>
		/// 
	 	/// </summary>
		///<param name="getBoardingDetailsReq"></param>
		///<param name="credential">An explicit ICredential object that you want to authenticate this call against</param> 
	 	public GetBoardingDetailsResponseType GetBoardingDetails(GetBoardingDetailsReq getBoardingDetailsReq, ICredential credential)
	 	{	 			 		
			setStandardParams(getBoardingDetailsReq.GetBoardingDetailsRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, getBoardingDetailsReq.ToXMLString(null, "GetBoardingDetailsReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, credential);
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPI";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new GetBoardingDetailsResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='GetBoardingDetailsResponse']")
			);
			
	 	}

		/// <summary>
		/// 
	 	/// </summary>
		///<param name="createMobilePaymentReq"></param>
		///<param name="apiUserName">API Username that you want to authenticate this call against. This username and the corresponding 3-token/certificate credentials must be available in Web.Config/App.Config</param>
	 	public CreateMobilePaymentResponseType CreateMobilePayment(CreateMobilePaymentReq createMobilePaymentReq, string apiUserName)
	 	{	 		
			setStandardParams(createMobilePaymentReq.CreateMobilePaymentRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, createMobilePaymentReq.ToXMLString(null, "CreateMobilePaymentReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, apiUserName, getAccessToken(), getAccessTokenSecret());
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPI";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new CreateMobilePaymentResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='CreateMobilePaymentResponse']")
			);
			
	 	}
	 
	 	/// <summary> 
		/// 
	 	/// </summary>
		///<param name="createMobilePaymentReq"></param>
	 	
	 	public CreateMobilePaymentResponseType CreateMobilePayment(CreateMobilePaymentReq createMobilePaymentReq)
	 	{
	 		return CreateMobilePayment(createMobilePaymentReq,(string) null);
	 	}
	 	
	 	/// <summary>
		/// 
	 	/// </summary>
		///<param name="createMobilePaymentReq"></param>
		///<param name="credential">An explicit ICredential object that you want to authenticate this call against</param> 
	 	public CreateMobilePaymentResponseType CreateMobilePayment(CreateMobilePaymentReq createMobilePaymentReq, ICredential credential)
	 	{	 			 		
			setStandardParams(createMobilePaymentReq.CreateMobilePaymentRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, createMobilePaymentReq.ToXMLString(null, "CreateMobilePaymentReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, credential);
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPI";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new CreateMobilePaymentResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='CreateMobilePaymentResponse']")
			);
			
	 	}

		/// <summary>
		/// 
	 	/// </summary>
		///<param name="getMobileStatusReq"></param>
		///<param name="apiUserName">API Username that you want to authenticate this call against. This username and the corresponding 3-token/certificate credentials must be available in Web.Config/App.Config</param>
	 	public GetMobileStatusResponseType GetMobileStatus(GetMobileStatusReq getMobileStatusReq, string apiUserName)
	 	{	 		
			setStandardParams(getMobileStatusReq.GetMobileStatusRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, getMobileStatusReq.ToXMLString(null, "GetMobileStatusReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, apiUserName, getAccessToken(), getAccessTokenSecret());
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPI";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new GetMobileStatusResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='GetMobileStatusResponse']")
			);
			
	 	}
	 
	 	/// <summary> 
		/// 
	 	/// </summary>
		///<param name="getMobileStatusReq"></param>
	 	
	 	public GetMobileStatusResponseType GetMobileStatus(GetMobileStatusReq getMobileStatusReq)
	 	{
	 		return GetMobileStatus(getMobileStatusReq,(string) null);
	 	}
	 	
	 	/// <summary>
		/// 
	 	/// </summary>
		///<param name="getMobileStatusReq"></param>
		///<param name="credential">An explicit ICredential object that you want to authenticate this call against</param> 
	 	public GetMobileStatusResponseType GetMobileStatus(GetMobileStatusReq getMobileStatusReq, ICredential credential)
	 	{	 			 		
			setStandardParams(getMobileStatusReq.GetMobileStatusRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, getMobileStatusReq.ToXMLString(null, "GetMobileStatusReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, credential);
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPI";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new GetMobileStatusResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='GetMobileStatusResponse']")
			);
			
	 	}

		/// <summary>
		/// 
	 	/// </summary>
		///<param name="setMobileCheckoutReq"></param>
		///<param name="apiUserName">API Username that you want to authenticate this call against. This username and the corresponding 3-token/certificate credentials must be available in Web.Config/App.Config</param>
	 	public SetMobileCheckoutResponseType SetMobileCheckout(SetMobileCheckoutReq setMobileCheckoutReq, string apiUserName)
	 	{	 		
			setStandardParams(setMobileCheckoutReq.SetMobileCheckoutRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, setMobileCheckoutReq.ToXMLString(null, "SetMobileCheckoutReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, apiUserName, getAccessToken(), getAccessTokenSecret());
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPI";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new SetMobileCheckoutResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='SetMobileCheckoutResponse']")
			);
			
	 	}
	 
	 	/// <summary> 
		/// 
	 	/// </summary>
		///<param name="setMobileCheckoutReq"></param>
	 	
	 	public SetMobileCheckoutResponseType SetMobileCheckout(SetMobileCheckoutReq setMobileCheckoutReq)
	 	{
	 		return SetMobileCheckout(setMobileCheckoutReq,(string) null);
	 	}
	 	
	 	/// <summary>
		/// 
	 	/// </summary>
		///<param name="setMobileCheckoutReq"></param>
		///<param name="credential">An explicit ICredential object that you want to authenticate this call against</param> 
	 	public SetMobileCheckoutResponseType SetMobileCheckout(SetMobileCheckoutReq setMobileCheckoutReq, ICredential credential)
	 	{	 			 		
			setStandardParams(setMobileCheckoutReq.SetMobileCheckoutRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, setMobileCheckoutReq.ToXMLString(null, "SetMobileCheckoutReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, credential);
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPI";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new SetMobileCheckoutResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='SetMobileCheckoutResponse']")
			);
			
	 	}

		/// <summary>
		/// 
	 	/// </summary>
		///<param name="doMobileCheckoutPaymentReq"></param>
		///<param name="apiUserName">API Username that you want to authenticate this call against. This username and the corresponding 3-token/certificate credentials must be available in Web.Config/App.Config</param>
	 	public DoMobileCheckoutPaymentResponseType DoMobileCheckoutPayment(DoMobileCheckoutPaymentReq doMobileCheckoutPaymentReq, string apiUserName)
	 	{	 		
			setStandardParams(doMobileCheckoutPaymentReq.DoMobileCheckoutPaymentRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, doMobileCheckoutPaymentReq.ToXMLString(null, "DoMobileCheckoutPaymentReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, apiUserName, getAccessToken(), getAccessTokenSecret());
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPI";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new DoMobileCheckoutPaymentResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='DoMobileCheckoutPaymentResponse']")
			);
			
	 	}
	 
	 	/// <summary> 
		/// 
	 	/// </summary>
		///<param name="doMobileCheckoutPaymentReq"></param>
	 	
	 	public DoMobileCheckoutPaymentResponseType DoMobileCheckoutPayment(DoMobileCheckoutPaymentReq doMobileCheckoutPaymentReq)
	 	{
	 		return DoMobileCheckoutPayment(doMobileCheckoutPaymentReq,(string) null);
	 	}
	 	
	 	/// <summary>
		/// 
	 	/// </summary>
		///<param name="doMobileCheckoutPaymentReq"></param>
		///<param name="credential">An explicit ICredential object that you want to authenticate this call against</param> 
	 	public DoMobileCheckoutPaymentResponseType DoMobileCheckoutPayment(DoMobileCheckoutPaymentReq doMobileCheckoutPaymentReq, ICredential credential)
	 	{	 			 		
			setStandardParams(doMobileCheckoutPaymentReq.DoMobileCheckoutPaymentRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, doMobileCheckoutPaymentReq.ToXMLString(null, "DoMobileCheckoutPaymentReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, credential);
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPI";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new DoMobileCheckoutPaymentResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='DoMobileCheckoutPaymentResponse']")
			);
			
	 	}

		/// <summary>
		/// 
	 	/// </summary>
		///<param name="getBalanceReq"></param>
		///<param name="apiUserName">API Username that you want to authenticate this call against. This username and the corresponding 3-token/certificate credentials must be available in Web.Config/App.Config</param>
	 	public GetBalanceResponseType GetBalance(GetBalanceReq getBalanceReq, string apiUserName)
	 	{	 		
			setStandardParams(getBalanceReq.GetBalanceRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, getBalanceReq.ToXMLString(null, "GetBalanceReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, apiUserName, getAccessToken(), getAccessTokenSecret());
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPI";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new GetBalanceResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='GetBalanceResponse']")
			);
			
	 	}
	 
	 	/// <summary> 
		/// 
	 	/// </summary>
		///<param name="getBalanceReq"></param>
	 	
	 	public GetBalanceResponseType GetBalance(GetBalanceReq getBalanceReq)
	 	{
	 		return GetBalance(getBalanceReq,(string) null);
	 	}
	 	
	 	/// <summary>
		/// 
	 	/// </summary>
		///<param name="getBalanceReq"></param>
		///<param name="credential">An explicit ICredential object that you want to authenticate this call against</param> 
	 	public GetBalanceResponseType GetBalance(GetBalanceReq getBalanceReq, ICredential credential)
	 	{	 			 		
			setStandardParams(getBalanceReq.GetBalanceRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, getBalanceReq.ToXMLString(null, "GetBalanceReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, credential);
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPI";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new GetBalanceResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='GetBalanceResponse']")
			);
			
	 	}

		/// <summary>
		/// 
	 	/// </summary>
		///<param name="getPalDetailsReq"></param>
		///<param name="apiUserName">API Username that you want to authenticate this call against. This username and the corresponding 3-token/certificate credentials must be available in Web.Config/App.Config</param>
	 	public GetPalDetailsResponseType GetPalDetails(GetPalDetailsReq getPalDetailsReq, string apiUserName)
	 	{	 		
			setStandardParams(getPalDetailsReq.GetPalDetailsRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, getPalDetailsReq.ToXMLString(null, "GetPalDetailsReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, apiUserName, getAccessToken(), getAccessTokenSecret());
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPI";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new GetPalDetailsResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='GetPalDetailsResponse']")
			);
			
	 	}
	 
	 	/// <summary> 
		/// 
	 	/// </summary>
		///<param name="getPalDetailsReq"></param>
	 	
	 	public GetPalDetailsResponseType GetPalDetails(GetPalDetailsReq getPalDetailsReq)
	 	{
	 		return GetPalDetails(getPalDetailsReq,(string) null);
	 	}
	 	
	 	/// <summary>
		/// 
	 	/// </summary>
		///<param name="getPalDetailsReq"></param>
		///<param name="credential">An explicit ICredential object that you want to authenticate this call against</param> 
	 	public GetPalDetailsResponseType GetPalDetails(GetPalDetailsReq getPalDetailsReq, ICredential credential)
	 	{	 			 		
			setStandardParams(getPalDetailsReq.GetPalDetailsRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, getPalDetailsReq.ToXMLString(null, "GetPalDetailsReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, credential);
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPI";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new GetPalDetailsResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='GetPalDetailsResponse']")
			);
			
	 	}

		/// <summary>
		/// 
	 	/// </summary>
		///<param name="doExpressCheckoutPaymentReq"></param>
		///<param name="apiUserName">API Username that you want to authenticate this call against. This username and the corresponding 3-token/certificate credentials must be available in Web.Config/App.Config</param>
	 	public DoExpressCheckoutPaymentResponseType DoExpressCheckoutPayment(DoExpressCheckoutPaymentReq doExpressCheckoutPaymentReq, string apiUserName)
	 	{	 		
			setStandardParams(doExpressCheckoutPaymentReq.DoExpressCheckoutPaymentRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, doExpressCheckoutPaymentReq.ToXMLString(null, "DoExpressCheckoutPaymentReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, apiUserName, getAccessToken(), getAccessTokenSecret());
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new DoExpressCheckoutPaymentResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='DoExpressCheckoutPaymentResponse']")
			);
			
	 	}
	 
	 	/// <summary> 
		/// 
	 	/// </summary>
		///<param name="doExpressCheckoutPaymentReq"></param>
	 	
	 	public DoExpressCheckoutPaymentResponseType DoExpressCheckoutPayment(DoExpressCheckoutPaymentReq doExpressCheckoutPaymentReq)
	 	{
	 		return DoExpressCheckoutPayment(doExpressCheckoutPaymentReq,(string) null);
	 	}
	 	
	 	/// <summary>
		/// 
	 	/// </summary>
		///<param name="doExpressCheckoutPaymentReq"></param>
		///<param name="credential">An explicit ICredential object that you want to authenticate this call against</param> 
	 	public DoExpressCheckoutPaymentResponseType DoExpressCheckoutPayment(DoExpressCheckoutPaymentReq doExpressCheckoutPaymentReq, ICredential credential)
	 	{	 			 		
			setStandardParams(doExpressCheckoutPaymentReq.DoExpressCheckoutPaymentRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, doExpressCheckoutPaymentReq.ToXMLString(null, "DoExpressCheckoutPaymentReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, credential);
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new DoExpressCheckoutPaymentResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='DoExpressCheckoutPaymentResponse']")
			);
			
	 	}

		/// <summary>
		/// 
	 	/// </summary>
		///<param name="doUATPExpressCheckoutPaymentReq"></param>
		///<param name="apiUserName">API Username that you want to authenticate this call against. This username and the corresponding 3-token/certificate credentials must be available in Web.Config/App.Config</param>
	 	public DoUATPExpressCheckoutPaymentResponseType DoUATPExpressCheckoutPayment(DoUATPExpressCheckoutPaymentReq doUATPExpressCheckoutPaymentReq, string apiUserName)
	 	{	 		
			setStandardParams(doUATPExpressCheckoutPaymentReq.DoUATPExpressCheckoutPaymentRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, doUATPExpressCheckoutPaymentReq.ToXMLString(null, "DoUATPExpressCheckoutPaymentReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, apiUserName, getAccessToken(), getAccessTokenSecret());
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new DoUATPExpressCheckoutPaymentResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='DoUATPExpressCheckoutPaymentResponse']")
			);
			
	 	}
	 
	 	/// <summary> 
		/// 
	 	/// </summary>
		///<param name="doUATPExpressCheckoutPaymentReq"></param>
	 	
	 	public DoUATPExpressCheckoutPaymentResponseType DoUATPExpressCheckoutPayment(DoUATPExpressCheckoutPaymentReq doUATPExpressCheckoutPaymentReq)
	 	{
	 		return DoUATPExpressCheckoutPayment(doUATPExpressCheckoutPaymentReq,(string) null);
	 	}
	 	
	 	/// <summary>
		/// 
	 	/// </summary>
		///<param name="doUATPExpressCheckoutPaymentReq"></param>
		///<param name="credential">An explicit ICredential object that you want to authenticate this call against</param> 
	 	public DoUATPExpressCheckoutPaymentResponseType DoUATPExpressCheckoutPayment(DoUATPExpressCheckoutPaymentReq doUATPExpressCheckoutPaymentReq, ICredential credential)
	 	{	 			 		
			setStandardParams(doUATPExpressCheckoutPaymentReq.DoUATPExpressCheckoutPaymentRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, doUATPExpressCheckoutPaymentReq.ToXMLString(null, "DoUATPExpressCheckoutPaymentReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, credential);
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new DoUATPExpressCheckoutPaymentResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='DoUATPExpressCheckoutPaymentResponse']")
			);
			
	 	}

		/// <summary>
		/// 
	 	/// </summary>
		///<param name="setAuthFlowParamReq"></param>
		///<param name="apiUserName">API Username that you want to authenticate this call against. This username and the corresponding 3-token/certificate credentials must be available in Web.Config/App.Config</param>
	 	public SetAuthFlowParamResponseType SetAuthFlowParam(SetAuthFlowParamReq setAuthFlowParamReq, string apiUserName)
	 	{	 		
			setStandardParams(setAuthFlowParamReq.SetAuthFlowParamRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, setAuthFlowParamReq.ToXMLString(null, "SetAuthFlowParamReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, apiUserName, getAccessToken(), getAccessTokenSecret());
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new SetAuthFlowParamResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='SetAuthFlowParamResponse']")
			);
			
	 	}
	 
	 	/// <summary> 
		/// 
	 	/// </summary>
		///<param name="setAuthFlowParamReq"></param>
	 	
	 	public SetAuthFlowParamResponseType SetAuthFlowParam(SetAuthFlowParamReq setAuthFlowParamReq)
	 	{
	 		return SetAuthFlowParam(setAuthFlowParamReq,(string) null);
	 	}
	 	
	 	/// <summary>
		/// 
	 	/// </summary>
		///<param name="setAuthFlowParamReq"></param>
		///<param name="credential">An explicit ICredential object that you want to authenticate this call against</param> 
	 	public SetAuthFlowParamResponseType SetAuthFlowParam(SetAuthFlowParamReq setAuthFlowParamReq, ICredential credential)
	 	{	 			 		
			setStandardParams(setAuthFlowParamReq.SetAuthFlowParamRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, setAuthFlowParamReq.ToXMLString(null, "SetAuthFlowParamReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, credential);
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new SetAuthFlowParamResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='SetAuthFlowParamResponse']")
			);
			
	 	}

		/// <summary>
		/// 
	 	/// </summary>
		///<param name="getAuthDetailsReq"></param>
		///<param name="apiUserName">API Username that you want to authenticate this call against. This username and the corresponding 3-token/certificate credentials must be available in Web.Config/App.Config</param>
	 	public GetAuthDetailsResponseType GetAuthDetails(GetAuthDetailsReq getAuthDetailsReq, string apiUserName)
	 	{	 		
			setStandardParams(getAuthDetailsReq.GetAuthDetailsRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, getAuthDetailsReq.ToXMLString(null, "GetAuthDetailsReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, apiUserName, getAccessToken(), getAccessTokenSecret());
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new GetAuthDetailsResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='GetAuthDetailsResponse']")
			);
			
	 	}
	 
	 	/// <summary> 
		/// 
	 	/// </summary>
		///<param name="getAuthDetailsReq"></param>
	 	
	 	public GetAuthDetailsResponseType GetAuthDetails(GetAuthDetailsReq getAuthDetailsReq)
	 	{
	 		return GetAuthDetails(getAuthDetailsReq,(string) null);
	 	}
	 	
	 	/// <summary>
		/// 
	 	/// </summary>
		///<param name="getAuthDetailsReq"></param>
		///<param name="credential">An explicit ICredential object that you want to authenticate this call against</param> 
	 	public GetAuthDetailsResponseType GetAuthDetails(GetAuthDetailsReq getAuthDetailsReq, ICredential credential)
	 	{	 			 		
			setStandardParams(getAuthDetailsReq.GetAuthDetailsRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, getAuthDetailsReq.ToXMLString(null, "GetAuthDetailsReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, credential);
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new GetAuthDetailsResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='GetAuthDetailsResponse']")
			);
			
	 	}

		/// <summary>
		/// 
	 	/// </summary>
		///<param name="setAccessPermissionsReq"></param>
		///<param name="apiUserName">API Username that you want to authenticate this call against. This username and the corresponding 3-token/certificate credentials must be available in Web.Config/App.Config</param>
	 	public SetAccessPermissionsResponseType SetAccessPermissions(SetAccessPermissionsReq setAccessPermissionsReq, string apiUserName)
	 	{	 		
			setStandardParams(setAccessPermissionsReq.SetAccessPermissionsRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, setAccessPermissionsReq.ToXMLString(null, "SetAccessPermissionsReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, apiUserName, getAccessToken(), getAccessTokenSecret());
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new SetAccessPermissionsResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='SetAccessPermissionsResponse']")
			);
			
	 	}
	 
	 	/// <summary> 
		/// 
	 	/// </summary>
		///<param name="setAccessPermissionsReq"></param>
	 	
	 	public SetAccessPermissionsResponseType SetAccessPermissions(SetAccessPermissionsReq setAccessPermissionsReq)
	 	{
	 		return SetAccessPermissions(setAccessPermissionsReq,(string) null);
	 	}
	 	
	 	/// <summary>
		/// 
	 	/// </summary>
		///<param name="setAccessPermissionsReq"></param>
		///<param name="credential">An explicit ICredential object that you want to authenticate this call against</param> 
	 	public SetAccessPermissionsResponseType SetAccessPermissions(SetAccessPermissionsReq setAccessPermissionsReq, ICredential credential)
	 	{	 			 		
			setStandardParams(setAccessPermissionsReq.SetAccessPermissionsRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, setAccessPermissionsReq.ToXMLString(null, "SetAccessPermissionsReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, credential);
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new SetAccessPermissionsResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='SetAccessPermissionsResponse']")
			);
			
	 	}

		/// <summary>
		/// 
	 	/// </summary>
		///<param name="updateAccessPermissionsReq"></param>
		///<param name="apiUserName">API Username that you want to authenticate this call against. This username and the corresponding 3-token/certificate credentials must be available in Web.Config/App.Config</param>
	 	public UpdateAccessPermissionsResponseType UpdateAccessPermissions(UpdateAccessPermissionsReq updateAccessPermissionsReq, string apiUserName)
	 	{	 		
			setStandardParams(updateAccessPermissionsReq.UpdateAccessPermissionsRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, updateAccessPermissionsReq.ToXMLString(null, "UpdateAccessPermissionsReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, apiUserName, getAccessToken(), getAccessTokenSecret());
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new UpdateAccessPermissionsResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='UpdateAccessPermissionsResponse']")
			);
			
	 	}
	 
	 	/// <summary> 
		/// 
	 	/// </summary>
		///<param name="updateAccessPermissionsReq"></param>
	 	
	 	public UpdateAccessPermissionsResponseType UpdateAccessPermissions(UpdateAccessPermissionsReq updateAccessPermissionsReq)
	 	{
	 		return UpdateAccessPermissions(updateAccessPermissionsReq,(string) null);
	 	}
	 	
	 	/// <summary>
		/// 
	 	/// </summary>
		///<param name="updateAccessPermissionsReq"></param>
		///<param name="credential">An explicit ICredential object that you want to authenticate this call against</param> 
	 	public UpdateAccessPermissionsResponseType UpdateAccessPermissions(UpdateAccessPermissionsReq updateAccessPermissionsReq, ICredential credential)
	 	{	 			 		
			setStandardParams(updateAccessPermissionsReq.UpdateAccessPermissionsRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, updateAccessPermissionsReq.ToXMLString(null, "UpdateAccessPermissionsReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, credential);
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new UpdateAccessPermissionsResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='UpdateAccessPermissionsResponse']")
			);
			
	 	}

		/// <summary>
		/// 
	 	/// </summary>
		///<param name="getAccessPermissionDetailsReq"></param>
		///<param name="apiUserName">API Username that you want to authenticate this call against. This username and the corresponding 3-token/certificate credentials must be available in Web.Config/App.Config</param>
	 	public GetAccessPermissionDetailsResponseType GetAccessPermissionDetails(GetAccessPermissionDetailsReq getAccessPermissionDetailsReq, string apiUserName)
	 	{	 		
			setStandardParams(getAccessPermissionDetailsReq.GetAccessPermissionDetailsRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, getAccessPermissionDetailsReq.ToXMLString(null, "GetAccessPermissionDetailsReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, apiUserName, getAccessToken(), getAccessTokenSecret());
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new GetAccessPermissionDetailsResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='GetAccessPermissionDetailsResponse']")
			);
			
	 	}
	 
	 	/// <summary> 
		/// 
	 	/// </summary>
		///<param name="getAccessPermissionDetailsReq"></param>
	 	
	 	public GetAccessPermissionDetailsResponseType GetAccessPermissionDetails(GetAccessPermissionDetailsReq getAccessPermissionDetailsReq)
	 	{
	 		return GetAccessPermissionDetails(getAccessPermissionDetailsReq,(string) null);
	 	}
	 	
	 	/// <summary>
		/// 
	 	/// </summary>
		///<param name="getAccessPermissionDetailsReq"></param>
		///<param name="credential">An explicit ICredential object that you want to authenticate this call against</param> 
	 	public GetAccessPermissionDetailsResponseType GetAccessPermissionDetails(GetAccessPermissionDetailsReq getAccessPermissionDetailsReq, ICredential credential)
	 	{	 			 		
			setStandardParams(getAccessPermissionDetailsReq.GetAccessPermissionDetailsRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, getAccessPermissionDetailsReq.ToXMLString(null, "GetAccessPermissionDetailsReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, credential);
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new GetAccessPermissionDetailsResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='GetAccessPermissionDetailsResponse']")
			);
			
	 	}

		/// <summary>
		/// 
	 	/// </summary>
		///<param name="getIncentiveEvaluationReq"></param>
		///<param name="apiUserName">API Username that you want to authenticate this call against. This username and the corresponding 3-token/certificate credentials must be available in Web.Config/App.Config</param>
	 	public GetIncentiveEvaluationResponseType GetIncentiveEvaluation(GetIncentiveEvaluationReq getIncentiveEvaluationReq, string apiUserName)
	 	{	 		
			setStandardParams(getIncentiveEvaluationReq.GetIncentiveEvaluationRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, getIncentiveEvaluationReq.ToXMLString(null, "GetIncentiveEvaluationReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, apiUserName, getAccessToken(), getAccessTokenSecret());
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new GetIncentiveEvaluationResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='GetIncentiveEvaluationResponse']")
			);
			
	 	}
	 
	 	/// <summary> 
		/// 
	 	/// </summary>
		///<param name="getIncentiveEvaluationReq"></param>
	 	
	 	public GetIncentiveEvaluationResponseType GetIncentiveEvaluation(GetIncentiveEvaluationReq getIncentiveEvaluationReq)
	 	{
	 		return GetIncentiveEvaluation(getIncentiveEvaluationReq,(string) null);
	 	}
	 	
	 	/// <summary>
		/// 
	 	/// </summary>
		///<param name="getIncentiveEvaluationReq"></param>
		///<param name="credential">An explicit ICredential object that you want to authenticate this call against</param> 
	 	public GetIncentiveEvaluationResponseType GetIncentiveEvaluation(GetIncentiveEvaluationReq getIncentiveEvaluationReq, ICredential credential)
	 	{	 			 		
			setStandardParams(getIncentiveEvaluationReq.GetIncentiveEvaluationRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, getIncentiveEvaluationReq.ToXMLString(null, "GetIncentiveEvaluationReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, credential);
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new GetIncentiveEvaluationResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='GetIncentiveEvaluationResponse']")
			);
			
	 	}

		/// <summary>
		/// 
	 	/// </summary>
		///<param name="setExpressCheckoutReq"></param>
		///<param name="apiUserName">API Username that you want to authenticate this call against. This username and the corresponding 3-token/certificate credentials must be available in Web.Config/App.Config</param>
	 	public SetExpressCheckoutResponseType SetExpressCheckout(SetExpressCheckoutReq setExpressCheckoutReq, string apiUserName)
	 	{	 		
			setStandardParams(setExpressCheckoutReq.SetExpressCheckoutRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, setExpressCheckoutReq.ToXMLString(null, "SetExpressCheckoutReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, apiUserName, getAccessToken(), getAccessTokenSecret());
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new SetExpressCheckoutResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='SetExpressCheckoutResponse']")
			);
			
	 	}
	 
	 	/// <summary> 
		/// 
	 	/// </summary>
		///<param name="setExpressCheckoutReq"></param>
	 	
	 	public SetExpressCheckoutResponseType SetExpressCheckout(SetExpressCheckoutReq setExpressCheckoutReq)
	 	{
	 		return SetExpressCheckout(setExpressCheckoutReq,(string) null);
	 	}
	 	
	 	/// <summary>
		/// 
	 	/// </summary>
		///<param name="setExpressCheckoutReq"></param>
		///<param name="credential">An explicit ICredential object that you want to authenticate this call against</param> 
	 	public SetExpressCheckoutResponseType SetExpressCheckout(SetExpressCheckoutReq setExpressCheckoutReq, ICredential credential)
	 	{	 			 		
			setStandardParams(setExpressCheckoutReq.SetExpressCheckoutRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, setExpressCheckoutReq.ToXMLString(null, "SetExpressCheckoutReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, credential);
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new SetExpressCheckoutResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='SetExpressCheckoutResponse']")
			);
			
	 	}

		/// <summary>
		/// 
	 	/// </summary>
		///<param name="executeCheckoutOperationsReq"></param>
		///<param name="apiUserName">API Username that you want to authenticate this call against. This username and the corresponding 3-token/certificate credentials must be available in Web.Config/App.Config</param>
	 	public ExecuteCheckoutOperationsResponseType ExecuteCheckoutOperations(ExecuteCheckoutOperationsReq executeCheckoutOperationsReq, string apiUserName)
	 	{	 		
			setStandardParams(executeCheckoutOperationsReq.ExecuteCheckoutOperationsRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, executeCheckoutOperationsReq.ToXMLString(null, "ExecuteCheckoutOperationsReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, apiUserName, getAccessToken(), getAccessTokenSecret());
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new ExecuteCheckoutOperationsResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='ExecuteCheckoutOperationsResponse']")
			);
			
	 	}
	 
	 	/// <summary> 
		/// 
	 	/// </summary>
		///<param name="executeCheckoutOperationsReq"></param>
	 	
	 	public ExecuteCheckoutOperationsResponseType ExecuteCheckoutOperations(ExecuteCheckoutOperationsReq executeCheckoutOperationsReq)
	 	{
	 		return ExecuteCheckoutOperations(executeCheckoutOperationsReq,(string) null);
	 	}
	 	
	 	/// <summary>
		/// 
	 	/// </summary>
		///<param name="executeCheckoutOperationsReq"></param>
		///<param name="credential">An explicit ICredential object that you want to authenticate this call against</param> 
	 	public ExecuteCheckoutOperationsResponseType ExecuteCheckoutOperations(ExecuteCheckoutOperationsReq executeCheckoutOperationsReq, ICredential credential)
	 	{	 			 		
			setStandardParams(executeCheckoutOperationsReq.ExecuteCheckoutOperationsRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, executeCheckoutOperationsReq.ToXMLString(null, "ExecuteCheckoutOperationsReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, credential);
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new ExecuteCheckoutOperationsResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='ExecuteCheckoutOperationsResponse']")
			);
			
	 	}

		/// <summary>
		/// 
	 	/// </summary>
		///<param name="getExpressCheckoutDetailsReq"></param>
		///<param name="apiUserName">API Username that you want to authenticate this call against. This username and the corresponding 3-token/certificate credentials must be available in Web.Config/App.Config</param>
	 	public GetExpressCheckoutDetailsResponseType GetExpressCheckoutDetails(GetExpressCheckoutDetailsReq getExpressCheckoutDetailsReq, string apiUserName)
	 	{	 		
			setStandardParams(getExpressCheckoutDetailsReq.GetExpressCheckoutDetailsRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, getExpressCheckoutDetailsReq.ToXMLString(null, "GetExpressCheckoutDetailsReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, apiUserName, getAccessToken(), getAccessTokenSecret());
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new GetExpressCheckoutDetailsResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='GetExpressCheckoutDetailsResponse']")
			);
			
	 	}
	 
	 	/// <summary> 
		/// 
	 	/// </summary>
		///<param name="getExpressCheckoutDetailsReq"></param>
	 	
	 	public GetExpressCheckoutDetailsResponseType GetExpressCheckoutDetails(GetExpressCheckoutDetailsReq getExpressCheckoutDetailsReq)
	 	{
	 		return GetExpressCheckoutDetails(getExpressCheckoutDetailsReq,(string) null);
	 	}
	 	
	 	/// <summary>
		/// 
	 	/// </summary>
		///<param name="getExpressCheckoutDetailsReq"></param>
		///<param name="credential">An explicit ICredential object that you want to authenticate this call against</param> 
	 	public GetExpressCheckoutDetailsResponseType GetExpressCheckoutDetails(GetExpressCheckoutDetailsReq getExpressCheckoutDetailsReq, ICredential credential)
	 	{	 			 		
			setStandardParams(getExpressCheckoutDetailsReq.GetExpressCheckoutDetailsRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, getExpressCheckoutDetailsReq.ToXMLString(null, "GetExpressCheckoutDetailsReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, credential);
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new GetExpressCheckoutDetailsResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='GetExpressCheckoutDetailsResponse']")
			);
			
	 	}

		/// <summary>
		/// 
	 	/// </summary>
		///<param name="doDirectPaymentReq"></param>
		///<param name="apiUserName">API Username that you want to authenticate this call against. This username and the corresponding 3-token/certificate credentials must be available in Web.Config/App.Config</param>
	 	public DoDirectPaymentResponseType DoDirectPayment(DoDirectPaymentReq doDirectPaymentReq, string apiUserName)
	 	{	 		
			setStandardParams(doDirectPaymentReq.DoDirectPaymentRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, doDirectPaymentReq.ToXMLString(null, "DoDirectPaymentReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, apiUserName, getAccessToken(), getAccessTokenSecret());
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new DoDirectPaymentResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='DoDirectPaymentResponse']")
			);
			
	 	}
	 
	 	/// <summary> 
		/// 
	 	/// </summary>
		///<param name="doDirectPaymentReq"></param>
	 	
	 	public DoDirectPaymentResponseType DoDirectPayment(DoDirectPaymentReq doDirectPaymentReq)
	 	{
	 		return DoDirectPayment(doDirectPaymentReq,(string) null);
	 	}
	 	
	 	/// <summary>
		/// 
	 	/// </summary>
		///<param name="doDirectPaymentReq"></param>
		///<param name="credential">An explicit ICredential object that you want to authenticate this call against</param> 
	 	public DoDirectPaymentResponseType DoDirectPayment(DoDirectPaymentReq doDirectPaymentReq, ICredential credential)
	 	{	 			 		
			setStandardParams(doDirectPaymentReq.DoDirectPaymentRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, doDirectPaymentReq.ToXMLString(null, "DoDirectPaymentReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, credential);
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new DoDirectPaymentResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='DoDirectPaymentResponse']")
			);
			
	 	}

		/// <summary>
		/// 
	 	/// </summary>
		///<param name="managePendingTransactionStatusReq"></param>
		///<param name="apiUserName">API Username that you want to authenticate this call against. This username and the corresponding 3-token/certificate credentials must be available in Web.Config/App.Config</param>
	 	public ManagePendingTransactionStatusResponseType ManagePendingTransactionStatus(ManagePendingTransactionStatusReq managePendingTransactionStatusReq, string apiUserName)
	 	{	 		
			setStandardParams(managePendingTransactionStatusReq.ManagePendingTransactionStatusRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, managePendingTransactionStatusReq.ToXMLString(null, "ManagePendingTransactionStatusReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, apiUserName, getAccessToken(), getAccessTokenSecret());
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new ManagePendingTransactionStatusResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='ManagePendingTransactionStatusResponse']")
			);
			
	 	}
	 
	 	/// <summary> 
		/// 
	 	/// </summary>
		///<param name="managePendingTransactionStatusReq"></param>
	 	
	 	public ManagePendingTransactionStatusResponseType ManagePendingTransactionStatus(ManagePendingTransactionStatusReq managePendingTransactionStatusReq)
	 	{
	 		return ManagePendingTransactionStatus(managePendingTransactionStatusReq,(string) null);
	 	}
	 	
	 	/// <summary>
		/// 
	 	/// </summary>
		///<param name="managePendingTransactionStatusReq"></param>
		///<param name="credential">An explicit ICredential object that you want to authenticate this call against</param> 
	 	public ManagePendingTransactionStatusResponseType ManagePendingTransactionStatus(ManagePendingTransactionStatusReq managePendingTransactionStatusReq, ICredential credential)
	 	{	 			 		
			setStandardParams(managePendingTransactionStatusReq.ManagePendingTransactionStatusRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, managePendingTransactionStatusReq.ToXMLString(null, "ManagePendingTransactionStatusReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, credential);
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new ManagePendingTransactionStatusResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='ManagePendingTransactionStatusResponse']")
			);
			
	 	}

		/// <summary>
		/// 
	 	/// </summary>
		///<param name="doCancelReq"></param>
		///<param name="apiUserName">API Username that you want to authenticate this call against. This username and the corresponding 3-token/certificate credentials must be available in Web.Config/App.Config</param>
	 	public DoCancelResponseType DoCancel(DoCancelReq doCancelReq, string apiUserName)
	 	{	 		
			setStandardParams(doCancelReq.DoCancelRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, doCancelReq.ToXMLString(null, "DoCancelReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, apiUserName, getAccessToken(), getAccessTokenSecret());
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new DoCancelResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='DoCancelResponse']")
			);
			
	 	}
	 
	 	/// <summary> 
		/// 
	 	/// </summary>
		///<param name="doCancelReq"></param>
	 	
	 	public DoCancelResponseType DoCancel(DoCancelReq doCancelReq)
	 	{
	 		return DoCancel(doCancelReq,(string) null);
	 	}
	 	
	 	/// <summary>
		/// 
	 	/// </summary>
		///<param name="doCancelReq"></param>
		///<param name="credential">An explicit ICredential object that you want to authenticate this call against</param> 
	 	public DoCancelResponseType DoCancel(DoCancelReq doCancelReq, ICredential credential)
	 	{	 			 		
			setStandardParams(doCancelReq.DoCancelRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, doCancelReq.ToXMLString(null, "DoCancelReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, credential);
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new DoCancelResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='DoCancelResponse']")
			);
			
	 	}

		/// <summary>
		/// 
	 	/// </summary>
		///<param name="doCaptureReq"></param>
		///<param name="apiUserName">API Username that you want to authenticate this call against. This username and the corresponding 3-token/certificate credentials must be available in Web.Config/App.Config</param>
	 	public DoCaptureResponseType DoCapture(DoCaptureReq doCaptureReq, string apiUserName)
	 	{	 		
			setStandardParams(doCaptureReq.DoCaptureRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, doCaptureReq.ToXMLString(null, "DoCaptureReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, apiUserName, getAccessToken(), getAccessTokenSecret());
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new DoCaptureResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='DoCaptureResponse']")
			);
			
	 	}
	 
	 	/// <summary> 
		/// 
	 	/// </summary>
		///<param name="doCaptureReq"></param>
	 	
	 	public DoCaptureResponseType DoCapture(DoCaptureReq doCaptureReq)
	 	{
	 		return DoCapture(doCaptureReq,(string) null);
	 	}
	 	
	 	/// <summary>
		/// 
	 	/// </summary>
		///<param name="doCaptureReq"></param>
		///<param name="credential">An explicit ICredential object that you want to authenticate this call against</param> 
	 	public DoCaptureResponseType DoCapture(DoCaptureReq doCaptureReq, ICredential credential)
	 	{	 			 		
			setStandardParams(doCaptureReq.DoCaptureRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, doCaptureReq.ToXMLString(null, "DoCaptureReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, credential);
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new DoCaptureResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='DoCaptureResponse']")
			);
			
	 	}

		/// <summary>
		/// 
	 	/// </summary>
		///<param name="doReauthorizationReq"></param>
		///<param name="apiUserName">API Username that you want to authenticate this call against. This username and the corresponding 3-token/certificate credentials must be available in Web.Config/App.Config</param>
	 	public DoReauthorizationResponseType DoReauthorization(DoReauthorizationReq doReauthorizationReq, string apiUserName)
	 	{	 		
			setStandardParams(doReauthorizationReq.DoReauthorizationRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, doReauthorizationReq.ToXMLString(null, "DoReauthorizationReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, apiUserName, getAccessToken(), getAccessTokenSecret());
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new DoReauthorizationResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='DoReauthorizationResponse']")
			);
			
	 	}
	 
	 	/// <summary> 
		/// 
	 	/// </summary>
		///<param name="doReauthorizationReq"></param>
	 	
	 	public DoReauthorizationResponseType DoReauthorization(DoReauthorizationReq doReauthorizationReq)
	 	{
	 		return DoReauthorization(doReauthorizationReq,(string) null);
	 	}
	 	
	 	/// <summary>
		/// 
	 	/// </summary>
		///<param name="doReauthorizationReq"></param>
		///<param name="credential">An explicit ICredential object that you want to authenticate this call against</param> 
	 	public DoReauthorizationResponseType DoReauthorization(DoReauthorizationReq doReauthorizationReq, ICredential credential)
	 	{	 			 		
			setStandardParams(doReauthorizationReq.DoReauthorizationRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, doReauthorizationReq.ToXMLString(null, "DoReauthorizationReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, credential);
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new DoReauthorizationResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='DoReauthorizationResponse']")
			);
			
	 	}

		/// <summary>
		/// 
	 	/// </summary>
		///<param name="doVoidReq"></param>
		///<param name="apiUserName">API Username that you want to authenticate this call against. This username and the corresponding 3-token/certificate credentials must be available in Web.Config/App.Config</param>
	 	public DoVoidResponseType DoVoid(DoVoidReq doVoidReq, string apiUserName)
	 	{	 		
			setStandardParams(doVoidReq.DoVoidRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, doVoidReq.ToXMLString(null, "DoVoidReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, apiUserName, getAccessToken(), getAccessTokenSecret());
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new DoVoidResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='DoVoidResponse']")
			);
			
	 	}
	 
	 	/// <summary> 
		/// 
	 	/// </summary>
		///<param name="doVoidReq"></param>
	 	
	 	public DoVoidResponseType DoVoid(DoVoidReq doVoidReq)
	 	{
	 		return DoVoid(doVoidReq,(string) null);
	 	}
	 	
	 	/// <summary>
		/// 
	 	/// </summary>
		///<param name="doVoidReq"></param>
		///<param name="credential">An explicit ICredential object that you want to authenticate this call against</param> 
	 	public DoVoidResponseType DoVoid(DoVoidReq doVoidReq, ICredential credential)
	 	{	 			 		
			setStandardParams(doVoidReq.DoVoidRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, doVoidReq.ToXMLString(null, "DoVoidReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, credential);
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new DoVoidResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='DoVoidResponse']")
			);
			
	 	}

		/// <summary>
		/// 
	 	/// </summary>
		///<param name="doAuthorizationReq"></param>
		///<param name="apiUserName">API Username that you want to authenticate this call against. This username and the corresponding 3-token/certificate credentials must be available in Web.Config/App.Config</param>
	 	public DoAuthorizationResponseType DoAuthorization(DoAuthorizationReq doAuthorizationReq, string apiUserName)
	 	{	 		
			setStandardParams(doAuthorizationReq.DoAuthorizationRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, doAuthorizationReq.ToXMLString(null, "DoAuthorizationReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, apiUserName, getAccessToken(), getAccessTokenSecret());
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new DoAuthorizationResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='DoAuthorizationResponse']")
			);
			
	 	}
	 
	 	/// <summary> 
		/// 
	 	/// </summary>
		///<param name="doAuthorizationReq"></param>
	 	
	 	public DoAuthorizationResponseType DoAuthorization(DoAuthorizationReq doAuthorizationReq)
	 	{
	 		return DoAuthorization(doAuthorizationReq,(string) null);
	 	}
	 	
	 	/// <summary>
		/// 
	 	/// </summary>
		///<param name="doAuthorizationReq"></param>
		///<param name="credential">An explicit ICredential object that you want to authenticate this call against</param> 
	 	public DoAuthorizationResponseType DoAuthorization(DoAuthorizationReq doAuthorizationReq, ICredential credential)
	 	{	 			 		
			setStandardParams(doAuthorizationReq.DoAuthorizationRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, doAuthorizationReq.ToXMLString(null, "DoAuthorizationReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, credential);
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new DoAuthorizationResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='DoAuthorizationResponse']")
			);
			
	 	}

		/// <summary>
		/// 
	 	/// </summary>
		///<param name="updateAuthorizationReq"></param>
		///<param name="apiUserName">API Username that you want to authenticate this call against. This username and the corresponding 3-token/certificate credentials must be available in Web.Config/App.Config</param>
	 	public UpdateAuthorizationResponseType UpdateAuthorization(UpdateAuthorizationReq updateAuthorizationReq, string apiUserName)
	 	{	 		
			setStandardParams(updateAuthorizationReq.UpdateAuthorizationRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, updateAuthorizationReq.ToXMLString(null, "UpdateAuthorizationReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, apiUserName, getAccessToken(), getAccessTokenSecret());
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new UpdateAuthorizationResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='UpdateAuthorizationResponse']")
			);
			
	 	}
	 
	 	/// <summary> 
		/// 
	 	/// </summary>
		///<param name="updateAuthorizationReq"></param>
	 	
	 	public UpdateAuthorizationResponseType UpdateAuthorization(UpdateAuthorizationReq updateAuthorizationReq)
	 	{
	 		return UpdateAuthorization(updateAuthorizationReq,(string) null);
	 	}
	 	
	 	/// <summary>
		/// 
	 	/// </summary>
		///<param name="updateAuthorizationReq"></param>
		///<param name="credential">An explicit ICredential object that you want to authenticate this call against</param> 
	 	public UpdateAuthorizationResponseType UpdateAuthorization(UpdateAuthorizationReq updateAuthorizationReq, ICredential credential)
	 	{	 			 		
			setStandardParams(updateAuthorizationReq.UpdateAuthorizationRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, updateAuthorizationReq.ToXMLString(null, "UpdateAuthorizationReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, credential);
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new UpdateAuthorizationResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='UpdateAuthorizationResponse']")
			);
			
	 	}

		/// <summary>
		/// 
	 	/// </summary>
		///<param name="setCustomerBillingAgreementReq"></param>
		///<param name="apiUserName">API Username that you want to authenticate this call against. This username and the corresponding 3-token/certificate credentials must be available in Web.Config/App.Config</param>
	 	public SetCustomerBillingAgreementResponseType SetCustomerBillingAgreement(SetCustomerBillingAgreementReq setCustomerBillingAgreementReq, string apiUserName)
	 	{	 		
			setStandardParams(setCustomerBillingAgreementReq.SetCustomerBillingAgreementRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, setCustomerBillingAgreementReq.ToXMLString(null, "SetCustomerBillingAgreementReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, apiUserName, getAccessToken(), getAccessTokenSecret());
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new SetCustomerBillingAgreementResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='SetCustomerBillingAgreementResponse']")
			);
			
	 	}
	 
	 	/// <summary> 
		/// 
	 	/// </summary>
		///<param name="setCustomerBillingAgreementReq"></param>
	 	
	 	public SetCustomerBillingAgreementResponseType SetCustomerBillingAgreement(SetCustomerBillingAgreementReq setCustomerBillingAgreementReq)
	 	{
	 		return SetCustomerBillingAgreement(setCustomerBillingAgreementReq,(string) null);
	 	}
	 	
	 	/// <summary>
		/// 
	 	/// </summary>
		///<param name="setCustomerBillingAgreementReq"></param>
		///<param name="credential">An explicit ICredential object that you want to authenticate this call against</param> 
	 	public SetCustomerBillingAgreementResponseType SetCustomerBillingAgreement(SetCustomerBillingAgreementReq setCustomerBillingAgreementReq, ICredential credential)
	 	{	 			 		
			setStandardParams(setCustomerBillingAgreementReq.SetCustomerBillingAgreementRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, setCustomerBillingAgreementReq.ToXMLString(null, "SetCustomerBillingAgreementReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, credential);
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new SetCustomerBillingAgreementResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='SetCustomerBillingAgreementResponse']")
			);
			
	 	}

		/// <summary>
		/// 
	 	/// </summary>
		///<param name="getBillingAgreementCustomerDetailsReq"></param>
		///<param name="apiUserName">API Username that you want to authenticate this call against. This username and the corresponding 3-token/certificate credentials must be available in Web.Config/App.Config</param>
	 	public GetBillingAgreementCustomerDetailsResponseType GetBillingAgreementCustomerDetails(GetBillingAgreementCustomerDetailsReq getBillingAgreementCustomerDetailsReq, string apiUserName)
	 	{	 		
			setStandardParams(getBillingAgreementCustomerDetailsReq.GetBillingAgreementCustomerDetailsRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, getBillingAgreementCustomerDetailsReq.ToXMLString(null, "GetBillingAgreementCustomerDetailsReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, apiUserName, getAccessToken(), getAccessTokenSecret());
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new GetBillingAgreementCustomerDetailsResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='GetBillingAgreementCustomerDetailsResponse']")
			);
			
	 	}
	 
	 	/// <summary> 
		/// 
	 	/// </summary>
		///<param name="getBillingAgreementCustomerDetailsReq"></param>
	 	
	 	public GetBillingAgreementCustomerDetailsResponseType GetBillingAgreementCustomerDetails(GetBillingAgreementCustomerDetailsReq getBillingAgreementCustomerDetailsReq)
	 	{
	 		return GetBillingAgreementCustomerDetails(getBillingAgreementCustomerDetailsReq,(string) null);
	 	}
	 	
	 	/// <summary>
		/// 
	 	/// </summary>
		///<param name="getBillingAgreementCustomerDetailsReq"></param>
		///<param name="credential">An explicit ICredential object that you want to authenticate this call against</param> 
	 	public GetBillingAgreementCustomerDetailsResponseType GetBillingAgreementCustomerDetails(GetBillingAgreementCustomerDetailsReq getBillingAgreementCustomerDetailsReq, ICredential credential)
	 	{	 			 		
			setStandardParams(getBillingAgreementCustomerDetailsReq.GetBillingAgreementCustomerDetailsRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, getBillingAgreementCustomerDetailsReq.ToXMLString(null, "GetBillingAgreementCustomerDetailsReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, credential);
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new GetBillingAgreementCustomerDetailsResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='GetBillingAgreementCustomerDetailsResponse']")
			);
			
	 	}

		/// <summary>
		/// 
	 	/// </summary>
		///<param name="createBillingAgreementReq"></param>
		///<param name="apiUserName">API Username that you want to authenticate this call against. This username and the corresponding 3-token/certificate credentials must be available in Web.Config/App.Config</param>
	 	public CreateBillingAgreementResponseType CreateBillingAgreement(CreateBillingAgreementReq createBillingAgreementReq, string apiUserName)
	 	{	 		
			setStandardParams(createBillingAgreementReq.CreateBillingAgreementRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, createBillingAgreementReq.ToXMLString(null, "CreateBillingAgreementReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, apiUserName, getAccessToken(), getAccessTokenSecret());
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new CreateBillingAgreementResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='CreateBillingAgreementResponse']")
			);
			
	 	}
	 
	 	/// <summary> 
		/// 
	 	/// </summary>
		///<param name="createBillingAgreementReq"></param>
	 	
	 	public CreateBillingAgreementResponseType CreateBillingAgreement(CreateBillingAgreementReq createBillingAgreementReq)
	 	{
	 		return CreateBillingAgreement(createBillingAgreementReq,(string) null);
	 	}
	 	
	 	/// <summary>
		/// 
	 	/// </summary>
		///<param name="createBillingAgreementReq"></param>
		///<param name="credential">An explicit ICredential object that you want to authenticate this call against</param> 
	 	public CreateBillingAgreementResponseType CreateBillingAgreement(CreateBillingAgreementReq createBillingAgreementReq, ICredential credential)
	 	{	 			 		
			setStandardParams(createBillingAgreementReq.CreateBillingAgreementRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, createBillingAgreementReq.ToXMLString(null, "CreateBillingAgreementReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, credential);
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new CreateBillingAgreementResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='CreateBillingAgreementResponse']")
			);
			
	 	}

		/// <summary>
		/// 
	 	/// </summary>
		///<param name="doReferenceTransactionReq"></param>
		///<param name="apiUserName">API Username that you want to authenticate this call against. This username and the corresponding 3-token/certificate credentials must be available in Web.Config/App.Config</param>
	 	public DoReferenceTransactionResponseType DoReferenceTransaction(DoReferenceTransactionReq doReferenceTransactionReq, string apiUserName)
	 	{	 		
			setStandardParams(doReferenceTransactionReq.DoReferenceTransactionRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, doReferenceTransactionReq.ToXMLString(null, "DoReferenceTransactionReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, apiUserName, getAccessToken(), getAccessTokenSecret());
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new DoReferenceTransactionResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='DoReferenceTransactionResponse']")
			);
			
	 	}
	 
	 	/// <summary> 
		/// 
	 	/// </summary>
		///<param name="doReferenceTransactionReq"></param>
	 	
	 	public DoReferenceTransactionResponseType DoReferenceTransaction(DoReferenceTransactionReq doReferenceTransactionReq)
	 	{
	 		return DoReferenceTransaction(doReferenceTransactionReq,(string) null);
	 	}
	 	
	 	/// <summary>
		/// 
	 	/// </summary>
		///<param name="doReferenceTransactionReq"></param>
		///<param name="credential">An explicit ICredential object that you want to authenticate this call against</param> 
	 	public DoReferenceTransactionResponseType DoReferenceTransaction(DoReferenceTransactionReq doReferenceTransactionReq, ICredential credential)
	 	{	 			 		
			setStandardParams(doReferenceTransactionReq.DoReferenceTransactionRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, doReferenceTransactionReq.ToXMLString(null, "DoReferenceTransactionReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, credential);
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new DoReferenceTransactionResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='DoReferenceTransactionResponse']")
			);
			
	 	}

		/// <summary>
		/// 
	 	/// </summary>
		///<param name="doNonReferencedCreditReq"></param>
		///<param name="apiUserName">API Username that you want to authenticate this call against. This username and the corresponding 3-token/certificate credentials must be available in Web.Config/App.Config</param>
	 	public DoNonReferencedCreditResponseType DoNonReferencedCredit(DoNonReferencedCreditReq doNonReferencedCreditReq, string apiUserName)
	 	{	 		
			setStandardParams(doNonReferencedCreditReq.DoNonReferencedCreditRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, doNonReferencedCreditReq.ToXMLString(null, "DoNonReferencedCreditReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, apiUserName, getAccessToken(), getAccessTokenSecret());
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new DoNonReferencedCreditResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='DoNonReferencedCreditResponse']")
			);
			
	 	}
	 
	 	/// <summary> 
		/// 
	 	/// </summary>
		///<param name="doNonReferencedCreditReq"></param>
	 	
	 	public DoNonReferencedCreditResponseType DoNonReferencedCredit(DoNonReferencedCreditReq doNonReferencedCreditReq)
	 	{
	 		return DoNonReferencedCredit(doNonReferencedCreditReq,(string) null);
	 	}
	 	
	 	/// <summary>
		/// 
	 	/// </summary>
		///<param name="doNonReferencedCreditReq"></param>
		///<param name="credential">An explicit ICredential object that you want to authenticate this call against</param> 
	 	public DoNonReferencedCreditResponseType DoNonReferencedCredit(DoNonReferencedCreditReq doNonReferencedCreditReq, ICredential credential)
	 	{	 			 		
			setStandardParams(doNonReferencedCreditReq.DoNonReferencedCreditRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, doNonReferencedCreditReq.ToXMLString(null, "DoNonReferencedCreditReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, credential);
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new DoNonReferencedCreditResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='DoNonReferencedCreditResponse']")
			);
			
	 	}

		/// <summary>
		/// 
	 	/// </summary>
		///<param name="doUATPAuthorizationReq"></param>
		///<param name="apiUserName">API Username that you want to authenticate this call against. This username and the corresponding 3-token/certificate credentials must be available in Web.Config/App.Config</param>
	 	public DoUATPAuthorizationResponseType DoUATPAuthorization(DoUATPAuthorizationReq doUATPAuthorizationReq, string apiUserName)
	 	{	 		
			setStandardParams(doUATPAuthorizationReq.DoUATPAuthorizationRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, doUATPAuthorizationReq.ToXMLString(null, "DoUATPAuthorizationReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, apiUserName, getAccessToken(), getAccessTokenSecret());
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new DoUATPAuthorizationResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='DoUATPAuthorizationResponse']")
			);
			
	 	}
	 
	 	/// <summary> 
		/// 
	 	/// </summary>
		///<param name="doUATPAuthorizationReq"></param>
	 	
	 	public DoUATPAuthorizationResponseType DoUATPAuthorization(DoUATPAuthorizationReq doUATPAuthorizationReq)
	 	{
	 		return DoUATPAuthorization(doUATPAuthorizationReq,(string) null);
	 	}
	 	
	 	/// <summary>
		/// 
	 	/// </summary>
		///<param name="doUATPAuthorizationReq"></param>
		///<param name="credential">An explicit ICredential object that you want to authenticate this call against</param> 
	 	public DoUATPAuthorizationResponseType DoUATPAuthorization(DoUATPAuthorizationReq doUATPAuthorizationReq, ICredential credential)
	 	{	 			 		
			setStandardParams(doUATPAuthorizationReq.DoUATPAuthorizationRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, doUATPAuthorizationReq.ToXMLString(null, "DoUATPAuthorizationReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, credential);
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new DoUATPAuthorizationResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='DoUATPAuthorizationResponse']")
			);
			
	 	}

		/// <summary>
		/// 
	 	/// </summary>
		///<param name="createRecurringPaymentsProfileReq"></param>
		///<param name="apiUserName">API Username that you want to authenticate this call against. This username and the corresponding 3-token/certificate credentials must be available in Web.Config/App.Config</param>
	 	public CreateRecurringPaymentsProfileResponseType CreateRecurringPaymentsProfile(CreateRecurringPaymentsProfileReq createRecurringPaymentsProfileReq, string apiUserName)
	 	{	 		
			setStandardParams(createRecurringPaymentsProfileReq.CreateRecurringPaymentsProfileRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, createRecurringPaymentsProfileReq.ToXMLString(null, "CreateRecurringPaymentsProfileReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, apiUserName, getAccessToken(), getAccessTokenSecret());
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new CreateRecurringPaymentsProfileResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='CreateRecurringPaymentsProfileResponse']")
			);
			
	 	}
	 
	 	/// <summary> 
		/// 
	 	/// </summary>
		///<param name="createRecurringPaymentsProfileReq"></param>
	 	
	 	public CreateRecurringPaymentsProfileResponseType CreateRecurringPaymentsProfile(CreateRecurringPaymentsProfileReq createRecurringPaymentsProfileReq)
	 	{
	 		return CreateRecurringPaymentsProfile(createRecurringPaymentsProfileReq,(string) null);
	 	}
	 	
	 	/// <summary>
		/// 
	 	/// </summary>
		///<param name="createRecurringPaymentsProfileReq"></param>
		///<param name="credential">An explicit ICredential object that you want to authenticate this call against</param> 
	 	public CreateRecurringPaymentsProfileResponseType CreateRecurringPaymentsProfile(CreateRecurringPaymentsProfileReq createRecurringPaymentsProfileReq, ICredential credential)
	 	{	 			 		
			setStandardParams(createRecurringPaymentsProfileReq.CreateRecurringPaymentsProfileRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, createRecurringPaymentsProfileReq.ToXMLString(null, "CreateRecurringPaymentsProfileReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, credential);
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new CreateRecurringPaymentsProfileResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='CreateRecurringPaymentsProfileResponse']")
			);
			
	 	}

		/// <summary>
		/// 
	 	/// </summary>
		///<param name="getRecurringPaymentsProfileDetailsReq"></param>
		///<param name="apiUserName">API Username that you want to authenticate this call against. This username and the corresponding 3-token/certificate credentials must be available in Web.Config/App.Config</param>
	 	public GetRecurringPaymentsProfileDetailsResponseType GetRecurringPaymentsProfileDetails(GetRecurringPaymentsProfileDetailsReq getRecurringPaymentsProfileDetailsReq, string apiUserName)
	 	{	 		
			setStandardParams(getRecurringPaymentsProfileDetailsReq.GetRecurringPaymentsProfileDetailsRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, getRecurringPaymentsProfileDetailsReq.ToXMLString(null, "GetRecurringPaymentsProfileDetailsReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, apiUserName, getAccessToken(), getAccessTokenSecret());
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new GetRecurringPaymentsProfileDetailsResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='GetRecurringPaymentsProfileDetailsResponse']")
			);
			
	 	}
	 
	 	/// <summary> 
		/// 
	 	/// </summary>
		///<param name="getRecurringPaymentsProfileDetailsReq"></param>
	 	
	 	public GetRecurringPaymentsProfileDetailsResponseType GetRecurringPaymentsProfileDetails(GetRecurringPaymentsProfileDetailsReq getRecurringPaymentsProfileDetailsReq)
	 	{
	 		return GetRecurringPaymentsProfileDetails(getRecurringPaymentsProfileDetailsReq,(string) null);
	 	}
	 	
	 	/// <summary>
		/// 
	 	/// </summary>
		///<param name="getRecurringPaymentsProfileDetailsReq"></param>
		///<param name="credential">An explicit ICredential object that you want to authenticate this call against</param> 
	 	public GetRecurringPaymentsProfileDetailsResponseType GetRecurringPaymentsProfileDetails(GetRecurringPaymentsProfileDetailsReq getRecurringPaymentsProfileDetailsReq, ICredential credential)
	 	{	 			 		
			setStandardParams(getRecurringPaymentsProfileDetailsReq.GetRecurringPaymentsProfileDetailsRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, getRecurringPaymentsProfileDetailsReq.ToXMLString(null, "GetRecurringPaymentsProfileDetailsReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, credential);
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new GetRecurringPaymentsProfileDetailsResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='GetRecurringPaymentsProfileDetailsResponse']")
			);
			
	 	}

		/// <summary>
		/// 
	 	/// </summary>
		///<param name="manageRecurringPaymentsProfileStatusReq"></param>
		///<param name="apiUserName">API Username that you want to authenticate this call against. This username and the corresponding 3-token/certificate credentials must be available in Web.Config/App.Config</param>
	 	public ManageRecurringPaymentsProfileStatusResponseType ManageRecurringPaymentsProfileStatus(ManageRecurringPaymentsProfileStatusReq manageRecurringPaymentsProfileStatusReq, string apiUserName)
	 	{	 		
			setStandardParams(manageRecurringPaymentsProfileStatusReq.ManageRecurringPaymentsProfileStatusRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, manageRecurringPaymentsProfileStatusReq.ToXMLString(null, "ManageRecurringPaymentsProfileStatusReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, apiUserName, getAccessToken(), getAccessTokenSecret());
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new ManageRecurringPaymentsProfileStatusResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='ManageRecurringPaymentsProfileStatusResponse']")
			);
			
	 	}
	 
	 	/// <summary> 
		/// 
	 	/// </summary>
		///<param name="manageRecurringPaymentsProfileStatusReq"></param>
	 	
	 	public ManageRecurringPaymentsProfileStatusResponseType ManageRecurringPaymentsProfileStatus(ManageRecurringPaymentsProfileStatusReq manageRecurringPaymentsProfileStatusReq)
	 	{
	 		return ManageRecurringPaymentsProfileStatus(manageRecurringPaymentsProfileStatusReq,(string) null);
	 	}
	 	
	 	/// <summary>
		/// 
	 	/// </summary>
		///<param name="manageRecurringPaymentsProfileStatusReq"></param>
		///<param name="credential">An explicit ICredential object that you want to authenticate this call against</param> 
	 	public ManageRecurringPaymentsProfileStatusResponseType ManageRecurringPaymentsProfileStatus(ManageRecurringPaymentsProfileStatusReq manageRecurringPaymentsProfileStatusReq, ICredential credential)
	 	{	 			 		
			setStandardParams(manageRecurringPaymentsProfileStatusReq.ManageRecurringPaymentsProfileStatusRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, manageRecurringPaymentsProfileStatusReq.ToXMLString(null, "ManageRecurringPaymentsProfileStatusReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, credential);
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new ManageRecurringPaymentsProfileStatusResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='ManageRecurringPaymentsProfileStatusResponse']")
			);
			
	 	}

		/// <summary>
		/// 
	 	/// </summary>
		///<param name="billOutstandingAmountReq"></param>
		///<param name="apiUserName">API Username that you want to authenticate this call against. This username and the corresponding 3-token/certificate credentials must be available in Web.Config/App.Config</param>
	 	public BillOutstandingAmountResponseType BillOutstandingAmount(BillOutstandingAmountReq billOutstandingAmountReq, string apiUserName)
	 	{	 		
			setStandardParams(billOutstandingAmountReq.BillOutstandingAmountRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, billOutstandingAmountReq.ToXMLString(null, "BillOutstandingAmountReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, apiUserName, getAccessToken(), getAccessTokenSecret());
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new BillOutstandingAmountResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='BillOutstandingAmountResponse']")
			);
			
	 	}
	 
	 	/// <summary> 
		/// 
	 	/// </summary>
		///<param name="billOutstandingAmountReq"></param>
	 	
	 	public BillOutstandingAmountResponseType BillOutstandingAmount(BillOutstandingAmountReq billOutstandingAmountReq)
	 	{
	 		return BillOutstandingAmount(billOutstandingAmountReq,(string) null);
	 	}
	 	
	 	/// <summary>
		/// 
	 	/// </summary>
		///<param name="billOutstandingAmountReq"></param>
		///<param name="credential">An explicit ICredential object that you want to authenticate this call against</param> 
	 	public BillOutstandingAmountResponseType BillOutstandingAmount(BillOutstandingAmountReq billOutstandingAmountReq, ICredential credential)
	 	{	 			 		
			setStandardParams(billOutstandingAmountReq.BillOutstandingAmountRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, billOutstandingAmountReq.ToXMLString(null, "BillOutstandingAmountReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, credential);
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new BillOutstandingAmountResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='BillOutstandingAmountResponse']")
			);
			
	 	}

		/// <summary>
		/// 
	 	/// </summary>
		///<param name="updateRecurringPaymentsProfileReq"></param>
		///<param name="apiUserName">API Username that you want to authenticate this call against. This username and the corresponding 3-token/certificate credentials must be available in Web.Config/App.Config</param>
	 	public UpdateRecurringPaymentsProfileResponseType UpdateRecurringPaymentsProfile(UpdateRecurringPaymentsProfileReq updateRecurringPaymentsProfileReq, string apiUserName)
	 	{	 		
			setStandardParams(updateRecurringPaymentsProfileReq.UpdateRecurringPaymentsProfileRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, updateRecurringPaymentsProfileReq.ToXMLString(null, "UpdateRecurringPaymentsProfileReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, apiUserName, getAccessToken(), getAccessTokenSecret());
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new UpdateRecurringPaymentsProfileResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='UpdateRecurringPaymentsProfileResponse']")
			);
			
	 	}
	 
	 	/// <summary> 
		/// 
	 	/// </summary>
		///<param name="updateRecurringPaymentsProfileReq"></param>
	 	
	 	public UpdateRecurringPaymentsProfileResponseType UpdateRecurringPaymentsProfile(UpdateRecurringPaymentsProfileReq updateRecurringPaymentsProfileReq)
	 	{
	 		return UpdateRecurringPaymentsProfile(updateRecurringPaymentsProfileReq,(string) null);
	 	}
	 	
	 	/// <summary>
		/// 
	 	/// </summary>
		///<param name="updateRecurringPaymentsProfileReq"></param>
		///<param name="credential">An explicit ICredential object that you want to authenticate this call against</param> 
	 	public UpdateRecurringPaymentsProfileResponseType UpdateRecurringPaymentsProfile(UpdateRecurringPaymentsProfileReq updateRecurringPaymentsProfileReq, ICredential credential)
	 	{	 			 		
			setStandardParams(updateRecurringPaymentsProfileReq.UpdateRecurringPaymentsProfileRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, updateRecurringPaymentsProfileReq.ToXMLString(null, "UpdateRecurringPaymentsProfileReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, credential);
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new UpdateRecurringPaymentsProfileResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='UpdateRecurringPaymentsProfileResponse']")
			);
			
	 	}

		/// <summary>
		/// 
	 	/// </summary>
		///<param name="reverseTransactionReq"></param>
		///<param name="apiUserName">API Username that you want to authenticate this call against. This username and the corresponding 3-token/certificate credentials must be available in Web.Config/App.Config</param>
	 	public ReverseTransactionResponseType ReverseTransaction(ReverseTransactionReq reverseTransactionReq, string apiUserName)
	 	{	 		
			setStandardParams(reverseTransactionReq.ReverseTransactionRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, reverseTransactionReq.ToXMLString(null, "ReverseTransactionReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, apiUserName, getAccessToken(), getAccessTokenSecret());
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new ReverseTransactionResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='ReverseTransactionResponse']")
			);
			
	 	}
	 
	 	/// <summary> 
		/// 
	 	/// </summary>
		///<param name="reverseTransactionReq"></param>
	 	
	 	public ReverseTransactionResponseType ReverseTransaction(ReverseTransactionReq reverseTransactionReq)
	 	{
	 		return ReverseTransaction(reverseTransactionReq,(string) null);
	 	}
	 	
	 	/// <summary>
		/// 
	 	/// </summary>
		///<param name="reverseTransactionReq"></param>
		///<param name="credential">An explicit ICredential object that you want to authenticate this call against</param> 
	 	public ReverseTransactionResponseType ReverseTransaction(ReverseTransactionReq reverseTransactionReq, ICredential credential)
	 	{	 			 		
			setStandardParams(reverseTransactionReq.ReverseTransactionRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, reverseTransactionReq.ToXMLString(null, "ReverseTransactionReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, credential);
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new ReverseTransactionResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='ReverseTransactionResponse']")
			);
			
	 	}

		/// <summary>
		/// 
	 	/// </summary>
		///<param name="externalRememberMeOptOutReq"></param>
		///<param name="apiUserName">API Username that you want to authenticate this call against. This username and the corresponding 3-token/certificate credentials must be available in Web.Config/App.Config</param>
	 	public ExternalRememberMeOptOutResponseType ExternalRememberMeOptOut(ExternalRememberMeOptOutReq externalRememberMeOptOutReq, string apiUserName)
	 	{	 		
			setStandardParams(externalRememberMeOptOutReq.ExternalRememberMeOptOutRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, externalRememberMeOptOutReq.ToXMLString(null, "ExternalRememberMeOptOutReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, apiUserName, getAccessToken(), getAccessTokenSecret());
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new ExternalRememberMeOptOutResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='ExternalRememberMeOptOutResponse']")
			);
			
	 	}
	 
	 	/// <summary> 
		/// 
	 	/// </summary>
		///<param name="externalRememberMeOptOutReq"></param>
	 	
	 	public ExternalRememberMeOptOutResponseType ExternalRememberMeOptOut(ExternalRememberMeOptOutReq externalRememberMeOptOutReq)
	 	{
	 		return ExternalRememberMeOptOut(externalRememberMeOptOutReq,(string) null);
	 	}
	 	
	 	/// <summary>
		/// 
	 	/// </summary>
		///<param name="externalRememberMeOptOutReq"></param>
		///<param name="credential">An explicit ICredential object that you want to authenticate this call against</param> 
	 	public ExternalRememberMeOptOutResponseType ExternalRememberMeOptOut(ExternalRememberMeOptOutReq externalRememberMeOptOutReq, ICredential credential)
	 	{	 			 		
			setStandardParams(externalRememberMeOptOutReq.ExternalRememberMeOptOutRequest);
			DefaultSOAPAPICallHandler defaultHandler = new DefaultSOAPAPICallHandler(this.config, externalRememberMeOptOutReq.ToXMLString(null, "ExternalRememberMeOptOutReq"), null, null);
			IAPICallPreHandler apiCallPreHandler = new MerchantAPICallPreHandler(this.config, defaultHandler, credential);
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKName = SDKName;
			((MerchantAPICallPreHandler) apiCallPreHandler).SDKVersion = SDKVersion;
			((MerchantAPICallPreHandler) apiCallPreHandler).PortName = "PayPalAPIAA";
			
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(Call(apiCallPreHandler));			
			return new ExternalRememberMeOptOutResponseType(
				xmlDocument.SelectSingleNode("*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='ExternalRememberMeOptOutResponse']")
			);
			
	 	}
	}
}
