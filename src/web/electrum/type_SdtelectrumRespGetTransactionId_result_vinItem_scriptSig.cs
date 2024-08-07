/*
				   File: type_SdtelectrumRespGetTransactionId_result_vinItem_scriptSig
			Description: scriptSig
				 Author: Nemo 🐠 for C# (.NET) version 18.0.8.180599
		   Program type: Callable routine
			  Main DBMS: 
*/
using System;
using System.Collections;
using GeneXus.Utils;
using GeneXus.Resources;
using GeneXus.Application;
using GeneXus.Metadata;
using GeneXus.Cryptography;
using GeneXus.Encryption;
using GeneXus.Http.Client;
using GeneXus.Http.Server;
using System.Reflection;
using System.Xml.Serialization;
using System.Runtime.Serialization;

using GeneXus.Programs;
namespace GeneXus.Programs.electrum
{
	[XmlRoot(ElementName="electrumRespGetTransactionId.result.vinItem.scriptSig")]
	[XmlType(TypeName="electrumRespGetTransactionId.result.vinItem.scriptSig" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtelectrumRespGetTransactionId_result_vinItem_scriptSig : GxUserType
	{
		public SdtelectrumRespGetTransactionId_result_vinItem_scriptSig( )
		{
			/* Constructor for serialization */
			gxTv_SdtelectrumRespGetTransactionId_result_vinItem_scriptSig_Asm = "";

			gxTv_SdtelectrumRespGetTransactionId_result_vinItem_scriptSig_Hex = "";

		}

		public SdtelectrumRespGetTransactionId_result_vinItem_scriptSig(IGxContext context)
		{
			this.context = context;	
			initialize();
		}

		#region Json
		private static Hashtable mapper;
		public override string JsonMap(string value)
		{
			if (mapper == null)
			{
				mapper = new Hashtable();
			}
			return (string)mapper[value]; ;
		}

		public override void ToJSON()
		{
			ToJSON(true) ;
			return;
		}

		public override void ToJSON(bool includeState)
		{
			AddObjectProperty("asm", gxTpr_Asm, false);


			AddObjectProperty("hex", gxTpr_Hex, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="asm")]
		[XmlElement(ElementName="asm")]
		public string gxTpr_Asm
		{
			get {
				return gxTv_SdtelectrumRespGetTransactionId_result_vinItem_scriptSig_Asm; 
			}
			set {
				gxTv_SdtelectrumRespGetTransactionId_result_vinItem_scriptSig_Asm = value;
				SetDirty("Asm");
			}
		}




		[SoapElement(ElementName="hex")]
		[XmlElement(ElementName="hex")]
		public string gxTpr_Hex
		{
			get {
				return gxTv_SdtelectrumRespGetTransactionId_result_vinItem_scriptSig_Hex; 
			}
			set {
				gxTv_SdtelectrumRespGetTransactionId_result_vinItem_scriptSig_Hex = value;
				SetDirty("Hex");
			}
		}



		public override bool ShouldSerializeSdtJson()
		{
			return true;
		}



		#endregion

		#region Initialization

		public void initialize( )
		{
			gxTv_SdtelectrumRespGetTransactionId_result_vinItem_scriptSig_Asm = "";
			gxTv_SdtelectrumRespGetTransactionId_result_vinItem_scriptSig_Hex = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtelectrumRespGetTransactionId_result_vinItem_scriptSig_Asm;
		 

		protected string gxTv_SdtelectrumRespGetTransactionId_result_vinItem_scriptSig_Hex;
		 


		#endregion
	}
	#region Rest interface
	[GxUnWrappedJson()]
	[DataContract(Name=@"electrumRespGetTransactionId.result.vinItem.scriptSig", Namespace="distributedcryptography")]
	public class SdtelectrumRespGetTransactionId_result_vinItem_scriptSig_RESTInterface : GxGenericCollectionItem<SdtelectrumRespGetTransactionId_result_vinItem_scriptSig>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtelectrumRespGetTransactionId_result_vinItem_scriptSig_RESTInterface( ) : base()
		{	
		}

		public SdtelectrumRespGetTransactionId_result_vinItem_scriptSig_RESTInterface( SdtelectrumRespGetTransactionId_result_vinItem_scriptSig psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="asm", Order=0)]
		public  string gxTpr_Asm
		{
			get { 
				return sdt.gxTpr_Asm;

			}
			set { 
				 sdt.gxTpr_Asm = value;
			}
		}

		[DataMember(Name="hex", Order=1)]
		public  string gxTpr_Hex
		{
			get { 
				return sdt.gxTpr_Hex;

			}
			set { 
				 sdt.gxTpr_Hex = value;
			}
		}


		#endregion

		public SdtelectrumRespGetTransactionId_result_vinItem_scriptSig sdt
		{
			get { 
				return (SdtelectrumRespGetTransactionId_result_vinItem_scriptSig)Sdt;
			}
			set { 
				Sdt = value;
			}
		}

		[OnDeserializing]
		void checkSdt( StreamingContext ctx )
		{
			if ( sdt == null )
			{
				sdt = new SdtelectrumRespGetTransactionId_result_vinItem_scriptSig() ;
			}
		}
	}
	#endregion
}