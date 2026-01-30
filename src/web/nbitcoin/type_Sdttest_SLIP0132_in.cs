/*
				   File: type_Sdttest_SLIP0132_in
			Description: test_SLIP0132_in
				 Author: Nemo 🐠 for C# (.NET) version 18.0.14.187820
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
	[XmlRoot(ElementName="test_SLIP0132_in")]
	[XmlType(TypeName="test_SLIP0132_in" , Namespace="distributedcryptography" )]
	[Serializable]
	public class Sdttest_SLIP0132_in : GxUserType
	{
		public Sdttest_SLIP0132_in( )
		{
			/* Constructor for serialization */
			gxTv_Sdttest_SLIP0132_in_Mnemonic = "";

			gxTv_Sdttest_SLIP0132_in_Networktype = "";

			gxTv_Sdttest_SLIP0132_in_Rootpath = "";

			gxTv_Sdttest_SLIP0132_in_Derivepathaddress = "";


		}

		public Sdttest_SLIP0132_in(IGxContext context)
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
			AddObjectProperty("Mnemonic", gxTpr_Mnemonic, false);


			AddObjectProperty("NetworkType", gxTpr_Networktype, false);


			AddObjectProperty("RootPath", gxTpr_Rootpath, false);


			AddObjectProperty("AddressType", gxTpr_Addresstype, false);


			AddObjectProperty("DerivePathAddress", gxTpr_Derivepathaddress, false);


			AddObjectProperty("testMethod", gxTpr_Testmethod, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="Mnemonic")]
		[XmlElement(ElementName="Mnemonic")]
		public string gxTpr_Mnemonic
		{
			get {
				return gxTv_Sdttest_SLIP0132_in_Mnemonic; 
			}
			set {
				gxTv_Sdttest_SLIP0132_in_Mnemonic = value;
				SetDirty("Mnemonic");
			}
		}




		[SoapElement(ElementName="NetworkType")]
		[XmlElement(ElementName="NetworkType")]
		public string gxTpr_Networktype
		{
			get {
				return gxTv_Sdttest_SLIP0132_in_Networktype; 
			}
			set {
				gxTv_Sdttest_SLIP0132_in_Networktype = value;
				SetDirty("Networktype");
			}
		}




		[SoapElement(ElementName="RootPath")]
		[XmlElement(ElementName="RootPath")]
		public string gxTpr_Rootpath
		{
			get {
				return gxTv_Sdttest_SLIP0132_in_Rootpath; 
			}
			set {
				gxTv_Sdttest_SLIP0132_in_Rootpath = value;
				SetDirty("Rootpath");
			}
		}




		[SoapElement(ElementName="AddressType")]
		[XmlElement(ElementName="AddressType")]
		public short gxTpr_Addresstype
		{
			get {
				return gxTv_Sdttest_SLIP0132_in_Addresstype; 
			}
			set {
				gxTv_Sdttest_SLIP0132_in_Addresstype = value;
				SetDirty("Addresstype");
			}
		}




		[SoapElement(ElementName="DerivePathAddress")]
		[XmlElement(ElementName="DerivePathAddress")]
		public string gxTpr_Derivepathaddress
		{
			get {
				return gxTv_Sdttest_SLIP0132_in_Derivepathaddress; 
			}
			set {
				gxTv_Sdttest_SLIP0132_in_Derivepathaddress = value;
				SetDirty("Derivepathaddress");
			}
		}




		[SoapElement(ElementName="testMethod")]
		[XmlElement(ElementName="testMethod")]
		public short gxTpr_Testmethod
		{
			get {
				return gxTv_Sdttest_SLIP0132_in_Testmethod; 
			}
			set {
				gxTv_Sdttest_SLIP0132_in_Testmethod = value;
				SetDirty("Testmethod");
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
			gxTv_Sdttest_SLIP0132_in_Mnemonic = "";
			gxTv_Sdttest_SLIP0132_in_Networktype = "";
			gxTv_Sdttest_SLIP0132_in_Rootpath = "";

			gxTv_Sdttest_SLIP0132_in_Derivepathaddress = "";

			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_Sdttest_SLIP0132_in_Mnemonic;
		 

		protected string gxTv_Sdttest_SLIP0132_in_Networktype;
		 

		protected string gxTv_Sdttest_SLIP0132_in_Rootpath;
		 

		protected short gxTv_Sdttest_SLIP0132_in_Addresstype;
		 

		protected string gxTv_Sdttest_SLIP0132_in_Derivepathaddress;
		 

		protected short gxTv_Sdttest_SLIP0132_in_Testmethod;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"test_SLIP0132_in", Namespace="distributedcryptography")]
	public class Sdttest_SLIP0132_in_RESTInterface : GxGenericCollectionItem<Sdttest_SLIP0132_in>, System.Web.SessionState.IRequiresSessionState
	{
		public Sdttest_SLIP0132_in_RESTInterface( ) : base()
		{	
		}

		public Sdttest_SLIP0132_in_RESTInterface( Sdttest_SLIP0132_in psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("Mnemonic")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="Mnemonic", Order=0)]
		public  string gxTpr_Mnemonic
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Mnemonic);

			}
			set { 
				 sdt.gxTpr_Mnemonic = value;
			}
		}

		[JsonPropertyName("NetworkType")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="NetworkType", Order=1)]
		public  string gxTpr_Networktype
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Networktype);

			}
			set { 
				 sdt.gxTpr_Networktype = value;
			}
		}

		[JsonPropertyName("RootPath")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="RootPath", Order=2)]
		public  string gxTpr_Rootpath
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Rootpath);

			}
			set { 
				 sdt.gxTpr_Rootpath = value;
			}
		}

		[JsonPropertyName("AddressType")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="AddressType", Order=3)]
		public short gxTpr_Addresstype
		{
			get { 
				return sdt.gxTpr_Addresstype;

			}
			set { 
				sdt.gxTpr_Addresstype = value;
			}
		}

		[JsonPropertyName("DerivePathAddress")]
		[JsonPropertyOrder(4)]
		[DataMember(Name="DerivePathAddress", Order=4)]
		public  string gxTpr_Derivepathaddress
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Derivepathaddress);

			}
			set { 
				 sdt.gxTpr_Derivepathaddress = value;
			}
		}

		[JsonPropertyName("testMethod")]
		[JsonPropertyOrder(5)]
		[DataMember(Name="testMethod", Order=5)]
		public short gxTpr_Testmethod
		{
			get { 
				return sdt.gxTpr_Testmethod;

			}
			set { 
				sdt.gxTpr_Testmethod = value;
			}
		}


		#endregion
		[JsonIgnore]
		public Sdttest_SLIP0132_in sdt
		{
			get { 
				return (Sdttest_SLIP0132_in)Sdt;
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
				sdt = new Sdttest_SLIP0132_in() ;
			}
		}
	}
	#endregion
}