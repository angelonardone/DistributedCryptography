/*
				   File: type_SdtvIN
			Description: vIN
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
namespace GeneXus.Programs.sudodb
{
	[XmlRoot(ElementName="vIN")]
	[XmlType(TypeName="vIN" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtvIN : GxUserType
	{
		public SdtvIN( )
		{
			/* Constructor for serialization */
			gxTv_SdtvIN_Vintransactionid = "";

			gxTv_SdtvIN_Transactionid = "";

		}

		public SdtvIN(IGxContext context)
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
			AddObjectProperty("vINTransactionId", gxTpr_Vintransactionid, false);


			AddObjectProperty("vINn", gxTpr_Vinn, false);


			AddObjectProperty("TransactionId", gxTpr_Transactionid, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="vINTransactionId")]
		[XmlElement(ElementName="vINTransactionId")]
		public string gxTpr_Vintransactionid
		{
			get {
				return gxTv_SdtvIN_Vintransactionid; 
			}
			set {
				gxTv_SdtvIN_Vintransactionid = value;
				SetDirty("Vintransactionid");
			}
		}




		[SoapElement(ElementName="vINn")]
		[XmlElement(ElementName="vINn")]
		public long gxTpr_Vinn
		{
			get {
				return gxTv_SdtvIN_Vinn; 
			}
			set {
				gxTv_SdtvIN_Vinn = value;
				SetDirty("Vinn");
			}
		}




		[SoapElement(ElementName="TransactionId")]
		[XmlElement(ElementName="TransactionId")]
		public string gxTpr_Transactionid
		{
			get {
				return gxTv_SdtvIN_Transactionid; 
			}
			set {
				gxTv_SdtvIN_Transactionid = value;
				SetDirty("Transactionid");
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
			gxTv_SdtvIN_Vintransactionid = "";

			gxTv_SdtvIN_Transactionid = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtvIN_Vintransactionid;
		 

		protected long gxTv_SdtvIN_Vinn;
		 

		protected string gxTv_SdtvIN_Transactionid;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"vIN", Namespace="distributedcryptography")]
	public class SdtvIN_RESTInterface : GxGenericCollectionItem<SdtvIN>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtvIN_RESTInterface( ) : base()
		{	
		}

		public SdtvIN_RESTInterface( SdtvIN psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("vINTransactionId")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="vINTransactionId", Order=0)]
		public  string gxTpr_Vintransactionid
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Vintransactionid);

			}
			set { 
				 sdt.gxTpr_Vintransactionid = value;
			}
		}

		[JsonPropertyName("vINn")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="vINn", Order=1)]
		public  string gxTpr_Vinn
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Vinn, 10, 0));

			}
			set { 
				sdt.gxTpr_Vinn = (long) NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("TransactionId")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="TransactionId", Order=2)]
		public  string gxTpr_Transactionid
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Transactionid);

			}
			set { 
				 sdt.gxTpr_Transactionid = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtvIN sdt
		{
			get { 
				return (SdtvIN)Sdt;
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
				sdt = new SdtvIN() ;
			}
		}
	}
	#endregion
}