using System;
using System.Collections;
using GeneXus.Utils;
using GeneXus.Resources;
using GeneXus.Application;
using GeneXus.Metadata;
using GeneXus.Cryptography;
using System.Data;
using GeneXus.Data;
using com.genexus;
using GeneXus.Data.ADO;
using GeneXus.Data.NTier;
using GeneXus.Data.NTier.ADO;
using GeneXus.WebControls;
using GeneXus.Http;
using GeneXus.XML;
using GeneXus.Search;
using GeneXus.Encryption;
using GeneXus.Http.Client;
using System.Xml.Serialization;
using System.Runtime.Serialization;
namespace GeneXus.Programs.wallet {
   public class showtransactiondetail : GXDataArea
   {
      public showtransactiondetail( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public showtransactiondetail( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( string aP0_transactionId ,
                           long aP1_n ,
                           string aP2_transactionFileName )
      {
         this.AV18transactionId = aP0_transactionId;
         this.AV23n = aP1_n;
         this.AV19transactionFileName = aP2_transactionFileName;
         ExecuteImpl();
      }

      protected override void ExecutePrivate( )
      {
         isStatic = false;
         webExecute();
      }

      protected override void createObjects( )
      {
      }

      protected void INITWEB( )
      {
         initialize_properties( ) ;
         if ( nGotPars == 0 )
         {
            entryPointCalled = false;
            gxfirstwebparm = GetFirstPar( "transactionId");
            gxfirstwebparm_bkp = gxfirstwebparm;
            gxfirstwebparm = DecryptAjaxCall( gxfirstwebparm);
            toggleJsOutput = isJsOutputEnabled( );
            if ( context.isSpaRequest( ) )
            {
               disableJsOutput();
            }
            if ( StringUtil.StrCmp(gxfirstwebparm, "dyncall") == 0 )
            {
               setAjaxCallMode();
               if ( ! IsValidAjaxCall( true) )
               {
                  GxWebError = 1;
                  return  ;
               }
               dyncall( GetNextPar( )) ;
               return  ;
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxEvt") == 0 )
            {
               setAjaxEventMode();
               if ( ! IsValidAjaxCall( true) )
               {
                  GxWebError = 1;
                  return  ;
               }
               gxfirstwebparm = GetFirstPar( "transactionId");
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxfullajaxEvt") == 0 )
            {
               if ( ! IsValidAjaxCall( true) )
               {
                  GxWebError = 1;
                  return  ;
               }
               gxfirstwebparm = GetFirstPar( "transactionId");
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxNewRow_"+"Grid1") == 0 )
            {
               gxnrGrid1_newrow_invoke( ) ;
               return  ;
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxGridRefresh_"+"Grid1") == 0 )
            {
               gxgrGrid1_refresh_invoke( ) ;
               return  ;
            }
            else
            {
               if ( ! IsValidAjaxCall( false) )
               {
                  GxWebError = 1;
                  return  ;
               }
               gxfirstwebparm = gxfirstwebparm_bkp;
            }
            if ( ! entryPointCalled && ! ( isAjaxCallMode( ) || isFullAjaxMode( ) ) )
            {
               AV18transactionId = gxfirstwebparm;
               AssignAttri("", false, "AV18transactionId", AV18transactionId);
               GxWebStd.gx_hidden_field( context, "gxhash_vTRANSACTIONID", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV18transactionId, "")), context));
               if ( StringUtil.StrCmp(gxfirstwebparm, "viewer") != 0 )
               {
                  AV23n = (long)(Math.Round(NumberUtil.Val( GetPar( "n"), "."), 18, MidpointRounding.ToEven));
                  AssignAttri("", false, "AV23n", StringUtil.LTrimStr( (decimal)(AV23n), 10, 0));
                  GxWebStd.gx_hidden_field( context, "gxhash_vN", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(AV23n), "ZZZZZZZZZ9"), context));
                  AV19transactionFileName = GetPar( "transactionFileName");
                  AssignAttri("", false, "AV19transactionFileName", AV19transactionFileName);
                  GxWebStd.gx_hidden_field( context, "gxhash_vTRANSACTIONFILENAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV19transactionFileName, "")), context));
               }
            }
            if ( toggleJsOutput )
            {
               if ( context.isSpaRequest( ) )
               {
                  enableJsOutput();
               }
            }
         }
         if ( ! context.IsLocalStorageSupported( ) )
         {
            context.PushCurrentUrl();
         }
      }

      protected void gxnrGrid1_newrow_invoke( )
      {
         nRC_GXsfl_57 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_57"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_57_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_57_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_57_idx = GetPar( "sGXsfl_57_idx");
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxnrGrid1_newrow( ) ;
         /* End function gxnrGrid1_newrow_invoke */
      }

      protected void gxgrGrid1_refresh_invoke( )
      {
         AV16error = GetPar( "error");
         AV19transactionFileName = GetPar( "transactionFileName");
         AV18transactionId = GetPar( "transactionId");
         AV23n = (long)(Math.Round(NumberUtil.Val( GetPar( "n"), "."), 18, MidpointRounding.ToEven));
         AV17usedTransactionId = GetPar( "usedTransactionId");
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrGrid1_refresh( AV16error, AV19transactionFileName, AV18transactionId, AV23n, AV17usedTransactionId) ;
         AddString( context.getJSONResponse( )) ;
         /* End function gxgrGrid1_refresh_invoke */
      }

      public override void webExecute( )
      {
         createObjects();
         initialize();
         INITWEB( ) ;
         if ( ! isAjaxCallMode( ) )
         {
            MasterPageObj = (GXMasterPage) ClassLoader.GetInstance("general.ui.masterunanimosidebar", "GeneXus.Programs.general.ui.masterunanimosidebar", new Object[] {context});
            MasterPageObj.setDataArea(this,false);
            ValidateSpaRequest();
            MasterPageObj.webExecute();
            if ( ( GxWebError == 0 ) && context.isAjaxRequest( ) )
            {
               enableOutput();
               if ( ! context.isAjaxRequest( ) )
               {
                  context.GX_webresponse.AppendHeader("Cache-Control", "no-store");
               }
               if ( ! context.WillRedirect( ) )
               {
                  AddString( context.getJSONResponse( )) ;
               }
               else
               {
                  if ( context.isAjaxRequest( ) )
                  {
                     disableOutput();
                  }
                  RenderHtmlHeaders( ) ;
                  context.Redirect( context.wjLoc );
                  context.DispatchAjaxCommands();
               }
            }
         }
         this.cleanup();
      }

      public override short ExecuteStartEvent( )
      {
         PA0A2( ) ;
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            START0A2( ) ;
         }
         return gxajaxcallmode ;
      }

      public override void RenderHtmlHeaders( )
      {
         GxWebStd.gx_html_headers( context, 0, "", "", Form.Meta, Form.Metaequiv, true);
      }

      public override void RenderHtmlOpenForm( )
      {
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         context.WriteHtmlText( "<title>") ;
         context.SendWebValue( Form.Caption) ;
         context.WriteHtmlTextNl( "</title>") ;
         if ( context.isSpaRequest( ) )
         {
            disableOutput();
         }
         if ( StringUtil.Len( sDynURL) > 0 )
         {
            context.WriteHtmlText( "<BASE href=\""+sDynURL+"\" />") ;
         }
         define_styles( ) ;
         if ( nGXWrapped != 1 )
         {
            MasterPageObj.master_styles();
         }
         CloseStyles();
         if ( ( ( context.GetBrowserType( ) == 1 ) || ( context.GetBrowserType( ) == 5 ) ) && ( StringUtil.StrCmp(context.GetBrowserVersion( ), "7.0") == 0 ) )
         {
            context.AddJavascriptSource("json2.js", "?"+context.GetBuildNumber( 2014200), false, true);
         }
         context.AddJavascriptSource("jquery.js", "?"+context.GetBuildNumber( 2014200), false, true);
         context.AddJavascriptSource("gxgral.js", "?"+context.GetBuildNumber( 2014200), false, true);
         context.AddJavascriptSource("gxcfg.js", "?"+GetCacheInvalidationToken( ), false, true);
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         context.AddJavascriptSource("calendar.js", "?"+context.GetBuildNumber( 2014200), false, true);
         context.AddJavascriptSource("calendar-setup.js", "?"+context.GetBuildNumber( 2014200), false, true);
         context.AddJavascriptSource("calendar-en.js", "?"+context.GetBuildNumber( 2014200), false, true);
         context.WriteHtmlText( Form.Headerrawhtml) ;
         context.CloseHtmlHeader();
         if ( context.isSpaRequest( ) )
         {
            disableOutput();
         }
         FormProcess = " data-HasEnter=\"false\" data-Skiponenter=\"false\"";
         context.WriteHtmlText( "<body ") ;
         if ( StringUtil.StrCmp(context.GetLanguageProperty( "rtl"), "true") == 0 )
         {
            context.WriteHtmlText( " dir=\"rtl\" ") ;
         }
         bodyStyle = "" + "background-color:" + context.BuildHTMLColor( Form.Backcolor) + ";color:" + context.BuildHTMLColor( Form.Textcolor) + ";";
         if ( nGXWrapped == 0 )
         {
            bodyStyle += "-moz-opacity:0;opacity:0;";
         }
         if ( ! ( String.IsNullOrEmpty(StringUtil.RTrim( Form.Background)) ) )
         {
            bodyStyle += " background-image:url(" + context.convertURL( Form.Background) + ")";
         }
         context.WriteHtmlText( " "+"class=\"form-horizontal Form\""+" "+ "style='"+bodyStyle+"'") ;
         context.WriteHtmlText( FormProcess+">") ;
         context.skipLines(1);
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wallet.showtransactiondetail.aspx", new object[] {UrlEncode(StringUtil.RTrim(AV18transactionId)),UrlEncode(StringUtil.LTrimStr(AV23n,10,0)),UrlEncode(StringUtil.RTrim(AV19transactionFileName))}, new string[] {"transactionId","n","transactionFileName"}) +"\">") ;
         GxWebStd.gx_hidden_field( context, "_EventName", "");
         GxWebStd.gx_hidden_field( context, "_EventGridId", "");
         GxWebStd.gx_hidden_field( context, "_EventRowId", "");
         context.WriteHtmlText( "<div style=\"height:0;overflow:hidden\"><input type=\"submit\" title=\"submit\"  disabled></div>") ;
         AssignProp("", false, "FORM", "Class", "form-horizontal Form", true);
         toggleJsOutput = isJsOutputEnabled( );
         if ( context.isSpaRequest( ) )
         {
            disableJsOutput();
         }
      }

