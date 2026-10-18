/*
				   File: type_Sdtlogout__postOutput
			Description: logout__postOutput
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
	[XmlRoot(ElementName="logout__postOutput")]
	[XmlType(TypeName="logout__postOutput" , Namespace="distributedcryptography" )]
	[Serializable]
	public class Sdtlogout__postOutput : GxUserType
	{
		public Sdtlogout__postOutput( )
		{
			/* Constructor for serialization */
			gxTv_Sdtlogout__postOutput_Error = "";

		}

		public Sdtlogout__postOutput(IGxContext context)
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
			AddObjectProperty("error", gxTpr_Error, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="error")]
		[XmlElement(ElementName="error")]
		public string gxTpr_Error
		{
			get {
				return gxTv_Sdtlogout__postOutput_Error; 
			}
			set {
				gxTv_Sdtlogout__postOutput_Error = value;
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
			gxTv_Sdtlogout__postOutput_Error = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_Sdtlogout__postOutput_Error;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"logout__postOutput", Namespace="distributedcryptography")]
	public class Sdtlogout__postOutput_RESTInterface : GxGenericCollectionItem<Sdtlogout__postOutput>, System.Web.SessionState.IRequiresSessionState
	{
		public Sdtlogout__postOutput_RESTInterface( ) : base()
		{	
		}

		public Sdtlogout__postOutput_RESTInterface( Sdtlogout__postOutput psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("error")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="error", Order=0)]
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
		public Sdtlogout__postOutput sdt
		{
			get { 
				return (Sdtlogout__postOutput)Sdt;
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
				sdt = new Sdtlogout__postOutput() ;
			}
		}
	}
	#endregion
}