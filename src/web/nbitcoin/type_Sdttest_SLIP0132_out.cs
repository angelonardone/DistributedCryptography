/*
				   File: type_Sdttest_SLIP0132_out
			Description: test_SLIP0132_out
				 Author: Nemo 🐠 for C# (.NET) version 18.0.13.186676
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
namespace GeneXus.Programs.nbitcoin
{
	[XmlRoot(ElementName="test_SLIP0132_out")]
	[XmlType(TypeName="test_SLIP0132_out" , Namespace="distributedcryptography" )]
	[Serializable]
	public class Sdttest_SLIP0132_out : GxUserType
	{
		public Sdttest_SLIP0132_out( )
		{
			/* Constructor for serialization */
			gxTv_Sdttest_SLIP0132_out_Extprivatekey = "";

			gxTv_Sdttest_SLIP0132_out_Extpublickey = "";

			gxTv_Sdttest_SLIP0132_out_Derivedaddress = "";

		}

		public Sdttest_SLIP0132_out(IGxContext context)
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
			AddObjectProperty("ExtPrivateKey", gxTpr_Extprivatekey, false);


			AddObjectProperty("ExtPublicKey", gxTpr_Extpublickey, false);


			AddObjectProperty("DerivedAddress", gxTpr_Derivedaddress, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="ExtPrivateKey")]
		[XmlElement(ElementName="ExtPrivateKey")]
		public string gxTpr_Extprivatekey
		{
			get {
				return gxTv_Sdttest_SLIP0132_out_Extprivatekey; 
			}
			set {
				gxTv_Sdttest_SLIP0132_out_Extprivatekey = value;
				SetDirty("Extprivatekey");
			}
		}




		[SoapElement(ElementName="ExtPublicKey")]
		[XmlElement(ElementName="ExtPublicKey")]
		public string gxTpr_Extpublickey
		{
			get {
				return gxTv_Sdttest_SLIP0132_out_Extpublickey; 
			}
			set {
				gxTv_Sdttest_SLIP0132_out_Extpublickey = value;
				SetDirty("Extpublickey");
			}
		}




		[SoapElement(ElementName="DerivedAddress")]
		[XmlElement(ElementName="DerivedAddress")]
		public string gxTpr_Derivedaddress
		{
			get {
				return gxTv_Sdttest_SLIP0132_out_Derivedaddress; 
			}
			set {
				gxTv_Sdttest_SLIP0132_out_Derivedaddress = value;
				SetDirty("Derivedaddress");
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
			gxTv_Sdttest_SLIP0132_out_Extprivatekey = "";
			gxTv_Sdttest_SLIP0132_out_Extpublickey = "";
			gxTv_Sdttest_SLIP0132_out_Derivedaddress = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_Sdttest_SLIP0132_out_Extprivatekey;
		 

		protected string gxTv_Sdttest_SLIP0132_out_Extpublickey;
		 

		protected string gxTv_Sdttest_SLIP0132_out_Derivedaddress;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"test_SLIP0132_out", Namespace="distributedcryptography")]
	public class Sdttest_SLIP0132_out_RESTInterface : GxGenericCollectionItem<Sdttest_SLIP0132_out>, System.Web.SessionState.IRequiresSessionState
	{
		public Sdttest_SLIP0132_out_RESTInterface( ) : base()
		{	
		}

		public Sdttest_SLIP0132_out_RESTInterface( Sdttest_SLIP0132_out psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("ExtPrivateKey")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="ExtPrivateKey", Order=0)]
		public  string gxTpr_Extprivatekey
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Extprivatekey);

			}
			set { 
				 sdt.gxTpr_Extprivatekey = value;
			}
		}

		[JsonPropertyName("ExtPublicKey")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="ExtPublicKey", Order=1)]
		public  string gxTpr_Extpublickey
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Extpublickey);

			}
			set { 
				 sdt.gxTpr_Extpublickey = value;
			}
		}

		[JsonPropertyName("DerivedAddress")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="DerivedAddress", Order=2)]
		public  string gxTpr_Derivedaddress
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Derivedaddress);

			}
			set { 
				 sdt.gxTpr_Derivedaddress = value;
			}
		}


		#endregion
		[JsonIgnore]
		public Sdttest_SLIP0132_out sdt
		{
			get { 
				return (Sdttest_SLIP0132_out)Sdt;
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
				sdt = new Sdttest_SLIP0132_out() ;
			}
		}
	}
	#endregion
}