/*
				   File: type_SdtPassword_tag
			Description: Password_tag
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
	[XmlRoot(ElementName="Password_tag")]
	[XmlType(TypeName="Password_tag" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtPassword_tag : GxUserType
	{
		public SdtPassword_tag( )
		{
			/* Constructor for serialization */
			gxTv_SdtPassword_tag_Name = "";

		}

		public SdtPassword_tag(IGxContext context)
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
			AddObjectProperty("TagId", gxTpr_Tagid, false);


			AddObjectProperty("Name", gxTpr_Name, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="TagId")]
		[XmlElement(ElementName="TagId")]
		public Guid gxTpr_Tagid
		{
			get {
				return gxTv_SdtPassword_tag_Tagid; 
			}
			set {
				gxTv_SdtPassword_tag_Tagid = value;
				SetDirty("Tagid");
			}
		}




		[SoapElement(ElementName="Name")]
		[XmlElement(ElementName="Name")]
		public string gxTpr_Name
		{
			get {
				return gxTv_SdtPassword_tag_Name; 
			}
			set {
				gxTv_SdtPassword_tag_Name = value;
				SetDirty("Name");
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
			gxTv_SdtPassword_tag_Name = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected Guid gxTv_SdtPassword_tag_Tagid;
		 

		protected string gxTv_SdtPassword_tag_Name;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"Password_tag", Namespace="distributedcryptography")]
	public class SdtPassword_tag_RESTInterface : GxGenericCollectionItem<SdtPassword_tag>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtPassword_tag_RESTInterface( ) : base()
		{	
		}

		public SdtPassword_tag_RESTInterface( SdtPassword_tag psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("TagId")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="TagId", Order=0)]
		public Guid gxTpr_Tagid
		{
			get { 
				return sdt.gxTpr_Tagid;

			}
			set { 
				sdt.gxTpr_Tagid = value;
			}
		}

		[JsonPropertyName("Name")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="Name", Order=1)]
		public  string gxTpr_Name
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Name);

			}
			set { 
				 sdt.gxTpr_Name = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtPassword_tag sdt
		{
			get { 
				return (SdtPassword_tag)Sdt;
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
				sdt = new SdtPassword_tag() ;
			}
		}
	}
	#endregion
}