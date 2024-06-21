/*
				   File: type_SdtcreateGroup__postInput
			Description: createGroup__postInput
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


namespace GeneXus.Programs
{
	[XmlRoot(ElementName="createGroup__postInput")]
	[XmlType(TypeName="createGroup__postInput" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtcreateGroup__postInput : GxUserType
	{
		public SdtcreateGroup__postInput( )
		{
			/* Constructor for serialization */
			gxTv_SdtcreateGroup__postInput_Groupencryptedkey = "";

			gxTv_SdtcreateGroup__postInput_Groupiv = "";

			gxTv_SdtcreateGroup__postInput_Groupencrypted = "";

		}

		public SdtcreateGroup__postInput(IGxContext context)
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
			AddObjectProperty("GroupEncryptedKey", gxTpr_Groupencryptedkey, false);


			AddObjectProperty("GroupIV", gxTpr_Groupiv, false);


			AddObjectProperty("GroupEncrypted", gxTpr_Groupencrypted, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="GroupEncryptedKey")]
		[XmlElement(ElementName="GroupEncryptedKey")]
		public string gxTpr_Groupencryptedkey
		{
			get {
				return gxTv_SdtcreateGroup__postInput_Groupencryptedkey; 
			}
			set {
				gxTv_SdtcreateGroup__postInput_Groupencryptedkey = value;
				SetDirty("Groupencryptedkey");
			}
		}




		[SoapElement(ElementName="GroupIV")]
		[XmlElement(ElementName="GroupIV")]
		public string gxTpr_Groupiv
		{
			get {
				return gxTv_SdtcreateGroup__postInput_Groupiv; 
			}
			set {
				gxTv_SdtcreateGroup__postInput_Groupiv = value;
				SetDirty("Groupiv");
			}
		}




		[SoapElement(ElementName="GroupEncrypted")]
		[XmlElement(ElementName="GroupEncrypted")]
		public string gxTpr_Groupencrypted
		{
			get {
				return gxTv_SdtcreateGroup__postInput_Groupencrypted; 
			}
			set {
				gxTv_SdtcreateGroup__postInput_Groupencrypted = value;
				SetDirty("Groupencrypted");
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
			gxTv_SdtcreateGroup__postInput_Groupencryptedkey = "";
			gxTv_SdtcreateGroup__postInput_Groupiv = "";
			gxTv_SdtcreateGroup__postInput_Groupencrypted = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtcreateGroup__postInput_Groupencryptedkey;
		 

		protected string gxTv_SdtcreateGroup__postInput_Groupiv;
		 

		protected string gxTv_SdtcreateGroup__postInput_Groupencrypted;
		 


		#endregion
	}
	#region Rest interface
	[GxUnWrappedJson()]
	[DataContract(Name=@"createGroup__postInput", Namespace="distributedcryptography")]
	public class SdtcreateGroup__postInput_RESTInterface : GxGenericCollectionItem<SdtcreateGroup__postInput>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtcreateGroup__postInput_RESTInterface( ) : base()
		{	
		}

		public SdtcreateGroup__postInput_RESTInterface( SdtcreateGroup__postInput psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="GroupEncryptedKey", Order=0)]
		public  string gxTpr_Groupencryptedkey
		{
			get { 
				return sdt.gxTpr_Groupencryptedkey;

			}
			set { 
				 sdt.gxTpr_Groupencryptedkey = value;
			}
		}

		[DataMember(Name="GroupIV", Order=1)]
		public  string gxTpr_Groupiv
		{
			get { 
				return sdt.gxTpr_Groupiv;

			}
			set { 
				 sdt.gxTpr_Groupiv = value;
			}
		}

		[DataMember(Name="GroupEncrypted", Order=2)]
		public  string gxTpr_Groupencrypted
		{
			get { 
				return sdt.gxTpr_Groupencrypted;

			}
			set { 
				 sdt.gxTpr_Groupencrypted = value;
			}
		}


		#endregion

		public SdtcreateGroup__postInput sdt
		{
			get { 
				return (SdtcreateGroup__postInput)Sdt;
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
				sdt = new SdtcreateGroup__postInput() ;
			}
		}
	}
	#endregion
}