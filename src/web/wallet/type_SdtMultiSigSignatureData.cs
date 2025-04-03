/*
				   File: type_SdtMultiSigSignatureData
			Description: MultiSigSignatureData
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
namespace GeneXus.Programs.wallet
{
	[XmlRoot(ElementName="MultiSigSignatureData")]
	[XmlType(TypeName="MultiSigSignatureData" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtMultiSigSignatureData : GxUserType
	{
		public SdtMultiSigSignatureData( )
		{
			/* Constructor for serialization */
			gxTv_SdtMultiSigSignatureData_Contactusername = "";

		}

		public SdtMultiSigSignatureData(IGxContext context)
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
			AddObjectProperty("contactUserName", gxTpr_Contactusername, false);

			if (gxTv_SdtMultiSigSignatureData_Data != null)
			{
				AddObjectProperty("Data", gxTv_SdtMultiSigSignatureData_Data, false);
			}
			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="contactUserName")]
		[XmlElement(ElementName="contactUserName")]
		public string gxTpr_Contactusername
		{
			get {
				return gxTv_SdtMultiSigSignatureData_Contactusername; 
			}
			set {
				gxTv_SdtMultiSigSignatureData_Contactusername = value;
				SetDirty("Contactusername");
			}
		}




		[SoapElement(ElementName="Data" )]
		[XmlArray(ElementName="Data"  )]
		[XmlArrayItemAttribute(ElementName="DataItem" , IsNullable=false )]
		public GXBaseCollection<SdtMultiSigSignatureData_DataItem> gxTpr_Data
		{
			get {
				if ( gxTv_SdtMultiSigSignatureData_Data == null )
				{
					gxTv_SdtMultiSigSignatureData_Data = new GXBaseCollection<SdtMultiSigSignatureData_DataItem>( context, "MultiSigSignatureData.DataItem", "");
				}
				SetDirty("Data");
				return gxTv_SdtMultiSigSignatureData_Data;
			}
			set {
				gxTv_SdtMultiSigSignatureData_Data_N = false;
				gxTv_SdtMultiSigSignatureData_Data = value;
				SetDirty("Data");
			}
		}

		public void gxTv_SdtMultiSigSignatureData_Data_SetNull()
		{
			gxTv_SdtMultiSigSignatureData_Data_N = true;
			gxTv_SdtMultiSigSignatureData_Data = null;
		}

		public bool gxTv_SdtMultiSigSignatureData_Data_IsNull()
		{
			return gxTv_SdtMultiSigSignatureData_Data == null;
		}
		public bool ShouldSerializegxTpr_Data_GxSimpleCollection_Json()
		{
			return gxTv_SdtMultiSigSignatureData_Data != null && gxTv_SdtMultiSigSignatureData_Data.Count > 0;

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
			gxTv_SdtMultiSigSignatureData_Contactusername = "";

			gxTv_SdtMultiSigSignatureData_Data_N = true;

			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtMultiSigSignatureData_Contactusername;
		 
		protected bool gxTv_SdtMultiSigSignatureData_Data_N;
		protected GXBaseCollection<SdtMultiSigSignatureData_DataItem> gxTv_SdtMultiSigSignatureData_Data = null; 



		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"MultiSigSignatureData", Namespace="distributedcryptography")]
	public class SdtMultiSigSignatureData_RESTInterface : GxGenericCollectionItem<SdtMultiSigSignatureData>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtMultiSigSignatureData_RESTInterface( ) : base()
		{	
		}

		public SdtMultiSigSignatureData_RESTInterface( SdtMultiSigSignatureData psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("contactUserName")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="contactUserName", Order=0)]
		public  string gxTpr_Contactusername
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Contactusername);

			}
			set { 
				 sdt.gxTpr_Contactusername = value;
			}
		}

		[JsonPropertyName("Data")]
		[JsonPropertyOrder(1)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="Data", Order=1, EmitDefaultValue=false)]
		public GxGenericCollection<SdtMultiSigSignatureData_DataItem_RESTInterface> gxTpr_Data
		{
			get {
				if (sdt.ShouldSerializegxTpr_Data_GxSimpleCollection_Json())
					return new GxGenericCollection<SdtMultiSigSignatureData_DataItem_RESTInterface>(sdt.gxTpr_Data);
				else
					return null;

			}
			set {
				value.LoadCollection(sdt.gxTpr_Data);
			}
		}


		#endregion
		[JsonIgnore]
		public SdtMultiSigSignatureData sdt
		{
			get { 
				return (SdtMultiSigSignatureData)Sdt;
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
				sdt = new SdtMultiSigSignatureData() ;
			}
		}
	}
	#endregion
}