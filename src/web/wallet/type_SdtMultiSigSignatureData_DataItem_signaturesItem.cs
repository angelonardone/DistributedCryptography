/*
				   File: type_SdtMultiSigSignatureData_DataItem_signaturesItem
			Description: signatures
				 Author: Nemo 🐠 for C# (.NET) version 18.0.13.186702
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
using System.Text.Json.Serialization;

using GeneXus.Programs;
namespace GeneXus.Programs.wallet
{
	[XmlRoot(ElementName="MultiSigSignatureData.DataItem.signaturesItem")]
	[XmlType(TypeName="MultiSigSignatureData.DataItem.signaturesItem" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtMultiSigSignatureData_DataItem_signaturesItem : GxUserType
	{
		public SdtMultiSigSignatureData_DataItem_signaturesItem( )
		{
			/* Constructor for serialization */
			gxTv_SdtMultiSigSignatureData_DataItem_signaturesItem_Signature = "";

			gxTv_SdtMultiSigSignatureData_DataItem_signaturesItem_Xonlypubkey = "";

		}

		public SdtMultiSigSignatureData_DataItem_signaturesItem(IGxContext context)
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
			AddObjectProperty("signature", gxTpr_Signature, false);


			AddObjectProperty("XOnlyPubKey", gxTpr_Xonlypubkey, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="signature")]
		[XmlElement(ElementName="signature")]
		public string gxTpr_Signature
		{
			get {
				return gxTv_SdtMultiSigSignatureData_DataItem_signaturesItem_Signature; 
			}
			set {
				gxTv_SdtMultiSigSignatureData_DataItem_signaturesItem_Signature = value;
				SetDirty("Signature");
			}
		}




		[SoapElement(ElementName="XOnlyPubKey")]
		[XmlElement(ElementName="XOnlyPubKey")]
		public string gxTpr_Xonlypubkey
		{
			get {
				return gxTv_SdtMultiSigSignatureData_DataItem_signaturesItem_Xonlypubkey; 
			}
			set {
				gxTv_SdtMultiSigSignatureData_DataItem_signaturesItem_Xonlypubkey = value;
				SetDirty("Xonlypubkey");
			}
		}



		public override bool ShouldSerializeSdtJson()
		{
			return true;
		}



		#endregion

		#region Static Type Properties

		[XmlIgnore]
		private static GXTypeInfo _typeProps;
		protected override GXTypeInfo TypeInfo { get { return _typeProps; } set { _typeProps = value; } }

		#endregion

		#region Initialization

		public void initialize( )
		{
			gxTv_SdtMultiSigSignatureData_DataItem_signaturesItem_Signature = "";
			gxTv_SdtMultiSigSignatureData_DataItem_signaturesItem_Xonlypubkey = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtMultiSigSignatureData_DataItem_signaturesItem_Signature;
		 

		protected string gxTv_SdtMultiSigSignatureData_DataItem_signaturesItem_Xonlypubkey;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("wrapped")]
	[DataContract(Name=@"MultiSigSignatureData.DataItem.signaturesItem", Namespace="distributedcryptography")]
	public class SdtMultiSigSignatureData_DataItem_signaturesItem_RESTInterface : GxGenericCollectionItem<SdtMultiSigSignatureData_DataItem_signaturesItem>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtMultiSigSignatureData_DataItem_signaturesItem_RESTInterface( ) : base()
		{	
		}

		public SdtMultiSigSignatureData_DataItem_signaturesItem_RESTInterface( SdtMultiSigSignatureData_DataItem_signaturesItem psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("signature")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="signature", Order=0)]
		public  string gxTpr_Signature
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Signature);

			}
			set { 
				 sdt.gxTpr_Signature = value;
			}
		}

		[JsonPropertyName("XOnlyPubKey")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="XOnlyPubKey", Order=1)]
		public  string gxTpr_Xonlypubkey
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Xonlypubkey);

			}
			set { 
				 sdt.gxTpr_Xonlypubkey = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtMultiSigSignatureData_DataItem_signaturesItem sdt
		{
			get { 
				return (SdtMultiSigSignatureData_DataItem_signaturesItem)Sdt;
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
				sdt = new SdtMultiSigSignatureData_DataItem_signaturesItem() ;
			}
		}
	}
	#endregion
}