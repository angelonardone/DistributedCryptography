/*
				   File: type_SdtGxGetAddressess
			Description: GxGetAddressess
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
	[XmlRoot(ElementName="GxGetAddressess")]
	[XmlType(TypeName="GxGetAddressess" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtGxGetAddressess : GxUserType
	{
		public SdtGxGetAddressess( )
		{
			/* Constructor for serialization */
			gxTv_SdtGxGetAddressess_Sdt_addressess_N = true;

		}

		public SdtGxGetAddressess(IGxContext context)
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
			if (gxTv_SdtGxGetAddressess_Sdt_addressess != null)
			{
				AddObjectProperty("SDT_Addressess", gxTv_SdtGxGetAddressess_Sdt_addressess, false);
			}
			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="SDT_Addressess")]
		[XmlElement(ElementName="SDT_Addressess")]
		public GeneXus.Programs.SdtNBitcoin_SDT_Addressess gxTpr_Sdt_addressess
		{
			get {
				if ( gxTv_SdtGxGetAddressess_Sdt_addressess == null )
				{
					gxTv_SdtGxGetAddressess_Sdt_addressess = new GeneXus.Programs.SdtNBitcoin_SDT_Addressess(context);
					SetDirty("Sdt_addressess");
				}
				return gxTv_SdtGxGetAddressess_Sdt_addressess; 
			}
			set {
				gxTv_SdtGxGetAddressess_Sdt_addressess = value;
				SetDirty("Sdt_addressess");
			}
		}
		public void gxTv_SdtGxGetAddressess_Sdt_addressess_SetNull()
		{
			gxTv_SdtGxGetAddressess_Sdt_addressess_N = true;
			gxTv_SdtGxGetAddressess_Sdt_addressess = null;
		}

		public bool gxTv_SdtGxGetAddressess_Sdt_addressess_IsNull()
		{
			return gxTv_SdtGxGetAddressess_Sdt_addressess == null;
		}
		public bool ShouldSerializegxTpr_Sdt_addressess_Json()
		{
			return gxTv_SdtGxGetAddressess_Sdt_addressess != null;

		}

		public override bool ShouldSerializeSdtJson()
		{
			return (
				 ShouldSerializegxTpr_Sdt_addressess_Json()||  
				false);
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
			gxTv_SdtGxGetAddressess_Sdt_addressess_N = true;

			return  ;
		}



		#endregion

		#region Declaration

		protected GeneXus.Programs.SdtNBitcoin_SDT_Addressess gxTv_SdtGxGetAddressess_Sdt_addressess = null;
		protected bool gxTv_SdtGxGetAddressess_Sdt_addressess_N;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"GxGetAddressess", Namespace="distributedcryptography")]
	public class SdtGxGetAddressess_RESTInterface : GxGenericCollectionItem<SdtGxGetAddressess>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtGxGetAddressess_RESTInterface( ) : base()
		{	
		}

		public SdtGxGetAddressess_RESTInterface( SdtGxGetAddressess psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("SDT_Addressess")]
		[JsonPropertyOrder(0)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="SDT_Addressess", Order=0, EmitDefaultValue=false)]
		public GeneXus.Programs.SdtNBitcoin_SDT_Addressess_RESTInterface gxTpr_Sdt_addressess
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Sdt_addressess_Json())
					return new GeneXus.Programs.SdtNBitcoin_SDT_Addressess_RESTInterface(sdt.gxTpr_Sdt_addressess);
				else
					return null;

			}
			set { 
				sdt.gxTpr_Sdt_addressess = value.sdt;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtGxGetAddressess sdt
		{
			get { 
				return (SdtGxGetAddressess)Sdt;
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
				sdt = new SdtGxGetAddressess() ;
			}
		}
	}
	#endregion
}