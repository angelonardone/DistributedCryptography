/*
				   File: type_SdtTransactionLine
			Description: TransactionLine
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

namespace GeneXus.Programs.wallet
{
	[XmlRoot(ElementName="TransactionLine")]
	[XmlType(TypeName="TransactionLine" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtTransactionLine : GxUserType
	{
		public SdtTransactionLine( )
		{
			/* Constructor for serialization */
			gxTv_SdtTransactionLine_Outpoint = "";

			gxTv_SdtTransactionLine_Address = "";

			gxTv_SdtTransactionLine_Valuetext = "";

			gxTv_SdtTransactionLine_Owner = "";

		}

		public SdtTransactionLine(IGxContext context)
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
			AddObjectProperty("n", gxTpr_N, false);


			AddObjectProperty("outpoint", gxTpr_Outpoint, false);


			AddObjectProperty("address", gxTpr_Address, false);


			AddObjectProperty("value", StringUtil.LTrim( StringUtil.Str( (decimal)gxTpr_Value, 16, 8)), false);


			AddObjectProperty("valueText", gxTpr_Valuetext, false);


			AddObjectProperty("owner", gxTpr_Owner, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="n")]
		[XmlElement(ElementName="n")]
		public long gxTpr_N
		{
			get {
				return gxTv_SdtTransactionLine_N; 
			}
			set {
				gxTv_SdtTransactionLine_N = value;
				SetDirty("N");
			}
		}




		[SoapElement(ElementName="outpoint")]
		[XmlElement(ElementName="outpoint")]
		public string gxTpr_Outpoint
		{
			get {
				return gxTv_SdtTransactionLine_Outpoint; 
			}
			set {
				gxTv_SdtTransactionLine_Outpoint = value;
				SetDirty("Outpoint");
			}
		}




		[SoapElement(ElementName="address")]
		[XmlElement(ElementName="address")]
		public string gxTpr_Address
		{
			get {
				return gxTv_SdtTransactionLine_Address; 
			}
			set {
				gxTv_SdtTransactionLine_Address = value;
				SetDirty("Address");
			}
		}



		[SoapElement(ElementName="value")]
		[XmlElement(ElementName="value")]
		public string gxTpr_Value_double
		{
			get {
				return Convert.ToString(gxTv_SdtTransactionLine_Value, System.Globalization.CultureInfo.InvariantCulture);
			}
			set {
				gxTv_SdtTransactionLine_Value = NumberUtil.Val(value);
			}
		}
		[XmlIgnore]
		public decimal gxTpr_Value
		{
			get {
				return gxTv_SdtTransactionLine_Value; 
			}
			set {
				gxTv_SdtTransactionLine_Value = value;
				SetDirty("Value");
			}
		}




		[SoapElement(ElementName="valueText")]
		[XmlElement(ElementName="valueText")]
		public string gxTpr_Valuetext
		{
			get {
				return gxTv_SdtTransactionLine_Valuetext; 
			}
			set {
				gxTv_SdtTransactionLine_Valuetext = value;
				SetDirty("Valuetext");
			}
		}




		[SoapElement(ElementName="owner")]
		[XmlElement(ElementName="owner")]
		public string gxTpr_Owner
		{
			get {
				return gxTv_SdtTransactionLine_Owner; 
			}
			set {
				gxTv_SdtTransactionLine_Owner = value;
				SetDirty("Owner");
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
			gxTv_SdtTransactionLine_Outpoint = "";
			gxTv_SdtTransactionLine_Address = "";

			gxTv_SdtTransactionLine_Valuetext = "";
			gxTv_SdtTransactionLine_Owner = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected long gxTv_SdtTransactionLine_N;
		 

		protected string gxTv_SdtTransactionLine_Outpoint;
		 

		protected string gxTv_SdtTransactionLine_Address;
		 

		protected decimal gxTv_SdtTransactionLine_Value;
		 

		protected string gxTv_SdtTransactionLine_Valuetext;
		 

		protected string gxTv_SdtTransactionLine_Owner;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"TransactionLine", Namespace="distributedcryptography")]
	public class SdtTransactionLine_RESTInterface : GxGenericCollectionItem<SdtTransactionLine>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtTransactionLine_RESTInterface( ) : base()
		{	
		}

		public SdtTransactionLine_RESTInterface( SdtTransactionLine psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("n")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="n", Order=0)]
		public  string gxTpr_N
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_N, 10, 0));

			}
			set { 
				sdt.gxTpr_N = (long) NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("outpoint")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="outpoint", Order=1)]
		public  string gxTpr_Outpoint
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Outpoint);

			}
			set { 
				 sdt.gxTpr_Outpoint = value;
			}
		}

		[JsonPropertyName("address")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="address", Order=2)]
		public  string gxTpr_Address
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Address);

			}
			set { 
				 sdt.gxTpr_Address = value;
			}
		}

		[JsonPropertyName("value")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="value", Order=3)]
		public  string gxTpr_Value
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str(  sdt.gxTpr_Value, 16, 8));

			}
			set { 
				sdt.gxTpr_Value =  NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("valueText")]
		[JsonPropertyOrder(4)]
		[DataMember(Name="valueText", Order=4)]
		public  string gxTpr_Valuetext
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Valuetext);

			}
			set { 
				 sdt.gxTpr_Valuetext = value;
			}
		}

		[JsonPropertyName("owner")]
		[JsonPropertyOrder(5)]
		[DataMember(Name="owner", Order=5)]
		public  string gxTpr_Owner
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Owner);

			}
			set { 
				 sdt.gxTpr_Owner = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtTransactionLine sdt
		{
			get { 
				return (SdtTransactionLine)Sdt;
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
				sdt = new SdtTransactionLine() ;
			}
		}
	}
	#endregion
}