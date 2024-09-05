/*
				   File: type_SdtSdtChosenResult
			Description: SdtChosenResult
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
namespace GeneXus.Programs.wallet
{
	[XmlRoot(ElementName="SdtChosenResult")]
	[XmlType(TypeName="SdtChosenResult" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtSdtChosenResult : GxUserType
	{
		public SdtSdtChosenResult( )
		{
			/* Constructor for serialization */
		}

		public SdtSdtChosenResult(IGxContext context)
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
			if (gxTv_SdtSdtChosenResult_Chosenresult != null)
			{
				AddObjectProperty("chosenResult", gxTv_SdtSdtChosenResult_Chosenresult, false);
			}
			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="chosenResult" )]
		[XmlArray(ElementName="chosenResult"  )]
		[XmlArrayItemAttribute(ElementName="value" , IsNullable=false )]
		public GXBaseCollection<GeneXus.Programs.wallet.SdtSdtChosenResultItem> gxTpr_Chosenresult_GXBaseCollection
		{
			get {
				if ( gxTv_SdtSdtChosenResult_Chosenresult == null )
				{
					gxTv_SdtSdtChosenResult_Chosenresult = new GXBaseCollection<GeneXus.Programs.wallet.SdtSdtChosenResultItem>( context, "SdtChosenResultItem", "");
				}
				return gxTv_SdtSdtChosenResult_Chosenresult;
			}
			set {
				gxTv_SdtSdtChosenResult_Chosenresult_N = false;
				gxTv_SdtSdtChosenResult_Chosenresult = value;
			}
		}

		[XmlIgnore]
		public GXBaseCollection<GeneXus.Programs.wallet.SdtSdtChosenResultItem> gxTpr_Chosenresult
		{
			get {
				if ( gxTv_SdtSdtChosenResult_Chosenresult == null )
				{
					gxTv_SdtSdtChosenResult_Chosenresult = new GXBaseCollection<GeneXus.Programs.wallet.SdtSdtChosenResultItem>( context, "SdtChosenResultItem", "");
				}
				gxTv_SdtSdtChosenResult_Chosenresult_N = false;
				return gxTv_SdtSdtChosenResult_Chosenresult ;
			}
			set {
				gxTv_SdtSdtChosenResult_Chosenresult_N = false;
				gxTv_SdtSdtChosenResult_Chosenresult = value;
				SetDirty("Chosenresult");
			}
		}

		public void gxTv_SdtSdtChosenResult_Chosenresult_SetNull()
		{
			gxTv_SdtSdtChosenResult_Chosenresult_N = true;
			gxTv_SdtSdtChosenResult_Chosenresult = null;
		}

		public bool gxTv_SdtSdtChosenResult_Chosenresult_IsNull()
		{
			return gxTv_SdtSdtChosenResult_Chosenresult == null;
		}
		public bool ShouldSerializegxTpr_Chosenresult_GXBaseCollection_Json()
		{
			return gxTv_SdtSdtChosenResult_Chosenresult != null && gxTv_SdtSdtChosenResult_Chosenresult.Count > 0;

		}

		public override bool ShouldSerializeSdtJson()
		{
			return (
				 ShouldSerializegxTpr_Chosenresult_GXBaseCollection_Json()||  
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
			gxTv_SdtSdtChosenResult_Chosenresult_N = true;

			return  ;
		}



		#endregion

		#region Declaration

		protected bool gxTv_SdtSdtChosenResult_Chosenresult_N;
		protected GXBaseCollection<GeneXus.Programs.wallet.SdtSdtChosenResultItem> gxTv_SdtSdtChosenResult_Chosenresult = null;  


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"SdtChosenResult", Namespace="distributedcryptography")]
	public class SdtSdtChosenResult_RESTInterface : GxGenericCollectionItem<SdtSdtChosenResult>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtSdtChosenResult_RESTInterface( ) : base()
		{	
		}

		public SdtSdtChosenResult_RESTInterface( SdtSdtChosenResult psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="chosenResult", Order=0, EmitDefaultValue=false)]
		public  GxGenericCollection<GeneXus.Programs.wallet.SdtSdtChosenResultItem_RESTInterface> gxTpr_Chosenresult
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Chosenresult_GXBaseCollection_Json())
					return new GxGenericCollection<GeneXus.Programs.wallet.SdtSdtChosenResultItem_RESTInterface>(sdt.gxTpr_Chosenresult);
				else
					return null;

			}
			set { 
				value.LoadCollection(sdt.gxTpr_Chosenresult);
			}
		}


		#endregion

		public SdtSdtChosenResult sdt
		{
			get { 
				return (SdtSdtChosenResult)Sdt;
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
				sdt = new SdtSdtChosenResult() ;
			}
		}
	}
	#endregion
}