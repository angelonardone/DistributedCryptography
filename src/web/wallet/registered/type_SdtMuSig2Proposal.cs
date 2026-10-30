/*
				   File: type_SdtMuSig2Proposal
			Description: MuSig2Proposal
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
	[XmlRoot(ElementName="MuSig2Proposal")]
	[XmlType(TypeName="MuSig2Proposal" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtMuSig2Proposal : GxUserType
	{
		public SdtMuSig2Proposal( )
		{
			/* Constructor for serialization */
			gxTv_SdtMuSig2Proposal_Sendto = "";

			gxTv_SdtMuSig2Proposal_Description = "";


		}

		public SdtMuSig2Proposal(IGxContext context)
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
			AddObjectProperty("sendTo", gxTpr_Sendto, false);


			AddObjectProperty("amountBtc", StringUtil.LTrim( StringUtil.Str( (decimal)gxTpr_Amountbtc, 16, 8)), false);


			AddObjectProperty("sendAll", gxTpr_Sendall, false);


			AddObjectProperty("description", gxTpr_Description, false);


			AddObjectProperty("gatherDeadlineUnix", StringUtil.LTrim( StringUtil.Str( (decimal)gxTpr_Gatherdeadlineunix, 18, 0)), false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="sendTo")]
		[XmlElement(ElementName="sendTo")]
		public string gxTpr_Sendto
		{
			get {
				return gxTv_SdtMuSig2Proposal_Sendto; 
			}
			set {
				gxTv_SdtMuSig2Proposal_Sendto = value;
				SetDirty("Sendto");
			}
		}



		[SoapElement(ElementName="amountBtc")]
		[XmlElement(ElementName="amountBtc")]
		public string gxTpr_Amountbtc_double
		{
			get {
				return Convert.ToString(gxTv_SdtMuSig2Proposal_Amountbtc, System.Globalization.CultureInfo.InvariantCulture);
			}
			set {
				gxTv_SdtMuSig2Proposal_Amountbtc = NumberUtil.Val(value);
			}
		}
		[XmlIgnore]
		public decimal gxTpr_Amountbtc
		{
			get {
				return gxTv_SdtMuSig2Proposal_Amountbtc; 
			}
			set {
				gxTv_SdtMuSig2Proposal_Amountbtc = value;
				SetDirty("Amountbtc");
			}
		}




		[SoapElement(ElementName="sendAll")]
		[XmlElement(ElementName="sendAll")]
		public bool gxTpr_Sendall
		{
			get {
				return gxTv_SdtMuSig2Proposal_Sendall; 
			}
			set {
				gxTv_SdtMuSig2Proposal_Sendall = value;
				SetDirty("Sendall");
			}
		}




		[SoapElement(ElementName="description")]
		[XmlElement(ElementName="description")]
		public string gxTpr_Description
		{
			get {
				return gxTv_SdtMuSig2Proposal_Description; 
			}
			set {
				gxTv_SdtMuSig2Proposal_Description = value;
				SetDirty("Description");
			}
		}




		[SoapElement(ElementName="gatherDeadlineUnix")]
		[XmlElement(ElementName="gatherDeadlineUnix")]
		public long gxTpr_Gatherdeadlineunix
		{
			get {
				return gxTv_SdtMuSig2Proposal_Gatherdeadlineunix; 
			}
			set {
				gxTv_SdtMuSig2Proposal_Gatherdeadlineunix = value;
				SetDirty("Gatherdeadlineunix");
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
			gxTv_SdtMuSig2Proposal_Sendto = "";


			gxTv_SdtMuSig2Proposal_Description = "";

			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtMuSig2Proposal_Sendto;
		 

		protected decimal gxTv_SdtMuSig2Proposal_Amountbtc;
		 

		protected bool gxTv_SdtMuSig2Proposal_Sendall;
		 

		protected string gxTv_SdtMuSig2Proposal_Description;
		 

		protected long gxTv_SdtMuSig2Proposal_Gatherdeadlineunix;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"MuSig2Proposal", Namespace="distributedcryptography")]
	public class SdtMuSig2Proposal_RESTInterface : GxGenericCollectionItem<SdtMuSig2Proposal>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtMuSig2Proposal_RESTInterface( ) : base()
		{	
		}

		public SdtMuSig2Proposal_RESTInterface( SdtMuSig2Proposal psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("sendTo")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="sendTo", Order=0)]
		public  string gxTpr_Sendto
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Sendto);

			}
			set { 
				 sdt.gxTpr_Sendto = value;
			}
		}

		[JsonPropertyName("amountBtc")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="amountBtc", Order=1)]
		public  string gxTpr_Amountbtc
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str(  sdt.gxTpr_Amountbtc, 16, 8));

			}
			set { 
				sdt.gxTpr_Amountbtc =  NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("sendAll")]
		[JsonPropertyOrder(2)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="sendAll", Order=2)]
		public bool gxTpr_Sendall
		{
			get { 
				return sdt.gxTpr_Sendall;

			}
			set { 
				sdt.gxTpr_Sendall = value;
			}
		}

		[JsonPropertyName("description")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="description", Order=3)]
		public  string gxTpr_Description
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Description);

			}
			set { 
				 sdt.gxTpr_Description = value;
			}
		}

		[JsonPropertyName("gatherDeadlineUnix")]
		[JsonPropertyOrder(4)]
		[DataMember(Name="gatherDeadlineUnix", Order=4)]
		public  string gxTpr_Gatherdeadlineunix
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Gatherdeadlineunix, 18, 0));

			}
			set { 
				sdt.gxTpr_Gatherdeadlineunix = (long) NumberUtil.Val( value, ".");
			}
		}


		#endregion
		[JsonIgnore]
		public SdtMuSig2Proposal sdt
		{
			get { 
				return (SdtMuSig2Proposal)Sdt;
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
				sdt = new SdtMuSig2Proposal() ;
			}
		}
	}
	#endregion
}