/*
				   File: type_Sdttest_BIP32_out
			Description: test_BIP32_out
				 Author: Nemo 🐠 for C# (.NET) version 18.0.10.184260
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
namespace GeneXus.Programs.nbitcoin
{
	[XmlRoot(ElementName="test_BIP32_out")]
	[XmlType(TypeName="test_BIP32_out" , Namespace="distributedcryptography" )]
	[Serializable]
	public class Sdttest_BIP32_out : GxUserType
	{
		public Sdttest_BIP32_out( )
		{
			/* Constructor for serialization */
			gxTv_Sdttest_BIP32_out_Xprv = "";

			gxTv_Sdttest_BIP32_out_Xpub = "";

		}

		public Sdttest_BIP32_out(IGxContext context)
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
			AddObjectProperty("xprv", gxTpr_Xprv, false);


			AddObjectProperty("xpub", gxTpr_Xpub, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="xprv")]
		[XmlElement(ElementName="xprv")]
		public string gxTpr_Xprv
		{
			get {
				return gxTv_Sdttest_BIP32_out_Xprv; 
			}
			set {
				gxTv_Sdttest_BIP32_out_Xprv = value;
				SetDirty("Xprv");
			}
		}




		[SoapElement(ElementName="xpub")]
		[XmlElement(ElementName="xpub")]
		public string gxTpr_Xpub
		{
			get {
				return gxTv_Sdttest_BIP32_out_Xpub; 
			}
			set {
				gxTv_Sdttest_BIP32_out_Xpub = value;
				SetDirty("Xpub");
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
			gxTv_Sdttest_BIP32_out_Xprv = "";
			gxTv_Sdttest_BIP32_out_Xpub = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_Sdttest_BIP32_out_Xprv;
		 

		protected string gxTv_Sdttest_BIP32_out_Xpub;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"test_BIP32_out", Namespace="distributedcryptography")]
	public class Sdttest_BIP32_out_RESTInterface : GxGenericCollectionItem<Sdttest_BIP32_out>, System.Web.SessionState.IRequiresSessionState
	{
		public Sdttest_BIP32_out_RESTInterface( ) : base()
		{	
		}

		public Sdttest_BIP32_out_RESTInterface( Sdttest_BIP32_out psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="xprv", Order=0)]
		public  string gxTpr_Xprv
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Xprv);

			}
			set { 
				 sdt.gxTpr_Xprv = value;
			}
		}

		[DataMember(Name="xpub", Order=1)]
		public  string gxTpr_Xpub
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Xpub);

			}
			set { 
				 sdt.gxTpr_Xpub = value;
			}
		}


		#endregion

		public Sdttest_BIP32_out sdt
		{
			get { 
				return (Sdttest_BIP32_out)Sdt;
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
				sdt = new Sdttest_BIP32_out() ;
			}
		}
	}
	#endregion
}