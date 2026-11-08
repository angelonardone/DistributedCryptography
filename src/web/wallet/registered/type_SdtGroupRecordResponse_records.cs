/*
				   File: type_SdtGroupRecordResponse_records
			Description: records
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
	[XmlRoot(ElementName="GroupRecordResponse.records")]
	[XmlType(TypeName="GroupRecordResponse.records" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtGroupRecordResponse_records : GxUserType
	{
		public SdtGroupRecordResponse_records( )
		{
			/* Constructor for serialization */
		}

		public SdtGroupRecordResponse_records(IGxContext context)
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
			if (gxTv_SdtGroupRecordResponse_records_Record != null)
			{
				AddObjectProperty("Record", gxTv_SdtGroupRecordResponse_records_Record, false);
			}
			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="Record" )]
		[XmlArray(ElementName="Record"  )]
		[XmlArrayItemAttribute(ElementName="RecordItem" , IsNullable=false )]
		public GXBaseCollection<SdtGroupRecordResponse_records_RecordItem> gxTpr_Record
		{
			get {
				if ( gxTv_SdtGroupRecordResponse_records_Record == null )
				{
					gxTv_SdtGroupRecordResponse_records_Record = new GXBaseCollection<SdtGroupRecordResponse_records_RecordItem>( context, "GroupRecordResponse.records.RecordItem", "");
				}
				SetDirty("Record");
				return gxTv_SdtGroupRecordResponse_records_Record;
			}
			set {
				gxTv_SdtGroupRecordResponse_records_Record_N = false;
				gxTv_SdtGroupRecordResponse_records_Record = value;
				SetDirty("Record");
			}
		}

		public void gxTv_SdtGroupRecordResponse_records_Record_SetNull()
		{
			gxTv_SdtGroupRecordResponse_records_Record_N = true;
			gxTv_SdtGroupRecordResponse_records_Record = null;
		}

		public bool gxTv_SdtGroupRecordResponse_records_Record_IsNull()
		{
			return gxTv_SdtGroupRecordResponse_records_Record == null;
		}
		public bool ShouldSerializegxTpr_Record_GxSimpleCollection_Json()
		{
			return gxTv_SdtGroupRecordResponse_records_Record != null && gxTv_SdtGroupRecordResponse_records_Record.Count > 0;

		}


		public override bool ShouldSerializeSdtJson()
		{
			return (
				ShouldSerializegxTpr_Record_GxSimpleCollection_Json() || 
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
			gxTv_SdtGroupRecordResponse_records_Record_N = true;

			return  ;
		}



		#endregion

		#region Declaration

		protected bool gxTv_SdtGroupRecordResponse_records_Record_N;
		protected GXBaseCollection<SdtGroupRecordResponse_records_RecordItem> gxTv_SdtGroupRecordResponse_records_Record = null; 



		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"GroupRecordResponse.records", Namespace="distributedcryptography")]
	public class SdtGroupRecordResponse_records_RESTInterface : GxGenericCollectionItem<SdtGroupRecordResponse_records>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtGroupRecordResponse_records_RESTInterface( ) : base()
		{	
		}

		public SdtGroupRecordResponse_records_RESTInterface( SdtGroupRecordResponse_records psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("Record")]
		[JsonPropertyOrder(0)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="Record", Order=0, EmitDefaultValue=false)]
		public GxGenericCollection<SdtGroupRecordResponse_records_RecordItem_RESTInterface> gxTpr_Record
		{
			get {
				if (sdt.ShouldSerializegxTpr_Record_GxSimpleCollection_Json())
					return new GxGenericCollection<SdtGroupRecordResponse_records_RecordItem_RESTInterface>(sdt.gxTpr_Record);
				else
					return null;

			}
			set {
				value.LoadCollection(sdt.gxTpr_Record);
			}
		}


		#endregion
		[JsonIgnore]
		public SdtGroupRecordResponse_records sdt
		{
			get { 
				return (SdtGroupRecordResponse_records)Sdt;
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
				sdt = new SdtGroupRecordResponse_records() ;
			}
		}
	}
	#endregion
}