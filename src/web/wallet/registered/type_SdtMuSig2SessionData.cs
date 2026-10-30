/*
				   File: type_SdtMuSig2SessionData
			Description: MuSig2SessionData
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

using GeneXus.Programs;
using GeneXus.Programs.wallet;

namespace GeneXus.Programs.wallet.registered
{
	[XmlRoot(ElementName="MuSig2SessionData")]
	[XmlType(TypeName="MuSig2SessionData" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtMuSig2SessionData : GxUserType
	{
		public SdtMuSig2SessionData( )
		{
			/* Constructor for serialization */
			gxTv_SdtMuSig2SessionData_Path = "";

			gxTv_SdtMuSig2SessionData_Tx = "";


		}

		public SdtMuSig2SessionData(IGxContext context)
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
			AddObjectProperty("Version", gxTpr_Version, false);


			AddObjectProperty("Path", gxTpr_Path, false);


			AddObjectProperty("Tx", gxTpr_Tx, false);


			AddObjectProperty("ChangeSequence", gxTpr_Changesequence, false);

			if (gxTv_SdtMuSig2SessionData_Inputs != null)
			{
				AddObjectProperty("Inputs", gxTv_SdtMuSig2SessionData_Inputs, false);
			}
			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="Version")]
		[XmlElement(ElementName="Version")]
		public short gxTpr_Version
		{
			get {
				return gxTv_SdtMuSig2SessionData_Version; 
			}
			set {
				gxTv_SdtMuSig2SessionData_Version = value;
				SetDirty("Version");
			}
		}




		[SoapElement(ElementName="Path")]
		[XmlElement(ElementName="Path")]
		public string gxTpr_Path
		{
			get {
				return gxTv_SdtMuSig2SessionData_Path; 
			}
			set {
				gxTv_SdtMuSig2SessionData_Path = value;
				SetDirty("Path");
			}
		}




		[SoapElement(ElementName="Tx")]
		[XmlElement(ElementName="Tx")]
		public string gxTpr_Tx
		{
			get {
				return gxTv_SdtMuSig2SessionData_Tx; 
			}
			set {
				gxTv_SdtMuSig2SessionData_Tx = value;
				SetDirty("Tx");
			}
		}




		[SoapElement(ElementName="ChangeSequence")]
		[XmlElement(ElementName="ChangeSequence")]
		public long gxTpr_Changesequence
		{
			get {
				return gxTv_SdtMuSig2SessionData_Changesequence; 
			}
			set {
				gxTv_SdtMuSig2SessionData_Changesequence = value;
				SetDirty("Changesequence");
			}
		}




		[SoapElement(ElementName="Inputs" )]
		[XmlArray(ElementName="Inputs"  )]
		[XmlArrayItemAttribute(ElementName="InputsItem" , IsNullable=false )]
		public GXBaseCollection<SdtMuSig2SessionData_InputsItem> gxTpr_Inputs
		{
			get {
				if ( gxTv_SdtMuSig2SessionData_Inputs == null )
				{
					gxTv_SdtMuSig2SessionData_Inputs = new GXBaseCollection<SdtMuSig2SessionData_InputsItem>( context, "MuSig2SessionData.InputsItem", "");
				}
				SetDirty("Inputs");
				return gxTv_SdtMuSig2SessionData_Inputs;
			}
			set {
				gxTv_SdtMuSig2SessionData_Inputs_N = false;
				gxTv_SdtMuSig2SessionData_Inputs = value;
				SetDirty("Inputs");
			}
		}

		public void gxTv_SdtMuSig2SessionData_Inputs_SetNull()
		{
			gxTv_SdtMuSig2SessionData_Inputs_N = true;
			gxTv_SdtMuSig2SessionData_Inputs = null;
		}

		public bool gxTv_SdtMuSig2SessionData_Inputs_IsNull()
		{
			return gxTv_SdtMuSig2SessionData_Inputs == null;
		}
		public bool ShouldSerializegxTpr_Inputs_GxSimpleCollection_Json()
		{
			return gxTv_SdtMuSig2SessionData_Inputs != null && gxTv_SdtMuSig2SessionData_Inputs.Count > 0;

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
			gxTv_SdtMuSig2SessionData_Path = "";
			gxTv_SdtMuSig2SessionData_Tx = "";


			gxTv_SdtMuSig2SessionData_Inputs_N = true;

			return  ;
		}



		#endregion

		#region Declaration

		protected short gxTv_SdtMuSig2SessionData_Version;
		 

		protected string gxTv_SdtMuSig2SessionData_Path;
		 

		protected string gxTv_SdtMuSig2SessionData_Tx;
		 

		protected long gxTv_SdtMuSig2SessionData_Changesequence;
		 
		protected bool gxTv_SdtMuSig2SessionData_Inputs_N;
		protected GXBaseCollection<SdtMuSig2SessionData_InputsItem> gxTv_SdtMuSig2SessionData_Inputs = null; 



		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"MuSig2SessionData", Namespace="distributedcryptography")]
	public class SdtMuSig2SessionData_RESTInterface : GxGenericCollectionItem<SdtMuSig2SessionData>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtMuSig2SessionData_RESTInterface( ) : base()
		{	
		}

		public SdtMuSig2SessionData_RESTInterface( SdtMuSig2SessionData psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("Version")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="Version", Order=0)]
		public short gxTpr_Version
		{
			get { 
				return sdt.gxTpr_Version;

			}
			set { 
				sdt.gxTpr_Version = value;
			}
		}

		[JsonPropertyName("Path")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="Path", Order=1)]
		public  string gxTpr_Path
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Path);

			}
			set { 
				 sdt.gxTpr_Path = value;
			}
		}

		[JsonPropertyName("Tx")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="Tx", Order=2)]
		public  string gxTpr_Tx
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Tx);

			}
			set { 
				 sdt.gxTpr_Tx = value;
			}
		}

		[JsonPropertyName("ChangeSequence")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="ChangeSequence", Order=3)]
		public  string gxTpr_Changesequence
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Changesequence, 10, 0));

			}
			set { 
				sdt.gxTpr_Changesequence = (long) NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("Inputs")]
		[JsonPropertyOrder(4)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="Inputs", Order=4, EmitDefaultValue=false)]
		public GxGenericCollection<SdtMuSig2SessionData_InputsItem_RESTInterface> gxTpr_Inputs
		{
			get {
				if (sdt.ShouldSerializegxTpr_Inputs_GxSimpleCollection_Json())
					return new GxGenericCollection<SdtMuSig2SessionData_InputsItem_RESTInterface>(sdt.gxTpr_Inputs);
				else
					return null;

			}
			set {
				value.LoadCollection(sdt.gxTpr_Inputs);
			}
		}


		#endregion
		[JsonIgnore]
		public SdtMuSig2SessionData sdt
		{
			get { 
				return (SdtMuSig2SessionData)Sdt;
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
				sdt = new SdtMuSig2SessionData() ;
			}
		}
	}
	#endregion
}