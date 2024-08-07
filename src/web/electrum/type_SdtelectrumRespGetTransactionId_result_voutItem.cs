/*
				   File: type_SdtelectrumRespGetTransactionId_result_voutItem
			Description: vout
				 Author: Nemo 🐠 for C# (.NET) version 18.0.8.180599
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
namespace GeneXus.Programs.electrum
{
	[XmlRoot(ElementName="electrumRespGetTransactionId.result.voutItem")]
	[XmlType(TypeName="electrumRespGetTransactionId.result.voutItem" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtelectrumRespGetTransactionId_result_voutItem : GxUserType
	{
		public SdtelectrumRespGetTransactionId_result_voutItem( )
		{
			/* Constructor for serialization */
		}

		public SdtelectrumRespGetTransactionId_result_voutItem(IGxContext context)
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

			if (gxTv_SdtelectrumRespGetTransactionId_result_voutItem_Scriptpubkey != null)
			{
				AddObjectProperty("scriptPubKey", gxTv_SdtelectrumRespGetTransactionId_result_voutItem_Scriptpubkey, false);
			}

			AddObjectProperty("value", gxTpr_Value, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="n")]
		[XmlElement(ElementName="n")]
		public string gxTpr_N_double
		{
			get {
				return Convert.ToString(gxTv_SdtelectrumRespGetTransactionId_result_voutItem_N, System.Globalization.CultureInfo.InvariantCulture);
			}
			set {
				gxTv_SdtelectrumRespGetTransactionId_result_voutItem_N = (decimal)(Convert.ToDecimal(value, System.Globalization.CultureInfo.InvariantCulture));
			}
		}
		[XmlIgnore]
		public decimal gxTpr_N
		{
			get {
				return gxTv_SdtelectrumRespGetTransactionId_result_voutItem_N; 
			}
			set {
				gxTv_SdtelectrumRespGetTransactionId_result_voutItem_N = value;
				SetDirty("N");
			}
		}



		[SoapElement(ElementName="scriptPubKey" )]
		[XmlElement(ElementName="scriptPubKey" )]
		public SdtelectrumRespGetTransactionId_result_voutItem_scriptPubKey gxTpr_Scriptpubkey
		{
			get {
				if ( gxTv_SdtelectrumRespGetTransactionId_result_voutItem_Scriptpubkey == null )
				{
					gxTv_SdtelectrumRespGetTransactionId_result_voutItem_Scriptpubkey = new SdtelectrumRespGetTransactionId_result_voutItem_scriptPubKey(context);
				}
				gxTv_SdtelectrumRespGetTransactionId_result_voutItem_Scriptpubkey_N = false;
				return gxTv_SdtelectrumRespGetTransactionId_result_voutItem_Scriptpubkey;
			}
			set {
				gxTv_SdtelectrumRespGetTransactionId_result_voutItem_Scriptpubkey_N = false;
				gxTv_SdtelectrumRespGetTransactionId_result_voutItem_Scriptpubkey = value;
				SetDirty("Scriptpubkey");
			}

		}

		public void gxTv_SdtelectrumRespGetTransactionId_result_voutItem_Scriptpubkey_SetNull()
		{
			gxTv_SdtelectrumRespGetTransactionId_result_voutItem_Scriptpubkey_N = true;
			gxTv_SdtelectrumRespGetTransactionId_result_voutItem_Scriptpubkey = null;
		}

		public bool gxTv_SdtelectrumRespGetTransactionId_result_voutItem_Scriptpubkey_IsNull()
		{
			return gxTv_SdtelectrumRespGetTransactionId_result_voutItem_Scriptpubkey == null;
		}
		public bool ShouldSerializegxTpr_Scriptpubkey_Json()
		{
				return (gxTv_SdtelectrumRespGetTransactionId_result_voutItem_Scriptpubkey != null && gxTv_SdtelectrumRespGetTransactionId_result_voutItem_Scriptpubkey.ShouldSerializeSdtJson());

		}


		[SoapElement(ElementName="value")]
		[XmlElement(ElementName="value")]
		public string gxTpr_Value_double
		{
			get {
				return Convert.ToString(gxTv_SdtelectrumRespGetTransactionId_result_voutItem_Value, System.Globalization.CultureInfo.InvariantCulture);
			}
			set {
				gxTv_SdtelectrumRespGetTransactionId_result_voutItem_Value = (decimal)(Convert.ToDecimal(value, System.Globalization.CultureInfo.InvariantCulture));
			}
		}
		[XmlIgnore]
		public decimal gxTpr_Value
		{
			get {
				return gxTv_SdtelectrumRespGetTransactionId_result_voutItem_Value; 
			}
			set {
				gxTv_SdtelectrumRespGetTransactionId_result_voutItem_Value = value;
				SetDirty("Value");
			}
		}



		public override bool ShouldSerializeSdtJson()
		{
			return true;
		}



		#endregion

		#region Initialization

		public void initialize( )
		{
			gxTv_SdtelectrumRespGetTransactionId_result_voutItem_Scriptpubkey_N = true;


			return  ;
		}



		#endregion

		#region Declaration

		protected decimal gxTv_SdtelectrumRespGetTransactionId_result_voutItem_N;
		 
		protected bool gxTv_SdtelectrumRespGetTransactionId_result_voutItem_Scriptpubkey_N;
		protected SdtelectrumRespGetTransactionId_result_voutItem_scriptPubKey gxTv_SdtelectrumRespGetTransactionId_result_voutItem_Scriptpubkey = null; 


		protected decimal gxTv_SdtelectrumRespGetTransactionId_result_voutItem_Value;
		 


		#endregion
	}
	#region Rest interface
	[DataContract(Name=@"electrumRespGetTransactionId.result.voutItem", Namespace="distributedcryptography")]
	public class SdtelectrumRespGetTransactionId_result_voutItem_RESTInterface : GxGenericCollectionItem<SdtelectrumRespGetTransactionId_result_voutItem>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtelectrumRespGetTransactionId_result_voutItem_RESTInterface( ) : base()
		{	
		}

		public SdtelectrumRespGetTransactionId_result_voutItem_RESTInterface( SdtelectrumRespGetTransactionId_result_voutItem psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="n", Order=0)]
		public  string gxTpr_N
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str(  sdt.gxTpr_N, 10, 5));

			}
			set { 
				sdt.gxTpr_N =  NumberUtil.Val( value, ".");
			}
		}

		[DataMember(Name="scriptPubKey", Order=1, EmitDefaultValue=false)]
		public SdtelectrumRespGetTransactionId_result_voutItem_scriptPubKey_RESTInterface gxTpr_Scriptpubkey
		{
			get {
				if (sdt.ShouldSerializegxTpr_Scriptpubkey_Json())
					return new SdtelectrumRespGetTransactionId_result_voutItem_scriptPubKey_RESTInterface(sdt.gxTpr_Scriptpubkey);
				else
					return null;

			}

			set {
				sdt.gxTpr_Scriptpubkey = value.sdt;
			}

		}

		[DataMember(Name="value", Order=2)]
		public  string gxTpr_Value
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str(  sdt.gxTpr_Value, 10, 5));

			}
			set { 
				sdt.gxTpr_Value =  NumberUtil.Val( value, ".");
			}
		}


		#endregion

		public SdtelectrumRespGetTransactionId_result_voutItem sdt
		{
			get { 
				return (SdtelectrumRespGetTransactionId_result_voutItem)Sdt;
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
				sdt = new SdtelectrumRespGetTransactionId_result_voutItem() ;
			}
		}
	}
	#endregion
}