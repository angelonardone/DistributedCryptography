/*
				   File: type_SdtRubySeedSplitContact
			Description: RubySeedSplitContact
				 Author: Nemo 🐠 for C# (.NET) version 18.0.16.189595
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
	[XmlRoot(ElementName="RubySeedSplitContact")]
	[XmlType(TypeName="RubySeedSplitContact" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtRubySeedSplitContact : GxUserType
	{
		public SdtRubySeedSplitContact( )
		{
			/* Constructor for serialization */
			gxTv_SdtRubySeedSplitContact_Name = "";

			gxTv_SdtRubySeedSplitContact_Pubkey = "";


		}

		public SdtRubySeedSplitContact(IGxContext context)
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
			AddObjectProperty("name", gxTpr_Name, false);


			AddObjectProperty("pubKey", gxTpr_Pubkey, false);


			AddObjectProperty("shares", gxTpr_Shares, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="name")]
		[XmlElement(ElementName="name")]
		public string gxTpr_Name
		{
			get {
				return gxTv_SdtRubySeedSplitContact_Name; 
			}
			set {
				gxTv_SdtRubySeedSplitContact_Name = value;
				SetDirty("Name");
			}
		}




		[SoapElement(ElementName="pubKey")]
		[XmlElement(ElementName="pubKey")]
		public string gxTpr_Pubkey
		{
			get {
				return gxTv_SdtRubySeedSplitContact_Pubkey; 
			}
			set {
				gxTv_SdtRubySeedSplitContact_Pubkey = value;
				SetDirty("Pubkey");
			}
		}




		[SoapElement(ElementName="shares")]
		[XmlElement(ElementName="shares")]
		public short gxTpr_Shares
		{
			get {
				return gxTv_SdtRubySeedSplitContact_Shares; 
			}
			set {
				gxTv_SdtRubySeedSplitContact_Shares = value;
				SetDirty("Shares");
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
			gxTv_SdtRubySeedSplitContact_Name = "";
			gxTv_SdtRubySeedSplitContact_Pubkey = "";

			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtRubySeedSplitContact_Name;
		 

		protected string gxTv_SdtRubySeedSplitContact_Pubkey;
		 

		protected short gxTv_SdtRubySeedSplitContact_Shares;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"RubySeedSplitContact", Namespace="distributedcryptography")]
	public class SdtRubySeedSplitContact_RESTInterface : GxGenericCollectionItem<SdtRubySeedSplitContact>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtRubySeedSplitContact_RESTInterface( ) : base()
		{	
		}

		public SdtRubySeedSplitContact_RESTInterface( SdtRubySeedSplitContact psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("name")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="name", Order=0)]
		public  string gxTpr_Name
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Name);

			}
			set { 
				 sdt.gxTpr_Name = value;
			}
		}

		[JsonPropertyName("pubKey")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="pubKey", Order=1)]
		public  string gxTpr_Pubkey
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Pubkey);

			}
			set { 
				 sdt.gxTpr_Pubkey = value;
			}
		}

		[JsonPropertyName("shares")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="shares", Order=2)]
		public short gxTpr_Shares
		{
			get { 
				return sdt.gxTpr_Shares;

			}
			set { 
				sdt.gxTpr_Shares = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtRubySeedSplitContact sdt
		{
			get { 
				return (SdtRubySeedSplitContact)Sdt;
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
				sdt = new SdtRubySeedSplitContact() ;
			}
		}
	}
	#endregion
}