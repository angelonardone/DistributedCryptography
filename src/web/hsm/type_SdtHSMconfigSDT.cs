/*
				   File: type_SdtHSMconfigSDT
			Description: HSMconfigSDT
				 Author: Nemo 🐠 for C# (.NET) version 18.0.13.186676
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
namespace GeneXus.Programs.hsm
{
	[XmlRoot(ElementName="HSMconfigSDT")]
	[XmlType(TypeName="HSMconfigSDT" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtHSMconfigSDT : GxUserType
	{
		public SdtHSMconfigSDT( )
		{
			/* Constructor for serialization */
		}

		public SdtHSMconfigSDT(IGxContext context)
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
			AddObjectProperty("isActive", gxTpr_Isactive, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="isActive")]
		[XmlElement(ElementName="isActive")]
		public bool gxTpr_Isactive
		{
			get {
				return gxTv_SdtHSMconfigSDT_Isactive; 
			}
			set {
				gxTv_SdtHSMconfigSDT_Isactive = value;
				SetDirty("Isactive");
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
			return  ;
		}



		#endregion

		#region Declaration

		protected bool gxTv_SdtHSMconfigSDT_Isactive;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"HSMconfigSDT", Namespace="distributedcryptography")]
	public class SdtHSMconfigSDT_RESTInterface : GxGenericCollectionItem<SdtHSMconfigSDT>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtHSMconfigSDT_RESTInterface( ) : base()
		{	
		}

		public SdtHSMconfigSDT_RESTInterface( SdtHSMconfigSDT psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("isActive")]
		[JsonPropertyOrder(0)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="isActive", Order=0)]
		public bool gxTpr_Isactive
		{
			get { 
				return sdt.gxTpr_Isactive;

			}
			set { 
				sdt.gxTpr_Isactive = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtHSMconfigSDT sdt
		{
			get { 
				return (SdtHSMconfigSDT)Sdt;
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
				sdt = new SdtHSMconfigSDT() ;
			}
		}
	}
	#endregion
}