      protected void send_integrity_footer_hashes( )
      {
         GxWebStd.gx_hidden_field( context, "vERROR", StringUtil.RTrim( AV16error));
         GxWebStd.gx_hidden_field( context, "gxhash_vERROR", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV16error, "")), context));
         GxWebStd.gx_hidden_field( context, "vTRANSACTIONFILENAME", StringUtil.RTrim( AV19transactionFileName));
         GxWebStd.gx_hidden_field( context, "gxhash_vTRANSACTIONFILENAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV19transactionFileName, "")), context));
         GxWebStd.gx_hidden_field( context, "vTRANSACTIONID", StringUtil.RTrim( AV18transactionId));
         GxWebStd.gx_hidden_field( context, "gxhash_vTRANSACTIONID", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV18transactionId, "")), context));
         GxWebStd.gx_hidden_field( context, "vN", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV23n), 10, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "gxhash_vN", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(AV23n), "ZZZZZZZZZ9"), context));
         GxWebStd.gx_hidden_field( context, "vUSEDTRANSACTIONID", StringUtil.RTrim( AV17usedTransactionId));
         GxWebStd.gx_hidden_field( context, "gxhash_vUSEDTRANSACTIONID", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV17usedTransactionId, "")), context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "Showtransactionitem", AV15showTransactionItem);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("Showtransactionitem", AV15showTransactionItem);
         }
         GxWebStd.gx_hidden_field( context, "nRC_GXsfl_57", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_57), 8, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "vERROR", StringUtil.RTrim( AV16error));
         GxWebStd.gx_hidden_field( context, "gxhash_vERROR", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV16error, "")), context));
         GxWebStd.gx_hidden_field( context, "vTRANSACTIONFILENAME", StringUtil.RTrim( AV19transactionFileName));
         GxWebStd.gx_hidden_field( context, "gxhash_vTRANSACTIONFILENAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV19transactionFileName, "")), context));
         GxWebStd.gx_hidden_field( context, "vTRANSACTIONID", StringUtil.RTrim( AV18transactionId));
         GxWebStd.gx_hidden_field( context, "gxhash_vTRANSACTIONID", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV18transactionId, "")), context));
         GxWebStd.gx_hidden_field( context, "vN", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV23n), 10, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "gxhash_vN", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(AV23n), "ZZZZZZZZZ9"), context));
         GxWebStd.gx_hidden_field( context, "vUSEDTRANSACTIONID", StringUtil.RTrim( AV17usedTransactionId));
         GxWebStd.gx_hidden_field( context, "gxhash_vUSEDTRANSACTIONID", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV17usedTransactionId, "")), context));
         GxWebStd.gx_hidden_field( context, "vPOPUPNAME", StringUtil.RTrim( AV25PopupName));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vSHOWTRANSACTIONITEM", AV15showTransactionItem);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vSHOWTRANSACTIONITEM", AV15showTransactionItem);
         }
      }

      public override void RenderHtmlCloseForm( )
      {
         SendCloseFormHiddens( ) ;
         GxWebStd.gx_hidden_field( context, "GX_FocusControl", GX_FocusControl);
         SendAjaxEncryptionKey();
         SendSecurityToken((string)(sPrefix));
         SendComponentObjects();
         SendServerCommands();
         SendState();
         if ( context.isSpaRequest( ) )
         {
            disableOutput();
         }
         context.WriteHtmlTextNl( "</form>") ;
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         include_jscripts( ) ;
      }

      public override void RenderHtmlContent( )
      {
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            context.WriteHtmlText( "<div") ;
            GxWebStd.ClassAttribute( context, "gx-ct-body"+" "+(String.IsNullOrEmpty(StringUtil.RTrim( Form.Class)) ? "form-horizontal Form" : Form.Class)+"-fx");
            context.WriteHtmlText( ">") ;
            WE0A2( ) ;
            context.WriteHtmlText( "</div>") ;
         }
      }

      public override void DispatchEvents( )
      {
         EVT0A2( ) ;
      }

      public override bool HasEnterEvent( )
      {
         return false ;
      }

      public override GXWebForm GetForm( )
      {
         return Form ;
      }

      public override string GetSelfLink( )
      {
         return formatLink("wallet.showtransactiondetail.aspx", new object[] {UrlEncode(StringUtil.RTrim(AV18transactionId)),UrlEncode(StringUtil.LTrimStr(AV23n,10,0)),UrlEncode(StringUtil.RTrim(AV19transactionFileName))}, new string[] {"transactionId","n","transactionFileName"})  ;
      }

      public override string GetPgmname( )
      {
         return "Wallet.ShowTransactionDetail" ;
      }

      public override string GetPgmdesc( )
      {
         return "Transaction Detail" ;
      }

      protected void WB0A0( )
      {
         if ( context.isAjaxRequest( ) )
         {
            disableOutput();
         }
         if ( ! wbLoad )
         {
            if ( nGXWrapped == 1 )
            {
               RenderHtmlHeaders( ) ;
               RenderHtmlOpenForm( ) ;
            }
            GxWebStd.gx_msg_list( context, "", context.GX_msglist.DisplayMode, "", "", "", "false");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "Section", "start", "top", " "+"data-gx-base-lib=\"none\""+" "+"data-abstract-form"+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divMaintable_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "end", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 6,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttClose_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(57), 2, 0)+","+"null"+");", "Close", bttClose_Jsonclick, 5, "Close", "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'CLOSE\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/ShowTransactionDetail.htm");
            GxWebStd.gx_div_end( context, "end", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavCtldescription_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavCtldescription_Internalname, "Description", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Multiple line edit */
            ClassString = "Attribute";
            StyleString = "";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_html_textarea( context, edtavCtldescription_Internalname, AV15showTransactionItem.gxTpr_Description, "", "", 0, 1, edtavCtldescription_Enabled, 0, 80, "chr", 4, "row", 0, StyleString, ClassString, "", "", "250", -1, 0, "", "", -1, true, "", "'"+""+"'"+",false,"+"'"+""+"'", 0, "", "HLP_Wallet/ShowTransactionDetail.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 13,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttEditdescription_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(57), 2, 0)+","+"null"+");", "Edit Description", bttEditdescription_Jsonclick, 7, "Edit Description", "", StyleString, ClassString, bttEditdescription_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"e110a1_client"+"'", TempTags, "", 2, "HLP_Wallet/ShowTransactionDetail.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavCtltransactionid_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavCtltransactionid_Internalname, "Transaction Id", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            GxWebStd.gx_single_line_edit( context, edtavCtltransactionid_Internalname, StringUtil.RTrim( AV15showTransactionItem.gxTpr_Transactionid), StringUtil.RTrim( context.localUtil.Format( AV15showTransactionItem.gxTpr_Transactionid, "")), "", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavCtltransactionid_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavCtltransactionid_Enabled, 0, "text", "", 64, "chr", 1, "row", 64, 0, 0, 0, 0, -1, -1, true, "", "start", true, "", "HLP_Wallet/ShowTransactionDetail.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavCtln_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavCtln_Internalname, "n", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            GxWebStd.gx_single_line_edit( context, edtavCtln_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(AV15showTransactionItem.gxTpr_N), 18, 0, ".", "")), StringUtil.LTrim( ((edtavCtln_Enabled!=0) ? context.localUtil.Format( (decimal)(AV15showTransactionItem.gxTpr_N), "ZZZZZZZZZZZZZZZZZ9") : context.localUtil.Format( (decimal)(AV15showTransactionItem.gxTpr_N), "ZZZZZZZZZZZZZZZZZ9"))), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavCtln_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavCtln_Enabled, 0, "text", "1", 18, "chr", 1, "row", 18, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_Wallet/ShowTransactionDetail.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavCtlvalue_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavCtlvalue_Internalname, "Value", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            GxWebStd.gx_single_line_edit( context, edtavCtlvalue_Internalname, StringUtil.LTrim( StringUtil.NToC( AV15showTransactionItem.gxTpr_Value, 16, 8, ".", "")), StringUtil.LTrim( ((edtavCtlvalue_Enabled!=0) ? context.localUtil.Format( AV15showTransactionItem.gxTpr_Value, "ZZZZZZ9.99999999") : context.localUtil.Format( AV15showTransactionItem.gxTpr_Value, "ZZZZZZ9.99999999"))), "", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavCtlvalue_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavCtlvalue_Enabled, 0, "text", "", 16, "chr", 1, "row", 16, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_Wallet/ShowTransactionDetail.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavCtldatetime_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavCtldatetime_Internalname, "Received on", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            context.WriteHtmlText( "<div id=\""+edtavCtldatetime_Internalname+"_dp_container\" class=\"dp_container\" style=\"white-space:nowrap;display:inline;\">") ;
            GxWebStd.gx_single_line_edit( context, edtavCtldatetime_Internalname, context.localUtil.TToC( AV15showTransactionItem.gxTpr_Datetime, 10, 8, 1, 2, "/", ":", " "), context.localUtil.Format( AV15showTransactionItem.gxTpr_Datetime, "99/99/99 99:99"), "", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavCtldatetime_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavCtldatetime_Enabled, 0, "text", "", 17, "chr", 1, "row", 17, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_Wallet/ShowTransactionDetail.htm");
            GxWebStd.gx_bitmap( context, edtavCtldatetime_Internalname+"_dp_trigger", context.GetImagePath( "61b9b5d3-dff6-4d59-9b00-da61bc2cbe93", "", context.GetTheme( )), "", "", "", "", ((1==0)||(edtavCtldatetime_Enabled==0) ? 0 : 1), 0, "Date selector", "Date selector", 0, 1, 0, "", 0, "", 0, 0, 0, "", "", "cursor: pointer;", "", "", "", "", "", "", "", "", 1, false, false, "", "HLP_Wallet/ShowTransactionDetail.htm");
            context.WriteHtmlTextNl( "</div>") ;
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavCtlscriptpubkey_address_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavCtlscriptpubkey_address_Internalname, "Address", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Multiple line edit */
            ClassString = "Attribute";
            StyleString = "";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_html_textarea( context, edtavCtlscriptpubkey_address_Internalname, StringUtil.RTrim( AV15showTransactionItem.gxTpr_Scriptpubkey_address), "", "", 0, 1, edtavCtlscriptpubkey_address_Enabled, 0, 80, "chr", 4, "row", 0, StyleString, ClassString, "", "", "250", -1, 0, "", "", -1, true, "", "'"+""+"'"+",false,"+"'"+""+"'", 0, "", "HLP_Wallet/ShowTransactionDetail.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Control Group */
            GxWebStd.gx_group_start( context, grpGroup1_Internalname, "Spent on", grpGroup1_Visible, 0, "px", 0, "px", "Group", "", "HLP_Wallet/ShowTransactionDetail.htm");
            /* Div Control */
            GxWebStd.gx_div_start( context, divGroup1table_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavCtltransactionid2_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavCtltransactionid2_Internalname, "Transaction Id", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            GxWebStd.gx_single_line_edit( context, edtavCtltransactionid2_Internalname, StringUtil.RTrim( AV15showTransactionItem.gxTpr_Usedin.gxTpr_Transactionid), StringUtil.RTrim( context.localUtil.Format( AV15showTransactionItem.gxTpr_Usedin.gxTpr_Transactionid, "")), "", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavCtltransactionid2_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavCtltransactionid2_Enabled, 0, "text", "", 64, "chr", 1, "row", 64, 0, 0, 0, 0, -1, -1, true, "", "start", true, "", "HLP_Wallet/ShowTransactionDetail.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavCtln2_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavCtln2_Internalname, "n", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            GxWebStd.gx_single_line_edit( context, edtavCtln2_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(AV15showTransactionItem.gxTpr_Usedin.gxTpr_N), 18, 0, ".", "")), StringUtil.LTrim( ((edtavCtln2_Enabled!=0) ? context.localUtil.Format( (decimal)(AV15showTransactionItem.gxTpr_Usedin.gxTpr_N), "ZZZZZZZZZZZZZZZZZ9") : context.localUtil.Format( (decimal)(AV15showTransactionItem.gxTpr_Usedin.gxTpr_N), "ZZZZZZZZZZZZZZZZZ9"))), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavCtln2_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavCtln2_Enabled, 0, "text", "1", 18, "chr", 1, "row", 18, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_Wallet/ShowTransactionDetail.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavCtldatetime2_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavCtldatetime2_Internalname, "datetime", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            context.WriteHtmlText( "<div id=\""+edtavCtldatetime2_Internalname+"_dp_container\" class=\"dp_container\" style=\"white-space:nowrap;display:inline;\">") ;
            GxWebStd.gx_single_line_edit( context, edtavCtldatetime2_Internalname, context.localUtil.TToC( AV15showTransactionItem.gxTpr_Usedin.gxTpr_Datetime, 10, 8, 1, 2, "/", ":", " "), context.localUtil.Format( AV15showTransactionItem.gxTpr_Usedin.gxTpr_Datetime, "99/99/99 99:99"), "", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavCtldatetime2_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavCtldatetime2_Enabled, 0, "text", "", 17, "chr", 1, "row", 17, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_Wallet/ShowTransactionDetail.htm");
            GxWebStd.gx_bitmap( context, edtavCtldatetime2_Internalname+"_dp_trigger", context.GetImagePath( "61b9b5d3-dff6-4d59-9b00-da61bc2cbe93", "", context.GetTheme( )), "", "", "", "", ((1==0)||(edtavCtldatetime2_Enabled==0) ? 0 : 1), 0, "Date selector", "Date selector", 0, 1, 0, "", 0, "", 0, 0, 0, "", "", "cursor: pointer;", "", "", "", "", "", "", "", "", 1, false, false, "", "HLP_Wallet/ShowTransactionDetail.htm");
            context.WriteHtmlTextNl( "</div>") ;
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /*  Grid Control  */
            Grid1Container.SetIsFreestyle(true);
            Grid1Container.SetWrapped(nGXWrapped);
            StartGridControl57( ) ;
         }
         if ( wbEnd == 57 )
         {
            wbEnd = 0;
            nRC_GXsfl_57 = (int)(nGXsfl_57_idx-1);
            if ( Grid1Container.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "</table>") ;
               context.WriteHtmlText( "</div>") ;
            }
            else
            {
               AV37GXV10 = nGXsfl_57_idx;
               sStyleString = "";
               context.WriteHtmlText( "<div id=\""+"Grid1Container"+"Div\" "+sStyleString+">"+"</div>") ;
               context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Grid1", Grid1Container, subGrid1_Internalname);
               if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "Grid1ContainerData", Grid1Container.ToJavascriptSource());
               }
               if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "Grid1ContainerData"+"V", Grid1Container.GridValuesHidden());
               }
               else
               {
                  context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"Grid1ContainerData"+"V"+"\" value='"+Grid1Container.GridValuesHidden()+"'/>") ;
               }
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</fieldset>") ;
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         if ( wbEnd == 57 )
         {
            wbEnd = 0;
            if ( isFullAjaxMode( ) )
            {
               if ( Grid1Container.GetWrapped() == 1 )
               {
                  context.WriteHtmlText( "</table>") ;
                  context.WriteHtmlText( "</div>") ;
               }
               else
               {
                  AV37GXV10 = nGXsfl_57_idx;
                  sStyleString = "";
                  context.WriteHtmlText( "<div id=\""+"Grid1Container"+"Div\" "+sStyleString+">"+"</div>") ;
                  context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Grid1", Grid1Container, subGrid1_Internalname);
                  if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "Grid1ContainerData", Grid1Container.ToJavascriptSource());
                  }
                  if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "Grid1ContainerData"+"V", Grid1Container.GridValuesHidden());
                  }
                  else
                  {
                     context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"Grid1ContainerData"+"V"+"\" value='"+Grid1Container.GridValuesHidden()+"'/>") ;
                  }
               }
            }
         }
         wbLoad = true;
      }

      protected void START0A2( )
      {
         wbLoad = false;
         wbEnd = 0;
         wbStart = 0;
         if ( ! context.isSpaRequest( ) )
         {
            if ( context.ExposeMetadata( ) )
            {
               Form.Meta.addItem("generator", "GeneXus .NET 18_0_8-180599", 0) ;
            }
         }
         Form.Meta.addItem("description", "Transaction Detail", 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         wbErr = false;
         STRUP0A0( ) ;
      }

      protected void WS0A2( )
      {
         START0A2( ) ;
         EVT0A2( ) ;
      }

      protected void EVT0A2( )
      {
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) && ! wbErr )
            {
               /* Read Web Panel buttons. */
               sEvt = cgiGet( "_EventName");
               EvtGridId = cgiGet( "_EventGridId");
               EvtRowId = cgiGet( "_EventRowId");
               if ( StringUtil.Len( sEvt) > 0 )
               {
                  sEvtType = StringUtil.Left( sEvt, 1);
                  sEvt = StringUtil.Right( sEvt, (short)(StringUtil.Len( sEvt)-1));
                  if ( StringUtil.StrCmp(sEvtType, "M") != 0 )
                  {
                     if ( StringUtil.StrCmp(sEvtType, "E") == 0 )
                     {
                        sEvtType = StringUtil.Right( sEvt, 1);
                        if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                        {
                           sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                           if ( StringUtil.StrCmp(sEvt, "RFR") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                           }
                           else if ( StringUtil.StrCmp(sEvt, "GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              E120A2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'CLOSE'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'Close' */
                              E130A2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              dynload_actions( ) ;
                           }
                        }
                        else
                        {
                           sEvtType = StringUtil.Right( sEvt, 4);
                           sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-4));
                           if ( ( StringUtil.StrCmp(StringUtil.Left( sEvt, 7), "REFRESH") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 10), "GRID1.LOAD") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "ENTER") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 6), "CANCEL") == 0 ) )
                           {
                              nGXsfl_57_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                              sGXsfl_57_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_57_idx), 4, 0), 4, "0");
                              SubsflControlProps_572( ) ;
                              AV37GXV10 = nGXsfl_57_idx;
                              if ( ( AV15showTransactionItem.gxTpr_Usedin.gxTpr_Usedto.Count >= AV37GXV10 ) && ( AV37GXV10 > 0 ) )
                              {
                                 AV15showTransactionItem.gxTpr_Usedin.gxTpr_Usedto.CurrentItem = ((GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem)AV15showTransactionItem.gxTpr_Usedin.gxTpr_Usedto.Item(AV37GXV10));
                                 ((GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem)AV15showTransactionItem.gxTpr_Usedin.gxTpr_Usedto.Item(AV37GXV10)).gxTpr_Scriptpubkey_address = cgiGet( edtavCtlscriptpubkey_address1_Internalname);
                                 ((GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem)AV15showTransactionItem.gxTpr_Usedin.gxTpr_Usedto.Item(AV37GXV10)).gxTpr_N = (long)(Math.Round(context.localUtil.CToN( cgiGet( edtavCtln3_Internalname), ".", ","), 18, MidpointRounding.ToEven));
                                 ((GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem)AV15showTransactionItem.gxTpr_Usedin.gxTpr_Usedto.Item(AV37GXV10)).gxTpr_Value = context.localUtil.CToN( cgiGet( edtavCtlvalue3_Internalname), ".", ",");
                              }
                              sEvtType = StringUtil.Right( sEvt, 1);
                              if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                              {
                                 sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                                 if ( StringUtil.StrCmp(sEvt, "REFRESH") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Refresh */
                                    E140A2 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "GRID1.LOAD") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Grid1.Load */
                                    E150A2 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "ENTER") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    if ( ! wbErr )
                                    {
                                       Rfr0gs = false;
                                       if ( ! Rfr0gs )
                                       {
                                       }
                                       dynload_actions( ) ;
                                    }
                                    /* No code required for Cancel button. It is implemented as the Reset button. */
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                 }
                              }
                              else
                              {
                              }
                           }
                        }
                     }
                     context.wbHandled = 1;
                  }
               }
            }
         }
      }

      protected void WE0A2( )
      {
         if ( ! GxWebStd.gx_redirect( context) )
         {
            Rfr0gs = true;
            Refresh( ) ;
            if ( ! GxWebStd.gx_redirect( context) )
            {
               if ( nGXWrapped == 1 )
               {
                  RenderHtmlCloseForm( ) ;
               }
            }
         }
      }

      protected void PA0A2( )
      {
         if ( nDonePA == 0 )
         {
            if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
            {
               gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
            }
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            toggleJsOutput = isJsOutputEnabled( );
            if ( context.isSpaRequest( ) )
            {
               disableJsOutput();
            }
            init_web_controls( ) ;
            if ( toggleJsOutput )
            {
               if ( context.isSpaRequest( ) )
               {
                  enableJsOutput();
               }
            }
            if ( ! context.isAjaxRequest( ) )
            {
               GX_FocusControl = edtavCtldescription_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
            }
            nDonePA = 1;
         }
      }

      protected void dynload_actions( )
      {
         /* End function dynload_actions */
      }

      protected void gxnrGrid1_newrow( )
      {
         GxWebStd.set_html_headers( context, 0, "", "");
         SubsflControlProps_572( ) ;
         while ( nGXsfl_57_idx <= nRC_GXsfl_57 )
         {
            sendrow_572( ) ;
            nGXsfl_57_idx = ((subGrid1_Islastpage==1)&&(nGXsfl_57_idx+1>subGrid1_fnc_Recordsperpage( )) ? 1 : nGXsfl_57_idx+1);
            sGXsfl_57_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_57_idx), 4, 0), 4, "0");
            SubsflControlProps_572( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( Grid1Container)) ;
         /* End function gxnrGrid1_newrow */
      }

      protected void gxgrGrid1_refresh( string AV16error ,
                                        string AV19transactionFileName ,
                                        string AV18transactionId ,
                                        long AV23n ,
                                        string AV17usedTransactionId )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         GRID1_nCurrentRecord = 0;
         RF0A2( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         /* End function gxgrGrid1_refresh */
      }

      protected void send_integrity_hashes( )
      {
      }

      protected void clear_multi_value_controls( )
      {
         if ( context.isAjaxRequest( ) )
         {
            dynload_actions( ) ;
            before_start_formulas( ) ;
         }
      }

      protected void fix_multi_value_controls( )
      {
      }

      public void Refresh( )
      {
         send_integrity_hashes( ) ;
         RF0A2( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         edtavCtldescription_Enabled = 0;
         AssignProp("", false, edtavCtldescription_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtldescription_Enabled), 5, 0), true);
         edtavCtltransactionid_Enabled = 0;
         AssignProp("", false, edtavCtltransactionid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtltransactionid_Enabled), 5, 0), true);
         edtavCtln_Enabled = 0;
         AssignProp("", false, edtavCtln_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtln_Enabled), 5, 0), true);
         edtavCtlvalue_Enabled = 0;
         AssignProp("", false, edtavCtlvalue_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlvalue_Enabled), 5, 0), true);
         edtavCtldatetime_Enabled = 0;
         AssignProp("", false, edtavCtldatetime_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtldatetime_Enabled), 5, 0), true);
         edtavCtlscriptpubkey_address_Enabled = 0;
         AssignProp("", false, edtavCtlscriptpubkey_address_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlscriptpubkey_address_Enabled), 5, 0), true);
         edtavCtltransactionid2_Enabled = 0;
         AssignProp("", false, edtavCtltransactionid2_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtltransactionid2_Enabled), 5, 0), true);
         edtavCtln2_Enabled = 0;
         AssignProp("", false, edtavCtln2_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtln2_Enabled), 5, 0), true);
         edtavCtldatetime2_Enabled = 0;
         AssignProp("", false, edtavCtldatetime2_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtldatetime2_Enabled), 5, 0), true);
         edtavCtlscriptpubkey_address1_Enabled = 0;
         AssignProp("", false, edtavCtlscriptpubkey_address1_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlscriptpubkey_address1_Enabled), 5, 0), !bGXsfl_57_Refreshing);
         edtavCtln3_Enabled = 0;
         AssignProp("", false, edtavCtln3_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtln3_Enabled), 5, 0), !bGXsfl_57_Refreshing);
         edtavCtlvalue3_Enabled = 0;
         AssignProp("", false, edtavCtlvalue3_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlvalue3_Enabled), 5, 0), !bGXsfl_57_Refreshing);
      }

      protected void RF0A2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            Grid1Container.ClearRows();
         }
         wbStart = 57;
         /* Execute user event: Refresh */
         E140A2 ();
         nGXsfl_57_idx = 1;
         sGXsfl_57_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_57_idx), 4, 0), 4, "0");
         SubsflControlProps_572( ) ;
         bGXsfl_57_Refreshing = true;
         Grid1Container.AddObjectProperty("GridName", "Grid1");
         Grid1Container.AddObjectProperty("CmpContext", "");
         Grid1Container.AddObjectProperty("InMasterPage", "false");
         Grid1Container.AddObjectProperty("Class", StringUtil.RTrim( "FreeStyleGrid"));
         Grid1Container.AddObjectProperty("Class", "FreeStyleGrid");
         Grid1Container.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
         Grid1Container.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
         Grid1Container.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid1_Backcolorstyle), 1, 0, ".", "")));
         Grid1Container.PageSize = subGrid1_fnc_Recordsperpage( );
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            SubsflControlProps_572( ) ;
            /* Execute user event: Grid1.Load */
            E150A2 ();
            wbEnd = 57;
            WB0A0( ) ;
         }
         bGXsfl_57_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes0A2( )
      {
         GxWebStd.gx_hidden_field( context, "vERROR", StringUtil.RTrim( AV16error));
         GxWebStd.gx_hidden_field( context, "gxhash_vERROR", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV16error, "")), context));
         GxWebStd.gx_hidden_field( context, "vUSEDTRANSACTIONID", StringUtil.RTrim( AV17usedTransactionId));
         GxWebStd.gx_hidden_field( context, "gxhash_vUSEDTRANSACTIONID", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV17usedTransactionId, "")), context));
      }

      protected int subGrid1_fnc_Pagecount( )
      {
         return (int)(-1) ;
      }

      protected int subGrid1_fnc_Recordcount( )
      {
         return (int)(-1) ;
      }

      protected int subGrid1_fnc_Recordsperpage( )
      {
         return (int)(-1) ;
      }

      protected int subGrid1_fnc_Currentpage( )
      {
         return (int)(-1) ;
      }

      protected void before_start_formulas( )
      {
         edtavCtldescription_Enabled = 0;
         AssignProp("", false, edtavCtldescription_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtldescription_Enabled), 5, 0), true);
         edtavCtltransactionid_Enabled = 0;
         AssignProp("", false, edtavCtltransactionid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtltransactionid_Enabled), 5, 0), true);
         edtavCtln_Enabled = 0;
         AssignProp("", false, edtavCtln_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtln_Enabled), 5, 0), true);
         edtavCtlvalue_Enabled = 0;
         AssignProp("", false, edtavCtlvalue_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlvalue_Enabled), 5, 0), true);
         edtavCtldatetime_Enabled = 0;
         AssignProp("", false, edtavCtldatetime_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtldatetime_Enabled), 5, 0), true);
         edtavCtlscriptpubkey_address_Enabled = 0;
         AssignProp("", false, edtavCtlscriptpubkey_address_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlscriptpubkey_address_Enabled), 5, 0), true);
         edtavCtltransactionid2_Enabled = 0;
         AssignProp("", false, edtavCtltransactionid2_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtltransactionid2_Enabled), 5, 0), true);
         edtavCtln2_Enabled = 0;
         AssignProp("", false, edtavCtln2_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtln2_Enabled), 5, 0), true);
         edtavCtldatetime2_Enabled = 0;
         AssignProp("", false, edtavCtldatetime2_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtldatetime2_Enabled), 5, 0), true);
         edtavCtlscriptpubkey_address1_Enabled = 0;
         AssignProp("", false, edtavCtlscriptpubkey_address1_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlscriptpubkey_address1_Enabled), 5, 0), !bGXsfl_57_Refreshing);
         edtavCtln3_Enabled = 0;
         AssignProp("", false, edtavCtln3_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtln3_Enabled), 5, 0), !bGXsfl_57_Refreshing);
         edtavCtlvalue3_Enabled = 0;
         AssignProp("", false, edtavCtlvalue3_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlvalue3_Enabled), 5, 0), !bGXsfl_57_Refreshing);
         fix_multi_value_controls( ) ;
      }

      protected void STRUP0A0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         context.wbGlbDoneStart = 1;
         /* After Start, stand alone formulas. */
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read saved SDTs. */
            ajax_req_read_hidden_sdt(cgiGet( "vSHOWTRANSACTIONITEM"), AV15showTransactionItem);
            ajax_req_read_hidden_sdt(cgiGet( "Showtransactionitem"), AV15showTransactionItem);
            /* Read saved values. */
            nRC_GXsfl_57 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_57"), ".", ","), 18, MidpointRounding.ToEven));
            AV19transactionFileName = cgiGet( "vTRANSACTIONFILENAME");
            AV23n = (long)(Math.Round(context.localUtil.CToN( cgiGet( "vN"), ".", ","), 18, MidpointRounding.ToEven));
            AV18transactionId = cgiGet( "vTRANSACTIONID");
            nRC_GXsfl_57 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_57"), ".", ","), 18, MidpointRounding.ToEven));
            nGXsfl_57_fel_idx = 0;
            while ( nGXsfl_57_fel_idx < nRC_GXsfl_57 )
            {
               nGXsfl_57_fel_idx = ((subGrid1_Islastpage==1)&&(nGXsfl_57_fel_idx+1>subGrid1_fnc_Recordsperpage( )) ? 1 : nGXsfl_57_fel_idx+1);
               sGXsfl_57_fel_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_57_fel_idx), 4, 0), 4, "0");
               SubsflControlProps_fel_572( ) ;
               AV37GXV10 = nGXsfl_57_fel_idx;
               if ( ( AV15showTransactionItem.gxTpr_Usedin.gxTpr_Usedto.Count >= AV37GXV10 ) && ( AV37GXV10 > 0 ) )
               {
                  AV15showTransactionItem.gxTpr_Usedin.gxTpr_Usedto.CurrentItem = ((GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem)AV15showTransactionItem.gxTpr_Usedin.gxTpr_Usedto.Item(AV37GXV10));
                  ((GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem)AV15showTransactionItem.gxTpr_Usedin.gxTpr_Usedto.Item(AV37GXV10)).gxTpr_Scriptpubkey_address = cgiGet( edtavCtlscriptpubkey_address1_Internalname);
                  ((GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem)AV15showTransactionItem.gxTpr_Usedin.gxTpr_Usedto.Item(AV37GXV10)).gxTpr_N = (long)(Math.Round(context.localUtil.CToN( cgiGet( edtavCtln3_Internalname), ".", ","), 18, MidpointRounding.ToEven));
                  ((GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem)AV15showTransactionItem.gxTpr_Usedin.gxTpr_Usedto.Item(AV37GXV10)).gxTpr_Value = context.localUtil.CToN( cgiGet( edtavCtlvalue3_Internalname), ".", ",");
               }
            }
            if ( nGXsfl_57_fel_idx == 0 )
            {
               nGXsfl_57_idx = 1;
               sGXsfl_57_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_57_idx), 4, 0), 4, "0");
               SubsflControlProps_572( ) ;
            }
            nGXsfl_57_fel_idx = 1;
            /* Read variables values. */
            AV15showTransactionItem.gxTpr_Description = cgiGet( edtavCtldescription_Internalname);
            AV15showTransactionItem.gxTpr_Transactionid = cgiGet( edtavCtltransactionid_Internalname);
            if ( ( ( context.localUtil.CToN( cgiGet( edtavCtln_Internalname), ".", ",") < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavCtln_Internalname), ".", ",") > Convert.ToDecimal( 999999999999999999L )) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "CTLN");
               GX_FocusControl = edtavCtln_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV15showTransactionItem.gxTpr_N = 0;
            }
            else
            {
               AV15showTransactionItem.gxTpr_N = (long)(Math.Round(context.localUtil.CToN( cgiGet( edtavCtln_Internalname), ".", ","), 18, MidpointRounding.ToEven));
            }
            if ( ( ( context.localUtil.CToN( cgiGet( edtavCtlvalue_Internalname), ".", ",") < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavCtlvalue_Internalname), ".", ",") > 9999999.99999999m ) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "CTLVALUE");
               GX_FocusControl = edtavCtlvalue_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV15showTransactionItem.gxTpr_Value = 0;
            }
            else
            {
               AV15showTransactionItem.gxTpr_Value = context.localUtil.CToN( cgiGet( edtavCtlvalue_Internalname), ".", ",");
            }
            if ( context.localUtil.VCDateTime( cgiGet( edtavCtldatetime_Internalname), 1, 1) == 0 )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_baddatetime", new   object[]  {""}), 1, "CTLDATETIME");
               GX_FocusControl = edtavCtldatetime_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV15showTransactionItem.gxTpr_Datetime = (DateTime)(DateTime.MinValue);
            }
            else
            {
               AV15showTransactionItem.gxTpr_Datetime = context.localUtil.CToT( cgiGet( edtavCtldatetime_Internalname));
            }
            AV15showTransactionItem.gxTpr_Scriptpubkey_address = cgiGet( edtavCtlscriptpubkey_address_Internalname);
            AV15showTransactionItem.gxTpr_Usedin.gxTpr_Transactionid = cgiGet( edtavCtltransactionid2_Internalname);
            if ( ( ( context.localUtil.CToN( cgiGet( edtavCtln2_Internalname), ".", ",") < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavCtln2_Internalname), ".", ",") > Convert.ToDecimal( 999999999999999999L )) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "CTLN2");
               GX_FocusControl = edtavCtln2_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV15showTransactionItem.gxTpr_Usedin.gxTpr_N = 0;
            }
            else
            {
               AV15showTransactionItem.gxTpr_Usedin.gxTpr_N = (long)(Math.Round(context.localUtil.CToN( cgiGet( edtavCtln2_Internalname), ".", ","), 18, MidpointRounding.ToEven));
            }
            if ( context.localUtil.VCDateTime( cgiGet( edtavCtldatetime2_Internalname), 1, 1) == 0 )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_baddatetime", new   object[]  {""}), 1, "CTLDATETIME2");
               GX_FocusControl = edtavCtldatetime2_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV15showTransactionItem.gxTpr_Usedin.gxTpr_Datetime = (DateTime)(DateTime.MinValue);
            }
            else
            {
               AV15showTransactionItem.gxTpr_Usedin.gxTpr_Datetime = context.localUtil.CToT( cgiGet( edtavCtldatetime2_Internalname));
            }
            /* Read subfile selected row values. */
            /* Read hidden variables. */
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         }
         else
         {
            dynload_actions( ) ;
         }
      }

      protected void E140A2( )
      {
         if ( gx_refresh_fired )
         {
            return  ;
         }
         gx_refresh_fired = true;
         /* Refresh Routine */
         returnInSub = false;
         AV13StoredTransactions.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  AV19transactionFileName, out  AV16error), null);
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV16error)) )
         {
            AV41GXV14 = 1;
            while ( AV41GXV14 <= AV13StoredTransactions.gxTpr_Transaction.Count )
            {
               AV14TransactionItem = ((GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem)AV13StoredTransactions.gxTpr_Transaction.Item(AV41GXV14));
               if ( ( StringUtil.StrCmp(StringUtil.Trim( AV14TransactionItem.gxTpr_Transactionid), StringUtil.Trim( AV18transactionId)) == 0 ) && ( AV14TransactionItem.gxTpr_N == AV23n ) )
               {
                  AV15showTransactionItem = AV14TransactionItem;
                  gx_BV57 = true;
                  AV17usedTransactionId = AV14TransactionItem.gxTpr_Usedin.gxTpr_Transactionid;
                  AssignAttri("", false, "AV17usedTransactionId", AV17usedTransactionId);
                  GxWebStd.gx_hidden_field( context, "gxhash_vUSEDTRANSACTIONID", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV17usedTransactionId, "")), context));
                  if (true) break;
               }
               AV41GXV14 = (int)(AV41GXV14+1);
            }
         }
         else
         {
            GX_msglist.addItem("Error reading transactions file: "+AV16error);
         }
         grpGroup1_Visible = (!String.IsNullOrEmpty(StringUtil.RTrim( AV17usedTransactionId)) ? 1 : 0);
         AssignProp("", false, grpGroup1_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(grpGroup1_Visible), 5, 0), true);
         if ( StringUtil.StrCmp(AV17usedTransactionId, "MULTISIGNATURE IN PROGRESS") == 0 )
         {
            bttEditdescription_Visible = 0;
            AssignProp("", false, bttEditdescription_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttEditdescription_Visible), 5, 0), true);
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV15showTransactionItem", AV15showTransactionItem);
      }

      protected void E120A2( )
      {
         /* Extensions\Web\Popup_Onpopupclosed Routine */
         returnInSub = false;
         AV26webpanelPopupName = "Wallet.EditDescription";
         AV27strFound = (short)(StringUtil.StringSearch( AV25PopupName, StringUtil.Lower( AV26webpanelPopupName), 1));
         if ( AV27strFound > 0 )
         {
            context.DoAjaxRefresh();
         }
         /*  Sending Event outputs  */
         if ( gx_BV57 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV15showTransactionItem", AV15showTransactionItem);
            nGXsfl_57_bak_idx = nGXsfl_57_idx;
            gxgrGrid1_refresh( AV16error, AV19transactionFileName, AV18transactionId, AV23n, AV17usedTransactionId) ;
            nGXsfl_57_idx = nGXsfl_57_bak_idx;
            sGXsfl_57_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_57_idx), 4, 0), 4, "0");
            SubsflControlProps_572( ) ;
         }
      }

      protected void E130A2( )
      {
         /* 'Close' Routine */
         returnInSub = false;
         context.setWebReturnParms(new Object[] {});
         context.setWebReturnParmsMetadata(new Object[] {});
         context.wjLocDisableFrm = 1;
         context.nUserReturn = 1;
         returnInSub = true;
         if (true) return;
      }

      private void E150A2( )
      {
         /* Grid1_Load Routine */
         returnInSub = false;
         AV37GXV10 = 1;
         while ( AV37GXV10 <= AV15showTransactionItem.gxTpr_Usedin.gxTpr_Usedto.Count )
         {
            AV15showTransactionItem.gxTpr_Usedin.gxTpr_Usedto.CurrentItem = ((GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem)AV15showTransactionItem.gxTpr_Usedin.gxTpr_Usedto.Item(AV37GXV10));
            /* Load Method */
            if ( wbStart != -1 )
            {
               wbStart = 57;
            }
            sendrow_572( ) ;
            if ( isFullAjaxMode( ) && ! bGXsfl_57_Refreshing )
            {
               DoAjaxLoad(57, Grid1Row);
            }
            AV37GXV10 = (int)(AV37GXV10+1);
         }
      }

      public override void setparameters( Object[] obj )
      {
         createObjects();
         initialize();
         AV18transactionId = (string)getParm(obj,0);
         AssignAttri("", false, "AV18transactionId", AV18transactionId);
         GxWebStd.gx_hidden_field( context, "gxhash_vTRANSACTIONID", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV18transactionId, "")), context));
         AV23n = Convert.ToInt64(getParm(obj,1));
         AssignAttri("", false, "AV23n", StringUtil.LTrimStr( (decimal)(AV23n), 10, 0));
         GxWebStd.gx_hidden_field( context, "gxhash_vN", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(AV23n), "ZZZZZZZZZ9"), context));
         AV19transactionFileName = (string)getParm(obj,2);
         AssignAttri("", false, "AV19transactionFileName", AV19transactionFileName);
         GxWebStd.gx_hidden_field( context, "gxhash_vTRANSACTIONFILENAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV19transactionFileName, "")), context));
      }

      public override string getresponse( string sGXDynURL )
      {
         initialize_properties( ) ;
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         sDynURL = sGXDynURL;
         nGotPars = (short)(1);
         nGXWrapped = (short)(1);
         context.SetWrapped(true);
         PA0A2( ) ;
         WS0A2( ) ;
         WE0A2( ) ;
         this.cleanup();
         context.SetWrapped(false);
         context.GX_msglist = BackMsgLst;
         return "";
      }

      public void responsestatic( string sGXDynURL )
      {
      }

      protected void define_styles( )
      {
         AddStyleSheetFile("calendar-system.css", "");
         AddThemeStyleSheetFile("", context.GetTheme( )+".css", "?"+GetCacheInvalidationToken( ));
         bool outputEnabled = isOutputEnabled( );
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         idxLst = 1;
         while ( idxLst <= Form.Jscriptsrc.Count )
         {
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?202471164762", true, true);
            idxLst = (int)(idxLst+1);
         }
         if ( ! outputEnabled )
         {
            if ( context.isSpaRequest( ) )
            {
               disableOutput();
            }
         }
         /* End function define_styles */
      }

      protected void include_jscripts( )
      {
         context.AddJavascriptSource("messages.eng.js", "?"+GetCacheInvalidationToken( ), false, true);
         context.AddJavascriptSource("gxdec.js", "?"+context.GetBuildNumber( 2014200), false, true);
         context.AddJavascriptSource("wallet/showtransactiondetail.js", "?202471164762", false, true);
         context.AddJavascriptSource("web-extension/gx-web-extensions.js", "", false, true);
         /* End function include_jscripts */
      }

      protected void SubsflControlProps_572( )
      {
         edtavCtlscriptpubkey_address1_Internalname = "CTLSCRIPTPUBKEY_ADDRESS1_"+sGXsfl_57_idx;
         edtavCtln3_Internalname = "CTLN3_"+sGXsfl_57_idx;
         edtavCtlvalue3_Internalname = "CTLVALUE3_"+sGXsfl_57_idx;
      }

      protected void SubsflControlProps_fel_572( )
      {
         edtavCtlscriptpubkey_address1_Internalname = "CTLSCRIPTPUBKEY_ADDRESS1_"+sGXsfl_57_fel_idx;
         edtavCtln3_Internalname = "CTLN3_"+sGXsfl_57_fel_idx;
         edtavCtlvalue3_Internalname = "CTLVALUE3_"+sGXsfl_57_fel_idx;
      }

      protected void sendrow_572( )
      {
         SubsflControlProps_572( ) ;
         WB0A0( ) ;
         Grid1Row = GXWebRow.GetNew(context,Grid1Container);
         if ( subGrid1_Backcolorstyle == 0 )
         {
            /* None style subfile background logic. */
            subGrid1_Backstyle = 0;
            if ( StringUtil.StrCmp(subGrid1_Class, "") != 0 )
            {
               subGrid1_Linesclass = subGrid1_Class+"Odd";
            }
         }
         else if ( subGrid1_Backcolorstyle == 1 )
         {
            /* Uniform style subfile background logic. */
            subGrid1_Backstyle = 0;
            subGrid1_Backcolor = subGrid1_Allbackcolor;
            if ( StringUtil.StrCmp(subGrid1_Class, "") != 0 )
            {
               subGrid1_Linesclass = subGrid1_Class+"Uniform";
            }
         }
         else if ( subGrid1_Backcolorstyle == 2 )
         {
            /* Header style subfile background logic. */
            subGrid1_Backstyle = 1;
            if ( StringUtil.StrCmp(subGrid1_Class, "") != 0 )
            {
               subGrid1_Linesclass = subGrid1_Class+"Odd";
            }
            subGrid1_Backcolor = (int)(0xFFFFFF);
         }
         else if ( subGrid1_Backcolorstyle == 3 )
         {
            /* Report style subfile background logic. */
            subGrid1_Backstyle = 1;
            if ( ((int)((nGXsfl_57_idx) % (2))) == 0 )
            {
               subGrid1_Backcolor = (int)(0x0);
               if ( StringUtil.StrCmp(subGrid1_Class, "") != 0 )
               {
                  subGrid1_Linesclass = subGrid1_Class+"Even";
               }
            }
            else
            {
               subGrid1_Backcolor = (int)(0xFFFFFF);
               if ( StringUtil.StrCmp(subGrid1_Class, "") != 0 )
               {
                  subGrid1_Linesclass = subGrid1_Class+"Odd";
               }
            }
         }
         /* Start of Columns property logic. */
         if ( Grid1Container.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<tr"+" class=\""+subGrid1_Linesclass+"\" style=\""+""+"\""+" data-gxrow=\""+sGXsfl_57_idx+"\">") ;
         }
         /* Div Control */
         Grid1Row.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)divGrid1table_Internalname+"_"+sGXsfl_57_idx,(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"Table",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         Grid1Row.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"row",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         Grid1Row.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"col-xs-12 col-sm-4",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         Grid1Row.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"form-group gx-form-group",(string)"start",(string)"top",(string)""+" data-gx-for=\""+edtavCtlscriptpubkey_address1_Internalname+"\"",(string)"",(string)"div"});
         /* Attribute/Variable Label */
         Grid1Row.AddColumnProperties("html_label", -1, isAjaxCallMode( ), new Object[] {(string)edtavCtlscriptpubkey_address1_Internalname,(string)"Address",(string)"col-xs-12 AttributeLabel",(short)1,(bool)true,(string)""});
         /* Div Control */
         Grid1Row.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"col-xs-12 gx-attribute",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Multiple line edit */
         ClassString = "Attribute";
         StyleString = "";
         ClassString = "Attribute";
         StyleString = "";
         Grid1Row.AddColumnProperties("html_textarea", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlscriptpubkey_address1_Internalname,StringUtil.RTrim( ((GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem)AV15showTransactionItem.gxTpr_Usedin.gxTpr_Usedto.Item(AV37GXV10)).gxTpr_Scriptpubkey_address),(string)"",(string)"",(short)0,(short)1,(int)edtavCtlscriptpubkey_address1_Enabled,(short)0,(short)80,(string)"chr",(short)4,(string)"row",(short)0,(string)StyleString,(string)ClassString,(string)"",(string)"",(string)"250",(short)-1,(short)0,(string)"",(string)"",(short)-1,(bool)true,(string)"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(short)0,(string)""});
         Grid1Row.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         Grid1Row.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         Grid1Row.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         /* Div Control */
         Grid1Row.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"col-xs-12 col-sm-4",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         Grid1Row.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"form-group gx-form-group",(string)"start",(string)"top",(string)""+" data-gx-for=\""+edtavCtln3_Internalname+"\"",(string)"",(string)"div"});
         /* Attribute/Variable Label */
         Grid1Row.AddColumnProperties("html_label", -1, isAjaxCallMode( ), new Object[] {(string)edtavCtln3_Internalname,(string)"n",(string)"col-xs-12 AttributeLabel",(short)1,(bool)true,(string)""});
         /* Div Control */
         Grid1Row.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"col-xs-12 gx-attribute",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Single line edit */
         ROClassString = "Attribute";
         Grid1Row.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtln3_Internalname,StringUtil.LTrim( StringUtil.NToC( (decimal)(((GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem)AV15showTransactionItem.gxTpr_Usedin.gxTpr_Usedto.Item(AV37GXV10)).gxTpr_N), 18, 0, ".", "")),StringUtil.LTrim( ((edtavCtln3_Enabled!=0) ? context.localUtil.Format( (decimal)(((GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem)AV15showTransactionItem.gxTpr_Usedin.gxTpr_Usedto.Item(AV37GXV10)).gxTpr_N), "ZZZZZZZZZZZZZZZZZ9") : context.localUtil.Format( (decimal)(((GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem)AV15showTransactionItem.gxTpr_Usedin.gxTpr_Usedto.Item(AV37GXV10)).gxTpr_N), "ZZZZZZZZZZZZZZZZZ9"))),(string)" dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtln3_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)1,(int)edtavCtln3_Enabled,(short)0,(string)"text",(string)"1",(short)18,(string)"chr",(short)1,(string)"row",(short)18,(short)0,(short)0,(short)57,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
         Grid1Row.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         Grid1Row.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         Grid1Row.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         /* Div Control */
         Grid1Row.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"col-xs-12 col-sm-4",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         Grid1Row.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"form-group gx-form-group",(string)"start",(string)"top",(string)""+" data-gx-for=\""+edtavCtlvalue3_Internalname+"\"",(string)"",(string)"div"});
         /* Attribute/Variable Label */
         Grid1Row.AddColumnProperties("html_label", -1, isAjaxCallMode( ), new Object[] {(string)edtavCtlvalue3_Internalname,(string)"Value",(string)"col-xs-12 AttributeLabel",(short)1,(bool)true,(string)""});
         /* Div Control */
         Grid1Row.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"col-xs-12 gx-attribute",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Single line edit */
         ROClassString = "Attribute";
         Grid1Row.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlvalue3_Internalname,StringUtil.LTrim( StringUtil.NToC( ((GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem)AV15showTransactionItem.gxTpr_Usedin.gxTpr_Usedto.Item(AV37GXV10)).gxTpr_Value, 16, 8, ".", "")),StringUtil.LTrim( ((edtavCtlvalue3_Enabled!=0) ? context.localUtil.Format( ((GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem)AV15showTransactionItem.gxTpr_Usedin.gxTpr_Usedto.Item(AV37GXV10)).gxTpr_Value, "ZZZZZZ9.99999999") : context.localUtil.Format( ((GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem)AV15showTransactionItem.gxTpr_Usedin.gxTpr_Usedto.Item(AV37GXV10)).gxTpr_Value, "ZZZZZZ9.99999999"))),(string)"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlvalue3_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)1,(int)edtavCtlvalue3_Enabled,(short)0,(string)"text",(string)"",(short)16,(string)"chr",(short)1,(string)"row",(short)16,(short)0,(short)0,(short)57,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
         Grid1Row.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         Grid1Row.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         Grid1Row.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         Grid1Row.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         Grid1Row.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         send_integrity_lvl_hashes0A2( ) ;
         /* End of Columns property logic. */
         Grid1Container.AddRow(Grid1Row);
         nGXsfl_57_idx = ((subGrid1_Islastpage==1)&&(nGXsfl_57_idx+1>subGrid1_fnc_Recordsperpage( )) ? 1 : nGXsfl_57_idx+1);
         sGXsfl_57_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_57_idx), 4, 0), 4, "0");
         SubsflControlProps_572( ) ;
         /* End function sendrow_572 */
      }

      protected void init_web_controls( )
      {
         /* End function init_web_controls */
      }

      protected void StartGridControl57( )
      {
         if ( Grid1Container.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+"Grid1Container"+"DivS\" data-gxgridid=\"57\">") ;
            sStyleString = "";
            GxWebStd.gx_table_start( context, subGrid1_Internalname, subGrid1_Internalname, "", "FreeStyleGrid", 0, "", "", 1, 2, sStyleString, "", "", 0);
            Grid1Container.AddObjectProperty("GridName", "Grid1");
         }
         else
         {
            Grid1Container.AddObjectProperty("GridName", "Grid1");
            Grid1Container.AddObjectProperty("Header", subGrid1_Header);
            Grid1Container.AddObjectProperty("Class", StringUtil.RTrim( "FreeStyleGrid"));
            Grid1Container.AddObjectProperty("Class", "FreeStyleGrid");
            Grid1Container.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
            Grid1Container.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
            Grid1Container.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid1_Backcolorstyle), 1, 0, ".", "")));
            Grid1Container.AddObjectProperty("CmpContext", "");
            Grid1Container.AddObjectProperty("InMasterPage", "false");
            Grid1Column = GXWebColumn.GetNew(isAjaxCallMode( ));
            Grid1Container.AddColumnProperties(Grid1Column);
            Grid1Column = GXWebColumn.GetNew(isAjaxCallMode( ));
            Grid1Container.AddColumnProperties(Grid1Column);
            Grid1Column = GXWebColumn.GetNew(isAjaxCallMode( ));
            Grid1Container.AddColumnProperties(Grid1Column);
            Grid1Column = GXWebColumn.GetNew(isAjaxCallMode( ));
            Grid1Container.AddColumnProperties(Grid1Column);
            Grid1Column = GXWebColumn.GetNew(isAjaxCallMode( ));
            Grid1Container.AddColumnProperties(Grid1Column);
            Grid1Column = GXWebColumn.GetNew(isAjaxCallMode( ));
            Grid1Column.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlscriptpubkey_address1_Enabled), 5, 0, ".", "")));
            Grid1Container.AddColumnProperties(Grid1Column);
            Grid1Column = GXWebColumn.GetNew(isAjaxCallMode( ));
            Grid1Container.AddColumnProperties(Grid1Column);
            Grid1Column = GXWebColumn.GetNew(isAjaxCallMode( ));
            Grid1Container.AddColumnProperties(Grid1Column);
            Grid1Column = GXWebColumn.GetNew(isAjaxCallMode( ));
            Grid1Container.AddColumnProperties(Grid1Column);
            Grid1Column = GXWebColumn.GetNew(isAjaxCallMode( ));
            Grid1Column.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtln3_Enabled), 5, 0, ".", "")));
            Grid1Container.AddColumnProperties(Grid1Column);
            Grid1Column = GXWebColumn.GetNew(isAjaxCallMode( ));
            Grid1Container.AddColumnProperties(Grid1Column);
            Grid1Column = GXWebColumn.GetNew(isAjaxCallMode( ));
            Grid1Container.AddColumnProperties(Grid1Column);
            Grid1Column = GXWebColumn.GetNew(isAjaxCallMode( ));
            Grid1Container.AddColumnProperties(Grid1Column);
            Grid1Column = GXWebColumn.GetNew(isAjaxCallMode( ));
            Grid1Column.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlvalue3_Enabled), 5, 0, ".", "")));
            Grid1Container.AddColumnProperties(Grid1Column);
            Grid1Container.AddObjectProperty("Selectedindex", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid1_Selectedindex), 4, 0, ".", "")));
            Grid1Container.AddObjectProperty("Allowselection", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid1_Allowselection), 1, 0, ".", "")));
            Grid1Container.AddObjectProperty("Selectioncolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid1_Selectioncolor), 9, 0, ".", "")));
            Grid1Container.AddObjectProperty("Allowhover", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid1_Allowhovering), 1, 0, ".", "")));
            Grid1Container.AddObjectProperty("Hovercolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid1_Hoveringcolor), 9, 0, ".", "")));
            Grid1Container.AddObjectProperty("Allowcollapsing", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid1_Allowcollapsing), 1, 0, ".", "")));
            Grid1Container.AddObjectProperty("Collapsed", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid1_Collapsed), 1, 0, ".", "")));
         }
      }

      protected void init_default_properties( )
      {
         bttClose_Internalname = "CLOSE";
         edtavCtldescription_Internalname = "CTLDESCRIPTION";
         bttEditdescription_Internalname = "EDITDESCRIPTION";
         edtavCtltransactionid_Internalname = "CTLTRANSACTIONID";
         edtavCtln_Internalname = "CTLN";
         edtavCtlvalue_Internalname = "CTLVALUE";
         edtavCtldatetime_Internalname = "CTLDATETIME";
         edtavCtlscriptpubkey_address_Internalname = "CTLSCRIPTPUBKEY_ADDRESS";
         edtavCtltransactionid2_Internalname = "CTLTRANSACTIONID2";
         edtavCtln2_Internalname = "CTLN2";
         edtavCtldatetime2_Internalname = "CTLDATETIME2";
         edtavCtlscriptpubkey_address1_Internalname = "CTLSCRIPTPUBKEY_ADDRESS1";
         edtavCtln3_Internalname = "CTLN3";
         edtavCtlvalue3_Internalname = "CTLVALUE3";
         divGrid1table_Internalname = "GRID1TABLE";
         divGroup1table_Internalname = "GROUP1TABLE";
         grpGroup1_Internalname = "GROUP1";
         divMaintable_Internalname = "MAINTABLE";
         Form.Internalname = "FORM";
         subGrid1_Internalname = "GRID1";
      }

      public override void initialize_properties( )
      {
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
         if ( context.isSpaRequest( ) )
         {
            disableJsOutput();
         }
         init_default_properties( ) ;
         subGrid1_Allowcollapsing = 0;
         edtavCtlvalue3_Jsonclick = "";
         edtavCtlvalue3_Enabled = 0;
         edtavCtln3_Jsonclick = "";
         edtavCtln3_Enabled = 0;
         edtavCtlscriptpubkey_address1_Enabled = 0;
         subGrid1_Class = "FreeStyleGrid";
         subGrid1_Backcolorstyle = 0;
         edtavCtlvalue3_Enabled = -1;
         edtavCtln3_Enabled = -1;
         edtavCtlscriptpubkey_address1_Enabled = -1;
         edtavCtldatetime2_Enabled = -1;
         edtavCtln2_Enabled = -1;
         edtavCtltransactionid2_Enabled = -1;
         edtavCtlscriptpubkey_address_Enabled = -1;
         edtavCtldatetime_Enabled = -1;
         edtavCtlvalue_Enabled = -1;
         edtavCtln_Enabled = -1;
         edtavCtltransactionid_Enabled = -1;
         edtavCtldescription_Enabled = -1;
         edtavCtldatetime2_Jsonclick = "";
         edtavCtldatetime2_Enabled = 0;
         edtavCtln2_Jsonclick = "";
         edtavCtln2_Enabled = 0;
         edtavCtltransactionid2_Jsonclick = "";
         edtavCtltransactionid2_Enabled = 0;
         grpGroup1_Visible = 1;
         edtavCtlscriptpubkey_address_Enabled = 0;
         edtavCtldatetime_Jsonclick = "";
         edtavCtldatetime_Enabled = 0;
         edtavCtlvalue_Jsonclick = "";
         edtavCtlvalue_Enabled = 0;
         edtavCtln_Jsonclick = "";
         edtavCtln_Enabled = 0;
         edtavCtltransactionid_Jsonclick = "";
         edtavCtltransactionid_Enabled = 0;
         bttEditdescription_Visible = 1;
         edtavCtldescription_Enabled = 0;
         Form.Headerrawhtml = "";
         Form.Background = "";
         Form.Textcolor = 0;
         Form.Backcolor = (int)(0xFFFFFF);
         Form.Caption = "Transaction Detail";
         if ( context.isSpaRequest( ) )
         {
            enableJsOutput();
         }
      }

      public override bool SupportAjaxEvent( )
      {
         return true ;
      }

      public override void InitializeDynEvents( )
      {
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"GRID1_nFirstRecordOnPage"},{"av":"GRID1_nEOF"},{"av":"AV15showTransactionItem","fld":"vSHOWTRANSACTIONITEM"},{"av":"nRC_GXsfl_57","ctrl":"GRID1","prop":"GridRC","grid":57},{"av":"AV16error","fld":"vERROR","hsh":true},{"av":"AV19transactionFileName","fld":"vTRANSACTIONFILENAME","hsh":true},{"av":"AV18transactionId","fld":"vTRANSACTIONID","hsh":true},{"av":"AV23n","fld":"vN","pic":"ZZZZZZZZZ9","hsh":true},{"av":"AV17usedTransactionId","fld":"vUSEDTRANSACTIONID","hsh":true}]""");
         setEventMetadata("REFRESH",""","oparms":[{"av":"AV15showTransactionItem","fld":"vSHOWTRANSACTIONITEM"},{"av":"GRID1_nFirstRecordOnPage"},{"av":"nRC_GXsfl_57","ctrl":"GRID1","prop":"GridRC","grid":57},{"av":"AV17usedTransactionId","fld":"vUSEDTRANSACTIONID","hsh":true},{"av":"grpGroup1_Visible","ctrl":"GROUP1","prop":"Visible"},{"ctrl":"EDITDESCRIPTION","prop":"Visible"}]}""");
         setEventMetadata("'EDIT DESCRIPTION'","""{"handler":"E110A1","iparms":[{"av":"AV18transactionId","fld":"vTRANSACTIONID","hsh":true},{"av":"AV23n","fld":"vN","pic":"ZZZZZZZZZ9","hsh":true},{"av":"AV19transactionFileName","fld":"vTRANSACTIONFILENAME","hsh":true}]}""");
         setEventMetadata("GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED","""{"handler":"E120A2","iparms":[{"av":"GRID1_nFirstRecordOnPage"},{"av":"GRID1_nEOF"},{"av":"AV15showTransactionItem","fld":"vSHOWTRANSACTIONITEM"},{"av":"nRC_GXsfl_57","ctrl":"GRID1","prop":"GridRC","grid":57},{"av":"AV16error","fld":"vERROR","hsh":true},{"av":"AV19transactionFileName","fld":"vTRANSACTIONFILENAME","hsh":true},{"av":"AV18transactionId","fld":"vTRANSACTIONID","hsh":true},{"av":"AV23n","fld":"vN","pic":"ZZZZZZZZZ9","hsh":true},{"av":"AV17usedTransactionId","fld":"vUSEDTRANSACTIONID","hsh":true},{"av":"AV25PopupName","fld":"vPOPUPNAME"}]""");
         setEventMetadata("GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED",""","oparms":[{"av":"AV15showTransactionItem","fld":"vSHOWTRANSACTIONITEM"},{"av":"GRID1_nFirstRecordOnPage"},{"av":"nRC_GXsfl_57","ctrl":"GRID1","prop":"GridRC","grid":57},{"av":"AV17usedTransactionId","fld":"vUSEDTRANSACTIONID","hsh":true},{"av":"grpGroup1_Visible","ctrl":"GROUP1","prop":"Visible"},{"ctrl":"EDITDESCRIPTION","prop":"Visible"}]}""");
         setEventMetadata("'CLOSE'","""{"handler":"E130A2","iparms":[]}""");
         setEventMetadata("NULL","""{"handler":"Validv_Gxv13","iparms":[]}""");
         return  ;
      }

      public override void cleanup( )
      {
         CloseCursors();
         if ( IsMain )
         {
            context.CloseConnections();
         }
      }

      public override void initialize( )
      {
         wcpOAV18transactionId = "";
         wcpOAV19transactionFileName = "";
         gxfirstwebparm = "";
         gxfirstwebparm_bkp = "";
         AV16error = "";
         AV17usedTransactionId = "";
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         AV15showTransactionItem = new GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem(context);
         AV25PopupName = "";
         GX_FocusControl = "";
         Form = new GXWebForm();
         sPrefix = "";
         TempTags = "";
         ClassString = "";
         StyleString = "";
         bttClose_Jsonclick = "";
         bttEditdescription_Jsonclick = "";
         Grid1Container = new GXWebGrid( context);
         sStyleString = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         AV13StoredTransactions = new GeneXus.Programs.wallet.SdtStoredTransactions(context);
         AV14TransactionItem = new GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem(context);
         AV26webpanelPopupName = "";
         Grid1Row = new GXWebRow();
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         subGrid1_Linesclass = "";
         ROClassString = "";
         subGrid1_Header = "";
         Grid1Column = new GXWebColumn();
         /* GeneXus formulas. */
         edtavCtldescription_Enabled = 0;
         edtavCtltransactionid_Enabled = 0;
         edtavCtln_Enabled = 0;
         edtavCtlvalue_Enabled = 0;
         edtavCtldatetime_Enabled = 0;
         edtavCtlscriptpubkey_address_Enabled = 0;
         edtavCtltransactionid2_Enabled = 0;
         edtavCtln2_Enabled = 0;
         edtavCtldatetime2_Enabled = 0;
         edtavCtlscriptpubkey_address1_Enabled = 0;
         edtavCtln3_Enabled = 0;
         edtavCtlvalue3_Enabled = 0;
      }

      private short nGotPars ;
      private short GxWebError ;
      private short gxajaxcallmode ;
      private short wbEnd ;
      private short wbStart ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short subGrid1_Backcolorstyle ;
      private short GRID1_nEOF ;
      private short AV27strFound ;
      private short nGXWrapped ;
      private short subGrid1_Backstyle ;
      private short subGrid1_Allowselection ;
      private short subGrid1_Allowhovering ;
      private short subGrid1_Allowcollapsing ;
      private short subGrid1_Collapsed ;
      private int nRC_GXsfl_57 ;
      private int nGXsfl_57_idx=1 ;
      private int edtavCtldescription_Enabled ;
      private int bttEditdescription_Visible ;
      private int edtavCtltransactionid_Enabled ;
      private int edtavCtln_Enabled ;
      private int edtavCtlvalue_Enabled ;
      private int edtavCtldatetime_Enabled ;
      private int edtavCtlscriptpubkey_address_Enabled ;
      private int grpGroup1_Visible ;
      private int edtavCtltransactionid2_Enabled ;
      private int edtavCtln2_Enabled ;
      private int edtavCtldatetime2_Enabled ;
      private int AV37GXV10 ;
      private int subGrid1_Islastpage ;
      private int edtavCtlscriptpubkey_address1_Enabled ;
      private int edtavCtln3_Enabled ;
      private int edtavCtlvalue3_Enabled ;
      private int nGXsfl_57_fel_idx=1 ;
      private int AV41GXV14 ;
      private int nGXsfl_57_bak_idx=1 ;
      private int idxLst ;
      private int subGrid1_Backcolor ;
      private int subGrid1_Allbackcolor ;
      private int subGrid1_Selectedindex ;
      private int subGrid1_Selectioncolor ;
      private int subGrid1_Hoveringcolor ;
      private long AV23n ;
      private long wcpOAV23n ;
      private long GRID1_nCurrentRecord ;
      private long GRID1_nFirstRecordOnPage ;
      private string AV18transactionId ;
      private string AV19transactionFileName ;
      private string wcpOAV18transactionId ;
      private string wcpOAV19transactionFileName ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sGXsfl_57_idx="0001" ;
      private string AV16error ;
      private string AV17usedTransactionId ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string AV25PopupName ;
      private string GX_FocusControl ;
      private string sPrefix ;
      private string divMaintable_Internalname ;
      private string TempTags ;
      private string ClassString ;
      private string StyleString ;
      private string bttClose_Internalname ;
      private string bttClose_Jsonclick ;
      private string edtavCtldescription_Internalname ;
      private string bttEditdescription_Internalname ;
      private string bttEditdescription_Jsonclick ;
      private string edtavCtltransactionid_Internalname ;
      private string edtavCtltransactionid_Jsonclick ;
      private string edtavCtln_Internalname ;
      private string edtavCtln_Jsonclick ;
      private string edtavCtlvalue_Internalname ;
      private string edtavCtlvalue_Jsonclick ;
      private string edtavCtldatetime_Internalname ;
      private string edtavCtldatetime_Jsonclick ;
      private string edtavCtlscriptpubkey_address_Internalname ;
      private string grpGroup1_Internalname ;
      private string divGroup1table_Internalname ;
      private string edtavCtltransactionid2_Internalname ;
      private string edtavCtltransactionid2_Jsonclick ;
      private string edtavCtln2_Internalname ;
      private string edtavCtln2_Jsonclick ;
      private string edtavCtldatetime2_Internalname ;
      private string edtavCtldatetime2_Jsonclick ;
      private string sStyleString ;
      private string subGrid1_Internalname ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string edtavCtlscriptpubkey_address1_Internalname ;
      private string edtavCtln3_Internalname ;
      private string edtavCtlvalue3_Internalname ;
      private string sGXsfl_57_fel_idx="0001" ;
      private string AV26webpanelPopupName ;
      private string subGrid1_Class ;
      private string subGrid1_Linesclass ;
      private string divGrid1table_Internalname ;
      private string ROClassString ;
      private string edtavCtln3_Jsonclick ;
      private string edtavCtlvalue3_Jsonclick ;
      private string subGrid1_Header ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool bGXsfl_57_Refreshing=false ;
      private bool gxdyncontrolsrefreshing ;
      private bool gx_refresh_fired ;
      private bool returnInSub ;
      private bool gx_BV57 ;
      private GXWebGrid Grid1Container ;
      private GXWebRow Grid1Row ;
      private GXWebColumn Grid1Column ;
      private IGxDataStore dsDefault ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
      private GXWebForm Form ;
      private GeneXus.Programs.wallet.SdtStoredTransactions AV13StoredTransactions ;
      private GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem AV15showTransactionItem ;
      private GeneXus.Programs.wallet.SdtStoredTransactions_TransactionItem AV14TransactionItem ;
   }

}
