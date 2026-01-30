/*
				   File: type_SdtdeleteContact__postInput
			Description: deleteContact__postInput
				 Author: Nemo 🐠 for C# (.NET) version 18.0.14.187820
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
	[XmlRoot(ElementName="deleteContact__postInput")]
	[XmlType(TypeName="deleteContact__postInput" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtdeleteContact__postInput : GxUserType
	{
		public SdtdeleteContact__postInput( )
		{
			/* Constructor for serialization */
		}

		public SdtdeleteContact__postInput(IGxContext context)
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
			AddObjectProperty("ContactId", gxTpr_Contactid, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="ContactId")]
		[XmlElement(ElementName="ContactId")]
		public Guid gxTpr_Contactid
		{
			get {
				return gxTv_SdtdeleteContact__postInput_Contactid; 
			}
			set {
				gxTv_SdtdeleteContact__postInput_Contactid = value;
				SetDirty("Contactid");
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

		protected Guid gxTv_SdtdeleteContact__postInput_Contactid;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"deleteContact__postInput", Namespace="distributedcryptography")]
	public class SdtdeleteContact__postInput_RESTInterface : GxGenericCollectionItem<SdtdeleteContact__postInput>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtdeleteContact__postInput_RESTInterface( ) : base()
		{	
		}

		public SdtdeleteContact__postInput_RESTInterface( SdtdeleteContact__postInput psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("ContactId")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="ContactId", Order=0)]
		public Guid gxTpr_Contactid
		{
			get { 
				return sdt.gxTpr_Contactid;

			}
			set { 
				sdt.gxTpr_Contactid = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtdeleteContact__postInput sdt
		{
			get { 
				return (SdtdeleteContact__postInput)Sdt;
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
				sdt = new SdtdeleteContact__postInput() ;
			}
		}
	}
	#endregion
}