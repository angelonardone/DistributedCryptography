/*
				   File: type_SdtGroup_SDT_otherGroup
			Description: otherGroup
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
using GeneXus.Programs.wallet;
namespace GeneXus.Programs.wallet.registered
{
	[XmlRoot(ElementName="Group_SDT.otherGroup")]
	[XmlType(TypeName="Group_SDT.otherGroup" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtGroup_SDT_otherGroup : GxUserType
	{
		public SdtGroup_SDT_otherGroup( )
		{
			/* Constructor for serialization */
			gxTv_SdtGroup_SDT_otherGroup_Encpassword = "";

			gxTv_SdtGroup_SDT_otherGroup_Referenceusernname = "";

			gxTv_SdtGroup_SDT_otherGroup_Signature = "";

			gxTv_SdtGroup_SDT_otherGroup_Extpubkeymultisigreceiving = "";

			gxTv_SdtGroup_SDT_otherGroup_Extpubkeymultisigchange = "";

			gxTv_SdtGroup_SDT_otherGroup_Extpubkeytimebountyreceiving = "";

		}

		public SdtGroup_SDT_otherGroup(IGxContext context)
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
			AddObjectProperty("referenceGroupId", gxTpr_Referencegroupid, false);


			AddObjectProperty("invitationDeclined", gxTpr_Invitationdeclined, false);


			AddObjectProperty("encPassword", gxTpr_Encpassword, false);


			AddObjectProperty("referenceUsernName", gxTpr_Referenceusernname, false);


			AddObjectProperty("signature", gxTpr_Signature, false);


			AddObjectProperty("extPubKeyMultiSigReceiving", gxTpr_Extpubkeymultisigreceiving, false);


			AddObjectProperty("extPubKeyMultiSigChange", gxTpr_Extpubkeymultisigchange, false);


			AddObjectProperty("extPubKeyTimeBountyReceiving", gxTpr_Extpubkeytimebountyreceiving, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="referenceGroupId")]
		[XmlElement(ElementName="referenceGroupId")]
		public Guid gxTpr_Referencegroupid
		{
			get {
				return gxTv_SdtGroup_SDT_otherGroup_Referencegroupid; 
			}
			set {
				gxTv_SdtGroup_SDT_otherGroup_Referencegroupid = value;
				SetDirty("Referencegroupid");
			}
		}




		[SoapElement(ElementName="invitationDeclined")]
		[XmlElement(ElementName="invitationDeclined")]
		public bool gxTpr_Invitationdeclined
		{
			get {
				return gxTv_SdtGroup_SDT_otherGroup_Invitationdeclined; 
			}
			set {
				gxTv_SdtGroup_SDT_otherGroup_Invitationdeclined = value;
				SetDirty("Invitationdeclined");
			}
		}




		[SoapElement(ElementName="encPassword")]
		[XmlElement(ElementName="encPassword")]
		public string gxTpr_Encpassword
		{
			get {
				return gxTv_SdtGroup_SDT_otherGroup_Encpassword; 
			}
			set {
				gxTv_SdtGroup_SDT_otherGroup_Encpassword = value;
				SetDirty("Encpassword");
			}
		}




		[SoapElement(ElementName="referenceUsernName")]
		[XmlElement(ElementName="referenceUsernName")]
		public string gxTpr_Referenceusernname
		{
			get {
				return gxTv_SdtGroup_SDT_otherGroup_Referenceusernname; 
			}
			set {
				gxTv_SdtGroup_SDT_otherGroup_Referenceusernname = value;
				SetDirty("Referenceusernname");
			}
		}




		[SoapElement(ElementName="signature")]
		[XmlElement(ElementName="signature")]
		public string gxTpr_Signature
		{
			get {
				return gxTv_SdtGroup_SDT_otherGroup_Signature; 
			}
			set {
				gxTv_SdtGroup_SDT_otherGroup_Signature = value;
				SetDirty("Signature");
			}
		}




		[SoapElement(ElementName="extPubKeyMultiSigReceiving")]
		[XmlElement(ElementName="extPubKeyMultiSigReceiving")]
		public string gxTpr_Extpubkeymultisigreceiving
		{
			get {
				return gxTv_SdtGroup_SDT_otherGroup_Extpubkeymultisigreceiving; 
			}
			set {
				gxTv_SdtGroup_SDT_otherGroup_Extpubkeymultisigreceiving = value;
				SetDirty("Extpubkeymultisigreceiving");
			}
		}




		[SoapElement(ElementName="extPubKeyMultiSigChange")]
		[XmlElement(ElementName="extPubKeyMultiSigChange")]
		public string gxTpr_Extpubkeymultisigchange
		{
			get {
				return gxTv_SdtGroup_SDT_otherGroup_Extpubkeymultisigchange; 
			}
			set {
				gxTv_SdtGroup_SDT_otherGroup_Extpubkeymultisigchange = value;
				SetDirty("Extpubkeymultisigchange");
			}
		}




		[SoapElement(ElementName="extPubKeyTimeBountyReceiving")]
		[XmlElement(ElementName="extPubKeyTimeBountyReceiving")]
		public string gxTpr_Extpubkeytimebountyreceiving
		{
			get {
				return gxTv_SdtGroup_SDT_otherGroup_Extpubkeytimebountyreceiving; 
			}
			set {
				gxTv_SdtGroup_SDT_otherGroup_Extpubkeytimebountyreceiving = value;
				SetDirty("Extpubkeytimebountyreceiving");
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
			gxTv_SdtGroup_SDT_otherGroup_Encpassword = "";
			gxTv_SdtGroup_SDT_otherGroup_Referenceusernname = "";
			gxTv_SdtGroup_SDT_otherGroup_Signature = "";
			gxTv_SdtGroup_SDT_otherGroup_Extpubkeymultisigreceiving = "";
			gxTv_SdtGroup_SDT_otherGroup_Extpubkeymultisigchange = "";
			gxTv_SdtGroup_SDT_otherGroup_Extpubkeytimebountyreceiving = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected Guid gxTv_SdtGroup_SDT_otherGroup_Referencegroupid;
		 

		protected bool gxTv_SdtGroup_SDT_otherGroup_Invitationdeclined;
		 

		protected string gxTv_SdtGroup_SDT_otherGroup_Encpassword;
		 

		protected string gxTv_SdtGroup_SDT_otherGroup_Referenceusernname;
		 

		protected string gxTv_SdtGroup_SDT_otherGroup_Signature;
		 

		protected string gxTv_SdtGroup_SDT_otherGroup_Extpubkeymultisigreceiving;
		 

		protected string gxTv_SdtGroup_SDT_otherGroup_Extpubkeymultisigchange;
		 

		protected string gxTv_SdtGroup_SDT_otherGroup_Extpubkeytimebountyreceiving;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"Group_SDT.otherGroup", Namespace="distributedcryptography")]
	public class SdtGroup_SDT_otherGroup_RESTInterface : GxGenericCollectionItem<SdtGroup_SDT_otherGroup>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtGroup_SDT_otherGroup_RESTInterface( ) : base()
		{	
		}

		public SdtGroup_SDT_otherGroup_RESTInterface( SdtGroup_SDT_otherGroup psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("referenceGroupId")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="referenceGroupId", Order=0)]
		public Guid gxTpr_Referencegroupid
		{
			get { 
				return sdt.gxTpr_Referencegroupid;

			}
			set { 
				sdt.gxTpr_Referencegroupid = value;
			}
		}

		[JsonPropertyName("invitationDeclined")]
		[JsonPropertyOrder(1)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="invitationDeclined", Order=1)]
		public bool gxTpr_Invitationdeclined
		{
			get { 
				return sdt.gxTpr_Invitationdeclined;

			}
			set { 
				sdt.gxTpr_Invitationdeclined = value;
			}
		}

		[JsonPropertyName("encPassword")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="encPassword", Order=2)]
		public  string gxTpr_Encpassword
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Encpassword);

			}
			set { 
				 sdt.gxTpr_Encpassword = value;
			}
		}

		[JsonPropertyName("referenceUsernName")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="referenceUsernName", Order=3)]
		public  string gxTpr_Referenceusernname
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Referenceusernname);

			}
			set { 
				 sdt.gxTpr_Referenceusernname = value;
			}
		}

		[JsonPropertyName("signature")]
		[JsonPropertyOrder(4)]
		[DataMember(Name="signature", Order=4)]
		public  string gxTpr_Signature
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Signature);

			}
			set { 
				 sdt.gxTpr_Signature = value;
			}
		}

		[JsonPropertyName("extPubKeyMultiSigReceiving")]
		[JsonPropertyOrder(5)]
		[DataMember(Name="extPubKeyMultiSigReceiving", Order=5)]
		public  string gxTpr_Extpubkeymultisigreceiving
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Extpubkeymultisigreceiving);

			}
			set { 
				 sdt.gxTpr_Extpubkeymultisigreceiving = value;
			}
		}

		[JsonPropertyName("extPubKeyMultiSigChange")]
		[JsonPropertyOrder(6)]
		[DataMember(Name="extPubKeyMultiSigChange", Order=6)]
		public  string gxTpr_Extpubkeymultisigchange
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Extpubkeymultisigchange);

			}
			set { 
				 sdt.gxTpr_Extpubkeymultisigchange = value;
			}
		}

		[JsonPropertyName("extPubKeyTimeBountyReceiving")]
		[JsonPropertyOrder(7)]
		[DataMember(Name="extPubKeyTimeBountyReceiving", Order=7)]
		public  string gxTpr_Extpubkeytimebountyreceiving
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Extpubkeytimebountyreceiving);

			}
			set { 
				 sdt.gxTpr_Extpubkeytimebountyreceiving = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtGroup_SDT_otherGroup sdt
		{
			get { 
				return (SdtGroup_SDT_otherGroup)Sdt;
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
				sdt = new SdtGroup_SDT_otherGroup() ;
			}
		}
	}
	#endregion
}