/*
				   File: type_Sdttest_BIP32_in
			Description: test_BIP32_in
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
	[XmlRoot(ElementName="test_BIP32_in")]
	[XmlType(TypeName="test_BIP32_in" , Namespace="distributedcryptography" )]
	[Serializable]
	public class Sdttest_BIP32_in : GxUserType
	{
		public Sdttest_BIP32_in( )
		{
			/* Constructor for serialization */
			gxTv_Sdttest_BIP32_in_Seed = "";

			gxTv_Sdttest_BIP32_in_Path = "";

		}

		public Sdttest_BIP32_in(IGxContext context)
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
			AddObjectProperty("seed", gxTpr_Seed, false);


			AddObjectProperty("path", gxTpr_Path, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="seed")]
		[XmlElement(ElementName="seed")]
		public string gxTpr_Seed
		{
			get {
				return gxTv_Sdttest_BIP32_in_Seed; 
			}
			set {
				gxTv_Sdttest_BIP32_in_Seed = value;
				SetDirty("Seed");
			}
		}




		[SoapElement(ElementName="path")]
		[XmlElement(ElementName="path")]
		public string gxTpr_Path
		{
			get {
				return gxTv_Sdttest_BIP32_in_Path; 
			}
			set {
				gxTv_Sdttest_BIP32_in_Path = value;
				SetDirty("Path");
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
			gxTv_Sdttest_BIP32_in_Seed = "";
			gxTv_Sdttest_BIP32_in_Path = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_Sdttest_BIP32_in_Seed;
		 

		protected string gxTv_Sdttest_BIP32_in_Path;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"test_BIP32_in", Namespace="distributedcryptography")]
	public class Sdttest_BIP32_in_RESTInterface : GxGenericCollectionItem<Sdttest_BIP32_in>, System.Web.SessionState.IRequiresSessionState
	{
		public Sdttest_BIP32_in_RESTInterface( ) : base()
		{	
		}

		public Sdttest_BIP32_in_RESTInterface( Sdttest_BIP32_in psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("seed")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="seed", Order=0)]
		public  string gxTpr_Seed
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Seed);

			}
			set { 
				 sdt.gxTpr_Seed = value;
			}
		}

		[JsonPropertyName("path")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="path", Order=1)]
		public  string gxTpr_Path
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Path);

			}
			set { 
				 sdt.gxTpr_Path = value;
			}
		}


		#endregion
		[JsonIgnore]
		public Sdttest_BIP32_in sdt
		{
			get { 
				return (Sdttest_BIP32_in)Sdt;
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
				sdt = new Sdttest_BIP32_in() ;
			}
		}
	}
	#endregion
}