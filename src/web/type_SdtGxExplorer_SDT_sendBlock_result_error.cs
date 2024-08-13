/*
				   File: type_SdtGxExplorer_SDT_sendBlock_result_error
			Description: GxExplorer_SDT_sendBlock_result_error
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


namespace GeneXus.Programs
{
	[XmlRoot(ElementName="GxExplorer_SDT_sendBlock_result_error")]
	[XmlType(TypeName="GxExplorer_SDT_sendBlock_result_error" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtGxExplorer_SDT_sendBlock_result_error : GxUserType
	{
		public SdtGxExplorer_SDT_sendBlock_result_error( )
		{
			/* Constructor for serialization */
			gxTv_SdtGxExplorer_SDT_sendBlock_result_error_Message = "";

		}

		public SdtGxExplorer_SDT_sendBlock_result_error(IGxContext context)
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
			AddObjectProperty("code", gxTpr_Code, false);


			AddObjectProperty("message", gxTpr_Message, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="code")]
		[XmlElement(ElementName="code")]
		public string gxTpr_Code_double
		{
			get {
				return Convert.ToString(gxTv_SdtGxExplorer_SDT_sendBlock_result_error_Code, System.Globalization.CultureInfo.InvariantCulture);
			}
			set {
				gxTv_SdtGxExplorer_SDT_sendBlock_result_error_Code = (decimal)(Convert.ToDecimal(value, System.Globalization.CultureInfo.InvariantCulture));
			}
		}
		[XmlIgnore]
		public decimal gxTpr_Code
		{
			get {
				return gxTv_SdtGxExplorer_SDT_sendBlock_result_error_Code; 
			}
			set {
				gxTv_SdtGxExplorer_SDT_sendBlock_result_error_Code = value;
				SetDirty("Code");
			}
		}




		[SoapElement(ElementName="message")]
		[XmlElement(ElementName="message")]
		public string gxTpr_Message
		{
			get {
				return gxTv_SdtGxExplorer_SDT_sendBlock_result_error_Message; 
			}
			set {
				gxTv_SdtGxExplorer_SDT_sendBlock_result_error_Message = value;
				SetDirty("Message");
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
			gxTv_SdtGxExplorer_SDT_sendBlock_result_error_Message = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected decimal gxTv_SdtGxExplorer_SDT_sendBlock_result_error_Code;
		 

		protected string gxTv_SdtGxExplorer_SDT_sendBlock_result_error_Message;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"GxExplorer_SDT_sendBlock_result_error", Namespace="distributedcryptography")]
	public class SdtGxExplorer_SDT_sendBlock_result_error_RESTInterface : GxGenericCollectionItem<SdtGxExplorer_SDT_sendBlock_result_error>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtGxExplorer_SDT_sendBlock_result_error_RESTInterface( ) : base()
		{	
		}

		public SdtGxExplorer_SDT_sendBlock_result_error_RESTInterface( SdtGxExplorer_SDT_sendBlock_result_error psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="code", Order=0)]
		public  string gxTpr_Code
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str(  sdt.gxTpr_Code, 10, 2));

			}
			set { 
				sdt.gxTpr_Code =  NumberUtil.Val( value, ".");
			}
		}

		[DataMember(Name="message", Order=1)]
		public  string gxTpr_Message
		{
			get { 
				return sdt.gxTpr_Message;

			}
			set { 
				 sdt.gxTpr_Message = value;
			}
		}


		#endregion

		public SdtGxExplorer_SDT_sendBlock_result_error sdt
		{
			get { 
				return (SdtGxExplorer_SDT_sendBlock_result_error)Sdt;
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
				sdt = new SdtGxExplorer_SDT_sendBlock_result_error() ;
			}
		}
	}
	#endregion
}