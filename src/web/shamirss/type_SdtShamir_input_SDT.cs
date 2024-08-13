/*
				   File: type_SdtShamir_input_SDT
			Description: Shamir_input_SDT
				 Author: Nemo 🐠 for C# (.NET) version 18.0.10.184260
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
namespace GeneXus.Programs.shamirss
{
	[XmlRoot(ElementName="Shamir_input_SDT")]
	[XmlType(TypeName="Shamir_input_SDT" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtShamir_input_SDT : GxUserType
	{
		public SdtShamir_input_SDT( )
		{
			/* Constructor for serialization */
			gxTv_SdtShamir_input_SDT_Secret = "";

			gxTv_SdtShamir_input_SDT_Sha256 = "";

		}

		public SdtShamir_input_SDT(IGxContext context)
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
			AddObjectProperty("secret", gxTpr_Secret, false);


			AddObjectProperty("sha256", gxTpr_Sha256, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="secret")]
		[XmlElement(ElementName="secret")]
		public string gxTpr_Secret
		{
			get {
				return gxTv_SdtShamir_input_SDT_Secret; 
			}
			set {
				gxTv_SdtShamir_input_SDT_Secret = value;
				SetDirty("Secret");
			}
		}




		[SoapElement(ElementName="sha256")]
		[XmlElement(ElementName="sha256")]
		public string gxTpr_Sha256
		{
			get {
				return gxTv_SdtShamir_input_SDT_Sha256; 
			}
			set {
				gxTv_SdtShamir_input_SDT_Sha256 = value;
				SetDirty("Sha256");
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
			gxTv_SdtShamir_input_SDT_Secret = "";
			gxTv_SdtShamir_input_SDT_Sha256 = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtShamir_input_SDT_Secret;
		 

		protected string gxTv_SdtShamir_input_SDT_Sha256;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"Shamir_input_SDT", Namespace="distributedcryptography")]
	public class SdtShamir_input_SDT_RESTInterface : GxGenericCollectionItem<SdtShamir_input_SDT>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtShamir_input_SDT_RESTInterface( ) : base()
		{	
		}

		public SdtShamir_input_SDT_RESTInterface( SdtShamir_input_SDT psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="secret", Order=0)]
		public  string gxTpr_Secret
		{
			get { 
				return sdt.gxTpr_Secret;

			}
			set { 
				 sdt.gxTpr_Secret = value;
			}
		}

		[DataMember(Name="sha256", Order=1)]
		public  string gxTpr_Sha256
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Sha256);

			}
			set { 
				 sdt.gxTpr_Sha256 = value;
			}
		}


		#endregion

		public SdtShamir_input_SDT sdt
		{
			get { 
				return (SdtShamir_input_SDT)Sdt;
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
				sdt = new SdtShamir_input_SDT() ;
			}
		}
	}
	#endregion
}