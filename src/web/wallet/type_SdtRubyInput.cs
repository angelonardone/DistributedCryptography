/*
				   File: type_SdtRubyInput
			Description: RubyInput
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
	[XmlRoot(ElementName="RubyInput")]
	[XmlType(TypeName="RubyInput" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtRubyInput : GxUserType
	{
		public SdtRubyInput( )
		{
			/* Constructor for serialization */
			gxTv_SdtRubyInput_Txhex = "";


		}

		public SdtRubyInput(IGxContext context)
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
			AddObjectProperty("TxHex", gxTpr_Txhex, false);


			AddObjectProperty("Vout", gxTpr_Vout, false);


			AddObjectProperty("Chain", gxTpr_Chain, false);


			AddObjectProperty("Index", gxTpr_Index, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="TxHex")]
		[XmlElement(ElementName="TxHex")]
		public string gxTpr_Txhex
		{
			get {
				return gxTv_SdtRubyInput_Txhex; 
			}
			set {
				gxTv_SdtRubyInput_Txhex = value;
				SetDirty("Txhex");
			}
		}




		[SoapElement(ElementName="Vout")]
		[XmlElement(ElementName="Vout")]
		public long gxTpr_Vout
		{
			get {
				return gxTv_SdtRubyInput_Vout; 
			}
			set {
				gxTv_SdtRubyInput_Vout = value;
				SetDirty("Vout");
			}
		}




		[SoapElement(ElementName="Chain")]
		[XmlElement(ElementName="Chain")]
		public short gxTpr_Chain
		{
			get {
				return gxTv_SdtRubyInput_Chain; 
			}
			set {
				gxTv_SdtRubyInput_Chain = value;
				SetDirty("Chain");
			}
		}




		[SoapElement(ElementName="Index")]
		[XmlElement(ElementName="Index")]
		public long gxTpr_Index
		{
			get {
				return gxTv_SdtRubyInput_Index; 
			}
			set {
				gxTv_SdtRubyInput_Index = value;
				SetDirty("Index");
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
			gxTv_SdtRubyInput_Txhex = "";



			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtRubyInput_Txhex;
		 

		protected long gxTv_SdtRubyInput_Vout;
		 

		protected short gxTv_SdtRubyInput_Chain;
		 

		protected long gxTv_SdtRubyInput_Index;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"RubyInput", Namespace="distributedcryptography")]
	public class SdtRubyInput_RESTInterface : GxGenericCollectionItem<SdtRubyInput>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtRubyInput_RESTInterface( ) : base()
		{	
		}

		public SdtRubyInput_RESTInterface( SdtRubyInput psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("TxHex")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="TxHex", Order=0)]
		public  string gxTpr_Txhex
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Txhex);

			}
			set { 
				 sdt.gxTpr_Txhex = value;
			}
		}

		[JsonPropertyName("Vout")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="Vout", Order=1)]
		public  string gxTpr_Vout
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Vout, 10, 0));

			}
			set { 
				sdt.gxTpr_Vout = (long) NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("Chain")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="Chain", Order=2)]
		public short gxTpr_Chain
		{
			get { 
				return sdt.gxTpr_Chain;

			}
			set { 
				sdt.gxTpr_Chain = value;
			}
		}

		[JsonPropertyName("Index")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="Index", Order=3)]
		public  string gxTpr_Index
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Index, 10, 0));

			}
			set { 
				sdt.gxTpr_Index = (long) NumberUtil.Val( value, ".");
			}
		}


		#endregion
		[JsonIgnore]
		public SdtRubyInput sdt
		{
			get { 
				return (SdtRubyInput)Sdt;
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
				sdt = new SdtRubyInput() ;
			}
		}
	}
	#endregion
}