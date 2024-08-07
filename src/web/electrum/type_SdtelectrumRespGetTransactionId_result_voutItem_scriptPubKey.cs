/*
				   File: type_SdtelectrumRespGetTransactionId_result_voutItem_scriptPubKey
			Description: scriptPubKey
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
	[XmlRoot(ElementName="electrumRespGetTransactionId.result.voutItem.scriptPubKey")]
	[XmlType(TypeName="electrumRespGetTransactionId.result.voutItem.scriptPubKey" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtelectrumRespGetTransactionId_result_voutItem_scriptPubKey : GxUserType
	{
		public SdtelectrumRespGetTransactionId_result_voutItem_scriptPubKey( )
		{
			/* Constructor for serialization */
			gxTv_SdtelectrumRespGetTransactionId_result_voutItem_scriptPubKey_Address = "";

			gxTv_SdtelectrumRespGetTransactionId_result_voutItem_scriptPubKey_Asm = "";

			gxTv_SdtelectrumRespGetTransactionId_result_voutItem_scriptPubKey_Desc = "";

			gxTv_SdtelectrumRespGetTransactionId_result_voutItem_scriptPubKey_Hex = "";

			gxTv_SdtelectrumRespGetTransactionId_result_voutItem_scriptPubKey_Type = "";

		}

		public SdtelectrumRespGetTransactionId_result_voutItem_scriptPubKey(IGxContext context)
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
			AddObjectProperty("address", gxTpr_Address, false);


			AddObjectProperty("asm", gxTpr_Asm, false);


			AddObjectProperty("desc", gxTpr_Desc, false);


			AddObjectProperty("hex", gxTpr_Hex, false);


			AddObjectProperty("type", gxTpr_Type, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="address")]
		[XmlElement(ElementName="address")]
		public string gxTpr_Address
		{
			get {
				return gxTv_SdtelectrumRespGetTransactionId_result_voutItem_scriptPubKey_Address; 
			}
			set {
				gxTv_SdtelectrumRespGetTransactionId_result_voutItem_scriptPubKey_Address = value;
				SetDirty("Address");
			}
		}




		[SoapElement(ElementName="asm")]
		[XmlElement(ElementName="asm")]
		public string gxTpr_Asm
		{
			get {
				return gxTv_SdtelectrumRespGetTransactionId_result_voutItem_scriptPubKey_Asm; 
			}
			set {
				gxTv_SdtelectrumRespGetTransactionId_result_voutItem_scriptPubKey_Asm = value;
				SetDirty("Asm");
			}
		}




		[SoapElement(ElementName="desc")]
		[XmlElement(ElementName="desc")]
		public string gxTpr_Desc
		{
			get {
				return gxTv_SdtelectrumRespGetTransactionId_result_voutItem_scriptPubKey_Desc; 
			}
			set {
				gxTv_SdtelectrumRespGetTransactionId_result_voutItem_scriptPubKey_Desc = value;
				SetDirty("Desc");
			}
		}




		[SoapElement(ElementName="hex")]
		[XmlElement(ElementName="hex")]
		public string gxTpr_Hex
		{
			get {
				return gxTv_SdtelectrumRespGetTransactionId_result_voutItem_scriptPubKey_Hex; 
			}
			set {
				gxTv_SdtelectrumRespGetTransactionId_result_voutItem_scriptPubKey_Hex = value;
				SetDirty("Hex");
			}
		}




		[SoapElement(ElementName="type")]
		[XmlElement(ElementName="type")]
		public string gxTpr_Type
		{
			get {
				return gxTv_SdtelectrumRespGetTransactionId_result_voutItem_scriptPubKey_Type; 
			}
			set {
				gxTv_SdtelectrumRespGetTransactionId_result_voutItem_scriptPubKey_Type = value;
				SetDirty("Type");
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
			gxTv_SdtelectrumRespGetTransactionId_result_voutItem_scriptPubKey_Address = "";
			gxTv_SdtelectrumRespGetTransactionId_result_voutItem_scriptPubKey_Asm = "";
			gxTv_SdtelectrumRespGetTransactionId_result_voutItem_scriptPubKey_Desc = "";
			gxTv_SdtelectrumRespGetTransactionId_result_voutItem_scriptPubKey_Hex = "";
			gxTv_SdtelectrumRespGetTransactionId_result_voutItem_scriptPubKey_Type = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtelectrumRespGetTransactionId_result_voutItem_scriptPubKey_Address;
		 

		protected string gxTv_SdtelectrumRespGetTransactionId_result_voutItem_scriptPubKey_Asm;
		 

		protected string gxTv_SdtelectrumRespGetTransactionId_result_voutItem_scriptPubKey_Desc;
		 

		protected string gxTv_SdtelectrumRespGetTransactionId_result_voutItem_scriptPubKey_Hex;
		 

		protected string gxTv_SdtelectrumRespGetTransactionId_result_voutItem_scriptPubKey_Type;
		 


		#endregion
	}
	#region Rest interface
	[GxUnWrappedJson()]
	[DataContract(Name=@"electrumRespGetTransactionId.result.voutItem.scriptPubKey", Namespace="distributedcryptography")]
	public class SdtelectrumRespGetTransactionId_result_voutItem_scriptPubKey_RESTInterface : GxGenericCollectionItem<SdtelectrumRespGetTransactionId_result_voutItem_scriptPubKey>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtelectrumRespGetTransactionId_result_voutItem_scriptPubKey_RESTInterface( ) : base()
		{	
		}

		public SdtelectrumRespGetTransactionId_result_voutItem_scriptPubKey_RESTInterface( SdtelectrumRespGetTransactionId_result_voutItem_scriptPubKey psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="address", Order=0)]
		public  string gxTpr_Address
		{
			get { 
				return sdt.gxTpr_Address;

			}
			set { 
				 sdt.gxTpr_Address = value;
			}
		}

		[DataMember(Name="asm", Order=1)]
		public  string gxTpr_Asm
		{
			get { 
				return sdt.gxTpr_Asm;

			}
			set { 
				 sdt.gxTpr_Asm = value;
			}
		}

		[DataMember(Name="desc", Order=2)]
		public  string gxTpr_Desc
		{
			get { 
				return sdt.gxTpr_Desc;

			}
			set { 
				 sdt.gxTpr_Desc = value;
			}
		}

		[DataMember(Name="hex", Order=3)]
		public  string gxTpr_Hex
		{
			get { 
				return sdt.gxTpr_Hex;

			}
			set { 
				 sdt.gxTpr_Hex = value;
			}
		}

		[DataMember(Name="type", Order=4)]
		public  string gxTpr_Type
		{
			get { 
				return sdt.gxTpr_Type;

			}
			set { 
				 sdt.gxTpr_Type = value;
			}
		}


		#endregion

		public SdtelectrumRespGetTransactionId_result_voutItem_scriptPubKey sdt
		{
			get { 
				return (SdtelectrumRespGetTransactionId_result_voutItem_scriptPubKey)Sdt;
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
				sdt = new SdtelectrumRespGetTransactionId_result_voutItem_scriptPubKey() ;
			}
		}
	}
	#endregion
}