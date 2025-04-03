/*
				   File: type_SdtGAMWSLoginOutUserAttMVSDT
			Description: GAMWSLoginOutUserAttMVSDT
				 Author: Nemo 🐠 for C# (.NET) version 18.0.12.186073
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
using GeneXus.Programs.distcrypt;
namespace GeneXus.Programs.distcrypt.sso
{
	[XmlRoot(ElementName="GAMWSLoginOutUserAttMVSDT")]
	[XmlType(TypeName="GAMWSLoginOutUserAttMVSDT" , Namespace="GAM" )]
	[Serializable]
	public class SdtGAMWSLoginOutUserAttMVSDT : GxUserType
	{
		public SdtGAMWSLoginOutUserAttMVSDT( )
		{
			/* Constructor for serialization */
			gxTv_SdtGAMWSLoginOutUserAttMVSDT_Id = "";

			gxTv_SdtGAMWSLoginOutUserAttMVSDT_Value = "";

		}

		public SdtGAMWSLoginOutUserAttMVSDT(IGxContext context)
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
			AddObjectProperty("Id", gxTpr_Id, false);


			AddObjectProperty("Value", gxTpr_Value, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="Id")]
		[XmlElement(ElementName="Id")]
		public string gxTpr_Id
		{
			get {
				return gxTv_SdtGAMWSLoginOutUserAttMVSDT_Id; 
			}
			set {
				gxTv_SdtGAMWSLoginOutUserAttMVSDT_Id = value;
				SetDirty("Id");
			}
		}




		[SoapElement(ElementName="Value")]
		[XmlElement(ElementName="Value")]
		public string gxTpr_Value
		{
			get {
				return gxTv_SdtGAMWSLoginOutUserAttMVSDT_Value; 
			}
			set {
				gxTv_SdtGAMWSLoginOutUserAttMVSDT_Value = value;
				SetDirty("Value");
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
			gxTv_SdtGAMWSLoginOutUserAttMVSDT_Id = "";
			gxTv_SdtGAMWSLoginOutUserAttMVSDT_Value = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtGAMWSLoginOutUserAttMVSDT_Id;
		 

		protected string gxTv_SdtGAMWSLoginOutUserAttMVSDT_Value;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"GAMWSLoginOutUserAttMVSDT", Namespace="GAM")]
	public class SdtGAMWSLoginOutUserAttMVSDT_RESTInterface : GxGenericCollectionItem<SdtGAMWSLoginOutUserAttMVSDT>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtGAMWSLoginOutUserAttMVSDT_RESTInterface( ) : base()
		{	
		}

		public SdtGAMWSLoginOutUserAttMVSDT_RESTInterface( SdtGAMWSLoginOutUserAttMVSDT psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("Id")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="Id", Order=0)]
		public  string gxTpr_Id
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Id);

			}
			set { 
				 sdt.gxTpr_Id = value;
			}
		}

		[JsonPropertyName("Value")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="Value", Order=1)]
		public  string gxTpr_Value
		{
			get { 
				return sdt.gxTpr_Value;

			}
			set { 
				 sdt.gxTpr_Value = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtGAMWSLoginOutUserAttMVSDT sdt
		{
			get { 
				return (SdtGAMWSLoginOutUserAttMVSDT)Sdt;
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
				sdt = new SdtGAMWSLoginOutUserAttMVSDT() ;
			}
		}
	}
	#endregion
}