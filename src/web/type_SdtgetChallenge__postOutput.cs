/*
				   File: type_SdtgetChallenge__postOutput
			Description: getChallenge__postOutput
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


namespace GeneXus.Programs
{
	[XmlRoot(ElementName="getChallenge__postOutput")]
	[XmlType(TypeName="getChallenge__postOutput" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtgetChallenge__postOutput : GxUserType
	{
		public SdtgetChallenge__postOutput( )
		{
			/* Constructor for serialization */
			gxTv_SdtgetChallenge__postOutput_Challenge = "";

			gxTv_SdtgetChallenge__postOutput_Error = "";

		}

		public SdtgetChallenge__postOutput(IGxContext context)
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
			AddObjectProperty("Challenge", gxTpr_Challenge, false);


			AddObjectProperty("error", gxTpr_Error, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="Challenge")]
		[XmlElement(ElementName="Challenge")]
		public string gxTpr_Challenge
		{
			get {
				return gxTv_SdtgetChallenge__postOutput_Challenge; 
			}
			set {
				gxTv_SdtgetChallenge__postOutput_Challenge = value;
				SetDirty("Challenge");
			}
		}




		[SoapElement(ElementName="error")]
		[XmlElement(ElementName="error")]
		public string gxTpr_Error
		{
			get {
				return gxTv_SdtgetChallenge__postOutput_Error; 
			}
			set {
				gxTv_SdtgetChallenge__postOutput_Error = value;
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
			gxTv_SdtgetChallenge__postOutput_Challenge = "";
			gxTv_SdtgetChallenge__postOutput_Error = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtgetChallenge__postOutput_Challenge;
		 

		protected string gxTv_SdtgetChallenge__postOutput_Error;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"getChallenge__postOutput", Namespace="distributedcryptography")]
	public class SdtgetChallenge__postOutput_RESTInterface : GxGenericCollectionItem<SdtgetChallenge__postOutput>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtgetChallenge__postOutput_RESTInterface( ) : base()
		{	
		}

		public SdtgetChallenge__postOutput_RESTInterface( SdtgetChallenge__postOutput psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("Challenge")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="Challenge", Order=0)]
		public  string gxTpr_Challenge
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Challenge);

			}
			set { 
				 sdt.gxTpr_Challenge = value;
			}
		}

		[JsonPropertyName("error")]
		[JsonPropertyOrder(1)]
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
		[JsonIgnore]
		public SdtgetChallenge__postOutput sdt
		{
			get { 
				return (SdtgetChallenge__postOutput)Sdt;
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
				sdt = new SdtgetChallenge__postOutput() ;
			}
		}
	}
	#endregion
}