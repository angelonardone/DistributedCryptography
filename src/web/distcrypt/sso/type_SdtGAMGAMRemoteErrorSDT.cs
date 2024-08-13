/*
				   File: type_SdtGAMGAMRemoteErrorSDT
			Description: GAMGAMRemoteErrorSDT
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
using GeneXus.Programs.distcrypt;
namespace GeneXus.Programs.distcrypt.sso
{
	[XmlRoot(ElementName="GAMGAMRemoteErrorSDT")]
	[XmlType(TypeName="GAMGAMRemoteErrorSDT" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtGAMGAMRemoteErrorSDT : GxUserType
	{
		public SdtGAMGAMRemoteErrorSDT( )
		{
			/* Constructor for serialization */
			gxTv_SdtGAMGAMRemoteErrorSDT_Error = "";

		}

		public SdtGAMGAMRemoteErrorSDT(IGxContext context)
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
			AddObjectProperty("code", gxTpr_Code, false);


			AddObjectProperty("error", gxTpr_Error, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="code")]
		[XmlElement(ElementName="code")]
		public long gxTpr_Code
		{
			get {
				return gxTv_SdtGAMGAMRemoteErrorSDT_Code; 
			}
			set {
				gxTv_SdtGAMGAMRemoteErrorSDT_Code = value;
				SetDirty("Code");
			}
		}




		[SoapElement(ElementName="error")]
		[XmlElement(ElementName="error")]
		public string gxTpr_Error
		{
			get {
				return gxTv_SdtGAMGAMRemoteErrorSDT_Error; 
			}
			set {
				gxTv_SdtGAMGAMRemoteErrorSDT_Error = value;
				SetDirty("Error");
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
			gxTv_SdtGAMGAMRemoteErrorSDT_Error = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected long gxTv_SdtGAMGAMRemoteErrorSDT_Code;
		 

		protected string gxTv_SdtGAMGAMRemoteErrorSDT_Error;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"GAMGAMRemoteErrorSDT", Namespace="distributedcryptography")]
	public class SdtGAMGAMRemoteErrorSDT_RESTInterface : GxGenericCollectionItem<SdtGAMGAMRemoteErrorSDT>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtGAMGAMRemoteErrorSDT_RESTInterface( ) : base()
		{	
		}

		public SdtGAMGAMRemoteErrorSDT_RESTInterface( SdtGAMGAMRemoteErrorSDT psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="code", Order=0)]
		public  string gxTpr_Code
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Code, 12, 0));

			}
			set { 
				sdt.gxTpr_Code = (long) NumberUtil.Val( value, ".");
			}
		}

		[DataMember(Name="error", Order=1)]
		public  string gxTpr_Error
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Error);

			}
			set { 
				 sdt.gxTpr_Error = value;
			}
		}


		#endregion

		public SdtGAMGAMRemoteErrorSDT sdt
		{
			get { 
				return (SdtGAMGAMRemoteErrorSDT)Sdt;
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
				sdt = new SdtGAMGAMRemoteErrorSDT() ;
			}
		}
	}
	#endregion
}