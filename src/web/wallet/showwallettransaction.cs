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
using System.Text.Json.Serialization;
namespace GeneXus.Programs.wallet {
   public class showwallettransaction : GXHttpHandler
   {
      public showwallettransaction( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public showwallettransaction( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( string aP0_transactionId ,
                           string aP1_transactionFileName )
      {
         this.AV13transactionId = aP0_transactionId;
         this.AV12transactionFileName = aP1_transactionFileName;
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
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxNewRow_"+"Inputsgrid") == 0 )
            {
               gxnrInputsgrid_newrow_invoke( ) ;
               return  ;
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxGridRefresh_"+"Inputsgrid") == 0 )
            {
               gxgrInputsgrid_refresh_invoke( ) ;
               return  ;
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxNewRow_"+"Outputsgrid") == 0 )
            {
               gxnrOutputsgrid_newrow_invoke( ) ;
               return  ;
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxGridRefresh_"+"Outputsgrid") == 0 )
            {
               gxgrOutputsgrid_refresh_invoke( ) ;
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
               AV13transactionId = gxfirstwebparm;
               AssignAttri("", false, "AV13transactionId", AV13transactionId);
               GxWebStd.gx_hidden_field( context, "gxhash_vTRANSACTIONID", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV13transactionId, "")), context));
               if ( StringUtil.StrCmp(gxfirstwebparm, "viewer") != 0 )
               {
                  AV12transactionFileName = GetPar( "transactionFileName");
                  AssignAttri("", false, "AV12transactionFileName", AV12transactionFileName);
                  GxWebStd.gx_hidden_field( context, "gxhash_vTRANSACTIONFILENAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV12transactionFileName, "")), context));
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

      protected void gxnrInputsgrid_newrow_invoke( )
      {
         nRC_GXsfl_25 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_25"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_25_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_25_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_25_idx = GetPar( "sGXsfl_25_idx");
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxnrInputsgrid_newrow( ) ;
         /* End function gxnrInputsgrid_newrow_invoke */
      }

      protected void gxgrInputsgrid_refresh_invoke( )
      {
         subInputsgrid_Rows = (int)(Math.Round(NumberUtil.Val( GetPar( "subInputsgrid_Rows"), "."), 18, MidpointRounding.ToEven));
         subOutputsgrid_Rows = (int)(Math.Round(NumberUtil.Val( GetPar( "subOutputsgrid_Rows"), "."), 18, MidpointRounding.ToEven));
         AV12transactionFileName = GetPar( "transactionFileName");
         AV13transactionId = GetPar( "transactionId");
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrInputsgrid_refresh( subInputsgrid_Rows, subOutputsgrid_Rows, AV12transactionFileName, AV13transactionId) ;
         AddString( context.getJSONResponse( )) ;
         /* End function gxgrInputsgrid_refresh_invoke */
      }

      protected void gxnrOutputsgrid_newrow_invoke( )
      {
         nRC_GXsfl_35 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_35"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_35_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_35_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_35_idx = GetPar( "sGXsfl_35_idx");
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxnrOutputsgrid_newrow( ) ;
         /* End function gxnrOutputsgrid_newrow_invoke */
      }

      protected void gxgrOutputsgrid_refresh_invoke( )
      {
         subInputsgrid_Rows = (int)(Math.Round(NumberUtil.Val( GetPar( "subInputsgrid_Rows"), "."), 18, MidpointRounding.ToEven));
         subOutputsgrid_Rows = (int)(Math.Round(NumberUtil.Val( GetPar( "subOutputsgrid_Rows"), "."), 18, MidpointRounding.ToEven));
         AV12transactionFileName = GetPar( "transactionFileName");
         AV13transactionId = GetPar( "transactionId");
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrOutputsgrid_refresh( subInputsgrid_Rows, subOutputsgrid_Rows, AV12transactionFileName, AV13transactionId) ;
         AddString( context.getJSONResponse( )) ;
         /* End function gxgrOutputsgrid_refresh_invoke */
      }

      public override void webExecute( )
      {
         createObjects();
         initialize();
         INITWEB( ) ;
         if ( ! isAjaxCallMode( ) )
         {
            ValidateSpaRequest();
            PA3O2( ) ;
            if ( ( GxWebError == 0 ) && ! isAjaxCallMode( ) )
            {
               /* GeneXus formulas. */
               edtavShowntransactionid_Enabled = 0;
               AssignProp("", false, edtavShowntransactionid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavShowntransactionid_Enabled), 5, 0), true);
               edtavExplorerlink_Enabled = 0;
               AssignProp("", false, edtavExplorerlink_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavExplorerlink_Enabled), 5, 0), true);
               edtavCtlinowner_Enabled = 0;
               AssignProp("", false, edtavCtlinowner_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlinowner_Enabled), 5, 0), !bGXsfl_25_Refreshing);
               edtavCtlinaddress_Enabled = 0;
               AssignProp("", false, edtavCtlinaddress_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlinaddress_Enabled), 5, 0), !bGXsfl_25_Refreshing);
               edtavCtlinvalue_Enabled = 0;
               AssignProp("", false, edtavCtlinvalue_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlinvalue_Enabled), 5, 0), !bGXsfl_25_Refreshing);
               edtavCtlinoutpoint_Enabled = 0;
               AssignProp("", false, edtavCtlinoutpoint_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlinoutpoint_Enabled), 5, 0), !bGXsfl_25_Refreshing);
               edtavCtloutn_Enabled = 0;
               AssignProp("", false, edtavCtloutn_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtloutn_Enabled), 5, 0), !bGXsfl_35_Refreshing);
               edtavCtloutowner_Enabled = 0;
               AssignProp("", false, edtavCtloutowner_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtloutowner_Enabled), 5, 0), !bGXsfl_35_Refreshing);
               edtavCtloutaddress_Enabled = 0;
               AssignProp("", false, edtavCtloutaddress_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtloutaddress_Enabled), 5, 0), !bGXsfl_35_Refreshing);
               edtavCtloutvalue_Enabled = 0;
               AssignProp("", false, edtavCtloutvalue_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtloutvalue_Enabled), 5, 0), !bGXsfl_35_Refreshing);
               WS3O2( ) ;
               if ( ! isAjaxCallMode( ) )
               {
                  WE3O2( ) ;
               }
            }
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
         cleanup();
      }

      protected void RenderHtmlHeaders( )
      {
         GxWebStd.gx_html_headers( context, 0, "", "", Form.Meta, Form.Metaequiv, true);
      }

      protected void RenderHtmlOpenForm( )
      {
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         context.WriteHtmlText( "<title>") ;
         context.SendWebValue( "Transaction") ;
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
         CloseStyles();
         if ( ( ( context.GetBrowserType( ) == 1 ) || ( context.GetBrowserType( ) == 5 ) ) && ( StringUtil.StrCmp(context.GetBrowserVersion( ), "7.0") == 0 ) )
         {
            context.AddJavascriptSource("json2.js", "?"+context.GetBuildNumber( 1550520), false, true, false);
         }
         context.AddJavascriptSource("jquery.js", "?"+context.GetBuildNumber( 1550520), false, true, false);
         context.AddJavascriptSource("gxgral.js", "?"+context.GetBuildNumber( 1550520), false, true, false);
         context.AddJavascriptSource("gxcfg.js", "?"+GetCacheInvalidationToken( ), false, true, false);
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
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
         bodyStyle = "";
         if ( nGXWrapped == 0 )
         {
            bodyStyle += "-moz-opacity:0;opacity:0;";
         }
         context.WriteHtmlText( " "+"class=\"form-horizontal Form\""+" "+ "style='"+bodyStyle+"'") ;
         context.WriteHtmlText( FormProcess+">") ;
         context.skipLines(1);
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wallet.showwallettransaction", new object[] {UrlEncode(StringUtil.RTrim(AV13transactionId)),UrlEncode(StringUtil.RTrim(AV12transactionFileName))}, new string[] {"transactionId","transactionFileName"}) +"\">") ;
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
         GxWebStd.gx_hidden_field( context, "vTRANSACTIONFILENAME", StringUtil.RTrim( AV12transactionFileName));
         GxWebStd.gx_hidden_field( context, "gxhash_vTRANSACTIONFILENAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV12transactionFileName, "")), context));
         GxWebStd.gx_hidden_field( context, "vTRANSACTIONID", StringUtil.RTrim( AV13transactionId));
         GxWebStd.gx_hidden_field( context, "gxhash_vTRANSACTIONID", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV13transactionId, "")), context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "Outputs", AV10outputs);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("Outputs", AV10outputs);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "Inputs", AV9inputs);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("Inputs", AV9inputs);
         }
         GxWebStd.gx_hidden_field( context, "nRC_GXsfl_25", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_25), 8, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "nRC_GXsfl_35", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_35), 8, 0, ".", "")));
         GxWebStd.gx_boolean_hidden_field( context, "vUSERRESPONSE", AV15userResponse);
         GxWebStd.gx_hidden_field( context, "vTRANSACTIONFILENAME", StringUtil.RTrim( AV12transactionFileName));
         GxWebStd.gx_hidden_field( context, "gxhash_vTRANSACTIONFILENAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV12transactionFileName, "")), context));
         GxWebStd.gx_hidden_field( context, "vTRANSACTIONID", StringUtil.RTrim( AV13transactionId));
         GxWebStd.gx_hidden_field( context, "gxhash_vTRANSACTIONID", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV13transactionId, "")), context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vINPUTS", AV9inputs);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vINPUTS", AV9inputs);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vOUTPUTS", AV10outputs);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vOUTPUTS", AV10outputs);
         }
         GxWebStd.gx_hidden_field( context, "INPUTSGRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(INPUTSGRID_nFirstRecordOnPage), 15, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "OUTPUTSGRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(OUTPUTSGRID_nFirstRecordOnPage), 15, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "INPUTSGRID_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(INPUTSGRID_nEOF), 1, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "OUTPUTSGRID_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(OUTPUTSGRID_nEOF), 1, 0, ".", "")));
      }

      protected void RenderHtmlCloseForm3O2( )
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
         context.WriteHtmlTextNl( "</body>") ;
         context.WriteHtmlTextNl( "</html>") ;
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
      }

      public override string GetPgmname( )
      {
         return "Wallet.ShowWalletTransaction" ;
      }

      public override string GetPgmdesc( )
      {
         return "Transaction" ;
      }

      protected void WB3O0( )
      {
         if ( context.isAjaxRequest( ) )
         {
            disableOutput();
         }
         if ( ! wbLoad )
         {
            RenderHtmlHeaders( ) ;
            RenderHtmlOpenForm( ) ;
            GxWebStd.gx_msg_list( context, "", context.GX_msglist.DisplayMode, "", "", "", "false");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "Section", "start", "top", " "+"data-gx-base-lib=\"none\""+" "+"data-abstract-form"+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divMaintable_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-4", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTbheader_Internalname, lblTbheader_Caption, "", "", lblTbheader_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 0, "HLP_Wallet/ShowWalletTransaction.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-4", "end", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 8,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttForgetpayment_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(25), 2, 0)+","+"null"+");", "Forget this payment", bttForgetpayment_Jsonclick, 7, "Forget this payment", "", StyleString, ClassString, bttForgetpayment_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"e113o1_client"+"'", TempTags, "", 2, "HLP_Wallet/ShowWalletTransaction.htm");
            GxWebStd.gx_div_end( context, "end", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-4", "end", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 10,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttClose_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(25), 2, 0)+","+"null"+");", "Close", bttClose_Jsonclick, 5, "Close", "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'CLOSE\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/ShowWalletTransaction.htm");
            GxWebStd.gx_div_end( context, "end", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavShowntransactionid_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavShowntransactionid_Internalname, "Transaction Id", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 15,'',false,'" + sGXsfl_25_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavShowntransactionid_Internalname, AV11shownTransactionId, StringUtil.RTrim( context.localUtil.Format( AV11shownTransactionId, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,15);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavShowntransactionid_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavShowntransactionid_Enabled, 0, "text", "", 80, "chr", 1, "row", 100, 0, 0, 0, 0, -1, -1, true, "", "start", true, "", "HLP_Wallet/ShowWalletTransaction.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavExplorerlink_Internalname+"\"", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 19,'',false,'" + sGXsfl_25_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavExplorerlink_Internalname, StringUtil.RTrim( AV6explorerLink), StringUtil.RTrim( context.localUtil.Format( AV6explorerLink, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,19);\"", "'"+""+"'"+",false,"+"'"+""+"'", edtavExplorerlink_Link, edtavExplorerlink_Linktarget, "", "", edtavExplorerlink_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavExplorerlink_Enabled, 0, "text", "", 60, "chr", 1, "row", 60, 0, 0, 0, 0, -1, -1, true, "", "start", true, "", "HLP_Wallet/ShowWalletTransaction.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTbinputs_Internalname, "Inputs (coins spent)", "", "", lblTbinputs_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 0, "HLP_Wallet/ShowWalletTransaction.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /*  Grid Control  */
            InputsgridContainer.SetWrapped(nGXWrapped);
            StartGridControl25( ) ;
         }
         if ( wbEnd == 25 )
         {
            wbEnd = 0;
            nRC_GXsfl_25 = (int)(nGXsfl_25_idx-1);
            if ( InputsgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "</table>") ;
               context.WriteHtmlText( "</div>") ;
            }
            else
            {
               InputsgridContainer.AddObjectProperty("INPUTSGRID_nEOF", INPUTSGRID_nEOF);
               InputsgridContainer.AddObjectProperty("INPUTSGRID_nFirstRecordOnPage", INPUTSGRID_nFirstRecordOnPage);
               AV16GXV1 = nGXsfl_25_idx;
               sStyleString = "";
               context.WriteHtmlText( "<div id=\""+"InputsgridContainer"+"Div\" "+sStyleString+">"+"</div>") ;
               context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Inputsgrid", InputsgridContainer, subInputsgrid_Internalname);
               if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "InputsgridContainerData", InputsgridContainer.ToJavascriptSource());
               }
               if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "InputsgridContainerData"+"V", InputsgridContainer.GridValuesHidden());
               }
               else
               {
                  context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"InputsgridContainerData"+"V"+"\" value='"+InputsgridContainer.GridValuesHidden()+"'/>") ;
               }
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTboutputs_Internalname, "Outputs", "", "", lblTboutputs_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 0, "HLP_Wallet/ShowWalletTransaction.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /*  Grid Control  */
            OutputsgridContainer.SetWrapped(nGXWrapped);
            StartGridControl35( ) ;
         }
         if ( wbEnd == 35 )
         {
            wbEnd = 0;
            nRC_GXsfl_35 = (int)(nGXsfl_35_idx-1);
            if ( OutputsgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "</table>") ;
               context.WriteHtmlText( "</div>") ;
            }
            else
            {
               OutputsgridContainer.AddObjectProperty("OUTPUTSGRID_nEOF", OUTPUTSGRID_nEOF);
               OutputsgridContainer.AddObjectProperty("OUTPUTSGRID_nFirstRecordOnPage", OUTPUTSGRID_nFirstRecordOnPage);
               AV21GXV6 = nGXsfl_35_idx;
               sStyleString = "";
               context.WriteHtmlText( "<div id=\""+"OutputsgridContainer"+"Div\" "+sStyleString+">"+"</div>") ;
               context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Outputsgrid", OutputsgridContainer, subOutputsgrid_Internalname);
               if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "OutputsgridContainerData", OutputsgridContainer.ToJavascriptSource());
               }
               if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "OutputsgridContainerData"+"V", OutputsgridContainer.GridValuesHidden());
               }
               else
               {
                  context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"OutputsgridContainerData"+"V"+"\" value='"+OutputsgridContainer.GridValuesHidden()+"'/>") ;
               }
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         if ( wbEnd == 25 )
         {
            wbEnd = 0;
            if ( isFullAjaxMode( ) )
            {
               if ( InputsgridContainer.GetWrapped() == 1 )
               {
                  context.WriteHtmlText( "</table>") ;
                  context.WriteHtmlText( "</div>") ;
               }
               else
               {
                  InputsgridContainer.AddObjectProperty("INPUTSGRID_nEOF", INPUTSGRID_nEOF);
                  InputsgridContainer.AddObjectProperty("INPUTSGRID_nFirstRecordOnPage", INPUTSGRID_nFirstRecordOnPage);
                  AV16GXV1 = nGXsfl_25_idx;
                  sStyleString = "";
                  context.WriteHtmlText( "<div id=\""+"InputsgridContainer"+"Div\" "+sStyleString+">"+"</div>") ;
                  context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Inputsgrid", InputsgridContainer, subInputsgrid_Internalname);
                  if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "InputsgridContainerData", InputsgridContainer.ToJavascriptSource());
                  }
                  if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "InputsgridContainerData"+"V", InputsgridContainer.GridValuesHidden());
                  }
                  else
                  {
                     context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"InputsgridContainerData"+"V"+"\" value='"+InputsgridContainer.GridValuesHidden()+"'/>") ;
                  }
               }
            }
         }
         if ( wbEnd == 35 )
         {
            wbEnd = 0;
            if ( isFullAjaxMode( ) )
            {
               if ( OutputsgridContainer.GetWrapped() == 1 )
               {
                  context.WriteHtmlText( "</table>") ;
                  context.WriteHtmlText( "</div>") ;
               }
               else
               {
                  OutputsgridContainer.AddObjectProperty("OUTPUTSGRID_nEOF", OUTPUTSGRID_nEOF);
                  OutputsgridContainer.AddObjectProperty("OUTPUTSGRID_nFirstRecordOnPage", OUTPUTSGRID_nFirstRecordOnPage);
                  AV21GXV6 = nGXsfl_35_idx;
                  sStyleString = "";
                  context.WriteHtmlText( "<div id=\""+"OutputsgridContainer"+"Div\" "+sStyleString+">"+"</div>") ;
                  context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Outputsgrid", OutputsgridContainer, subOutputsgrid_Internalname);
                  if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "OutputsgridContainerData", OutputsgridContainer.ToJavascriptSource());
                  }
                  if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "OutputsgridContainerData"+"V", OutputsgridContainer.GridValuesHidden());
                  }
                  else
                  {
                     context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"OutputsgridContainerData"+"V"+"\" value='"+OutputsgridContainer.GridValuesHidden()+"'/>") ;
                  }
               }
            }
         }
         wbLoad = true;
      }

      protected void START3O2( )
      {
         wbLoad = false;
         wbEnd = 0;
         wbStart = 0;
         if ( ! context.isSpaRequest( ) )
         {
            if ( context.ExposeMetadata( ) )
            {
               Form.Meta.addItem("generator", "GeneXus .NET 18_0_16-189595", 0) ;
            }
         }
         Form.Meta.addItem("description", "Transaction", 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         wbErr = false;
         STRUP3O0( ) ;
      }

      protected void WS3O2( )
      {
         START3O2( ) ;
         EVT3O2( ) ;
      }

      protected void EVT3O2( )
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
                        else if ( StringUtil.StrCmp(sEvt, "'CLOSE'") == 0 )
                        {
                           context.wbHandled = 1;
                           dynload_actions( ) ;
                           /* Execute user event: 'Close' */
                           E123O2 ();
                        }
                        else if ( StringUtil.StrCmp(sEvt, "GX.EXTENSIONS.WEB.DIALOGS.ONCONFIRMCLOSED") == 0 )
                        {
                           context.wbHandled = 1;
                           dynload_actions( ) ;
                           E133O2 ();
                        }
                        else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                        {
                           context.wbHandled = 1;
                           dynload_actions( ) ;
                        }
                        else if ( StringUtil.StrCmp(sEvt, "INPUTSGRIDPAGING") == 0 )
                        {
                           context.wbHandled = 1;
                           sEvt = cgiGet( "INPUTSGRIDPAGING");
                           if ( StringUtil.StrCmp(sEvt, "FIRST") == 0 )
                           {
                              subinputsgrid_firstpage( ) ;
                           }
                           else if ( StringUtil.StrCmp(sEvt, "PREV") == 0 )
                           {
                              subinputsgrid_previouspage( ) ;
                           }
                           else if ( StringUtil.StrCmp(sEvt, "NEXT") == 0 )
                           {
                              subinputsgrid_nextpage( ) ;
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LAST") == 0 )
                           {
                              subinputsgrid_lastpage( ) ;
                           }
                        }
                        else if ( StringUtil.StrCmp(sEvt, "OUTPUTSGRIDPAGING") == 0 )
                        {
                           context.wbHandled = 1;
                           sEvt = cgiGet( "OUTPUTSGRIDPAGING");
                           if ( StringUtil.StrCmp(sEvt, "FIRST") == 0 )
                           {
                              suboutputsgrid_firstpage( ) ;
                           }
                           else if ( StringUtil.StrCmp(sEvt, "PREV") == 0 )
                           {
                              suboutputsgrid_previouspage( ) ;
                           }
                           else if ( StringUtil.StrCmp(sEvt, "NEXT") == 0 )
                           {
                              suboutputsgrid_nextpage( ) ;
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LAST") == 0 )
                           {
                              suboutputsgrid_lastpage( ) ;
                           }
                           dynload_actions( ) ;
                        }
                     }
                     else
                     {
                        sEvtType = StringUtil.Right( sEvt, 4);
                        sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-4));
                        if ( StringUtil.StrCmp(StringUtil.Left( sEvt, 16), "OUTPUTSGRID.LOAD") == 0 )
                        {
                           nGXsfl_35_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                           sGXsfl_35_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_35_idx), 4, 0), 4, "0");
                           SubsflControlProps_353( ) ;
                           AV21GXV6 = (int)(nGXsfl_35_idx+OUTPUTSGRID_nFirstRecordOnPage);
                           if ( ( AV10outputs.Count >= AV21GXV6 ) && ( AV21GXV6 > 0 ) )
                           {
                              AV10outputs.CurrentItem = ((GeneXus.Programs.wallet.SdtTransactionLine)AV10outputs.Item(AV21GXV6));
                           }
                           sEvtType = StringUtil.Right( sEvt, 1);
                           if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                           {
                              sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                              if ( StringUtil.StrCmp(sEvt, "OUTPUTSGRID.LOAD") == 0 )
                              {
                                 context.wbHandled = 1;
                                 dynload_actions( ) ;
                                 /* Execute user event: Outputsgrid.Load */
                                 E143O3 ();
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
                        else if ( ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "START") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 15), "INPUTSGRID.LOAD") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "ENTER") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 6), "CANCEL") == 0 ) )
                        {
                           nGXsfl_25_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                           sGXsfl_25_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_25_idx), 4, 0), 4, "0");
                           SubsflControlProps_252( ) ;
                           AV16GXV1 = (int)(nGXsfl_25_idx+INPUTSGRID_nFirstRecordOnPage);
                           if ( ( AV9inputs.Count >= AV16GXV1 ) && ( AV16GXV1 > 0 ) )
                           {
                              AV9inputs.CurrentItem = ((GeneXus.Programs.wallet.SdtTransactionLine)AV9inputs.Item(AV16GXV1));
                           }
                           sEvtType = StringUtil.Right( sEvt, 1);
                           if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                           {
                              sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                              if ( StringUtil.StrCmp(sEvt, "START") == 0 )
                              {
                                 context.wbHandled = 1;
                                 dynload_actions( ) ;
                                 /* Execute user event: Start */
                                 E153O2 ();
                              }
                              else if ( StringUtil.StrCmp(sEvt, "INPUTSGRID.LOAD") == 0 )
                              {
                                 context.wbHandled = 1;
                                 dynload_actions( ) ;
                                 /* Execute user event: Inputsgrid.Load */
                                 E163O2 ();
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

      protected void WE3O2( )
      {
         if ( ! GxWebStd.gx_redirect( context) )
         {
            Rfr0gs = true;
            Refresh( ) ;
            if ( ! GxWebStd.gx_redirect( context) )
            {
               RenderHtmlCloseForm3O2( ) ;
            }
         }
      }

      protected void PA3O2( )
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
               GX_FocusControl = edtavShowntransactionid_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
            }
            nDonePA = 1;
         }
      }

      protected void dynload_actions( )
      {
         /* End function dynload_actions */
      }

      protected void gxnrInputsgrid_newrow( )
      {
         GxWebStd.set_html_headers( context, 0, "", "");
         SubsflControlProps_252( ) ;
         while ( nGXsfl_25_idx <= nRC_GXsfl_25 )
         {
            sendrow_252( ) ;
            nGXsfl_25_idx = ((subInputsgrid_Islastpage==1)&&(nGXsfl_25_idx+1>subInputsgrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_25_idx+1);
            sGXsfl_25_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_25_idx), 4, 0), 4, "0");
            SubsflControlProps_252( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( InputsgridContainer)) ;
         /* End function gxnrInputsgrid_newrow */
      }

      protected void gxnrOutputsgrid_newrow( )
      {
         GxWebStd.set_html_headers( context, 0, "", "");
         SubsflControlProps_353( ) ;
         while ( nGXsfl_35_idx <= nRC_GXsfl_35 )
         {
            sendrow_353( ) ;
            nGXsfl_35_idx = ((subOutputsgrid_Islastpage==1)&&(nGXsfl_35_idx+1>subOutputsgrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_35_idx+1);
            sGXsfl_35_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_35_idx), 4, 0), 4, "0");
            SubsflControlProps_353( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( OutputsgridContainer)) ;
         /* End function gxnrOutputsgrid_newrow */
      }

      protected void gxgrInputsgrid_refresh( int subInputsgrid_Rows ,
                                             int subOutputsgrid_Rows ,
                                             string AV12transactionFileName ,
                                             string AV13transactionId )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         INPUTSGRID_nCurrentRecord = 0;
         RF3O2( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         /* End function gxgrInputsgrid_refresh */
      }

      protected void gxgrOutputsgrid_refresh( int subInputsgrid_Rows ,
                                              int subOutputsgrid_Rows ,
                                              string AV12transactionFileName ,
                                              string AV13transactionId )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         OUTPUTSGRID_nCurrentRecord = 0;
         RF3O3( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         /* End function gxgrOutputsgrid_refresh */
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
         RF3O2( ) ;
         RF3O3( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         edtavShowntransactionid_Enabled = 0;
         AssignProp("", false, edtavShowntransactionid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavShowntransactionid_Enabled), 5, 0), true);
         edtavExplorerlink_Enabled = 0;
         AssignProp("", false, edtavExplorerlink_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavExplorerlink_Enabled), 5, 0), true);
         edtavCtlinowner_Enabled = 0;
         edtavCtlinaddress_Enabled = 0;
         edtavCtlinvalue_Enabled = 0;
         edtavCtlinoutpoint_Enabled = 0;
         edtavCtloutn_Enabled = 0;
         edtavCtloutowner_Enabled = 0;
         edtavCtloutaddress_Enabled = 0;
         edtavCtloutvalue_Enabled = 0;
      }

      protected void RF3O2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            InputsgridContainer.ClearRows();
         }
         wbStart = 25;
         nGXsfl_25_idx = 1;
         sGXsfl_25_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_25_idx), 4, 0), 4, "0");
         SubsflControlProps_252( ) ;
         bGXsfl_25_Refreshing = true;
         InputsgridContainer.AddObjectProperty("GridName", "Inputsgrid");
         InputsgridContainer.AddObjectProperty("CmpContext", "");
         InputsgridContainer.AddObjectProperty("InMasterPage", "false");
         InputsgridContainer.AddObjectProperty("Class", "Grid");
         InputsgridContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
         InputsgridContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
         InputsgridContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subInputsgrid_Backcolorstyle), 1, 0, ".", "")));
         InputsgridContainer.PageSize = subInputsgrid_fnc_Recordsperpage( );
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            SubsflControlProps_252( ) ;
            /* Execute user event: Inputsgrid.Load */
            E163O2 ();
            if ( ( subInputsgrid_Islastpage == 0 ) && ( INPUTSGRID_nCurrentRecord > 0 ) && ( INPUTSGRID_nGridOutOfScope == 0 ) && ( nGXsfl_25_idx == 1 ) )
            {
               INPUTSGRID_nCurrentRecord = 0;
               INPUTSGRID_nGridOutOfScope = 1;
               subinputsgrid_firstpage( ) ;
               /* Execute user event: Inputsgrid.Load */
               E163O2 ();
            }
            wbEnd = 25;
            WB3O0( ) ;
         }
         bGXsfl_25_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes3O2( )
      {
      }

      protected void RF3O3( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            OutputsgridContainer.ClearRows();
         }
         wbStart = 35;
         nGXsfl_35_idx = 1;
         sGXsfl_35_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_35_idx), 4, 0), 4, "0");
         SubsflControlProps_353( ) ;
         bGXsfl_35_Refreshing = true;
         OutputsgridContainer.AddObjectProperty("GridName", "Outputsgrid");
         OutputsgridContainer.AddObjectProperty("CmpContext", "");
         OutputsgridContainer.AddObjectProperty("InMasterPage", "false");
         OutputsgridContainer.AddObjectProperty("Class", "Grid");
         OutputsgridContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
         OutputsgridContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
         OutputsgridContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subOutputsgrid_Backcolorstyle), 1, 0, ".", "")));
         OutputsgridContainer.PageSize = subOutputsgrid_fnc_Recordsperpage( );
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            SubsflControlProps_353( ) ;
            /* Execute user event: Outputsgrid.Load */
            E143O3 ();
            if ( ( subOutputsgrid_Islastpage == 0 ) && ( OUTPUTSGRID_nCurrentRecord > 0 ) && ( OUTPUTSGRID_nGridOutOfScope == 0 ) && ( nGXsfl_35_idx == 1 ) )
            {
               OUTPUTSGRID_nCurrentRecord = 0;
               OUTPUTSGRID_nGridOutOfScope = 1;
               suboutputsgrid_firstpage( ) ;
               /* Execute user event: Outputsgrid.Load */
               E143O3 ();
            }
            wbEnd = 35;
            WB3O0( ) ;
         }
         bGXsfl_35_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes3O3( )
      {
      }

      protected int subInputsgrid_fnc_Pagecount( )
      {
         INPUTSGRID_nRecordCount = subInputsgrid_fnc_Recordcount( );
         if ( ((long)((INPUTSGRID_nRecordCount) % (subInputsgrid_fnc_Recordsperpage( )))) == 0 )
         {
            return (int)(NumberUtil.Int( (long)(Math.Round(INPUTSGRID_nRecordCount/ (decimal)(subInputsgrid_fnc_Recordsperpage( )), 18, MidpointRounding.ToEven)))) ;
         }
         return (int)(NumberUtil.Int( (long)(Math.Round(INPUTSGRID_nRecordCount/ (decimal)(subInputsgrid_fnc_Recordsperpage( )), 18, MidpointRounding.ToEven)))+1) ;
      }

      protected int subInputsgrid_fnc_Recordcount( )
      {
         return AV9inputs.Count ;
      }

      protected int subInputsgrid_fnc_Recordsperpage( )
      {
         return (int)(100*1) ;
      }

      protected int subInputsgrid_fnc_Currentpage( )
      {
         return (int)(NumberUtil.Int( (long)(Math.Round(INPUTSGRID_nFirstRecordOnPage/ (decimal)(subInputsgrid_fnc_Recordsperpage( )), 18, MidpointRounding.ToEven)))+1) ;
      }

      protected short subinputsgrid_firstpage( )
      {
         INPUTSGRID_nFirstRecordOnPage = 0;
         GxWebStd.gx_hidden_field( context, "INPUTSGRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(INPUTSGRID_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrInputsgrid_refresh( subInputsgrid_Rows, subOutputsgrid_Rows, AV12transactionFileName, AV13transactionId) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected short subinputsgrid_nextpage( )
      {
         INPUTSGRID_nRecordCount = subInputsgrid_fnc_Recordcount( );
         if ( ( INPUTSGRID_nRecordCount >= subInputsgrid_fnc_Recordsperpage( ) ) && ( INPUTSGRID_nEOF == 0 ) )
         {
            INPUTSGRID_nFirstRecordOnPage = (long)(INPUTSGRID_nFirstRecordOnPage+subInputsgrid_fnc_Recordsperpage( ));
         }
         else
         {
            return 2 ;
         }
         GxWebStd.gx_hidden_field( context, "INPUTSGRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(INPUTSGRID_nFirstRecordOnPage), 15, 0, ".", "")));
         InputsgridContainer.AddObjectProperty("INPUTSGRID_nFirstRecordOnPage", INPUTSGRID_nFirstRecordOnPage);
         if ( isFullAjaxMode( ) )
         {
            gxgrInputsgrid_refresh( subInputsgrid_Rows, subOutputsgrid_Rows, AV12transactionFileName, AV13transactionId) ;
         }
         send_integrity_footer_hashes( ) ;
         return (short)(((INPUTSGRID_nEOF==0) ? 0 : 2)) ;
      }

      protected short subinputsgrid_previouspage( )
      {
         if ( INPUTSGRID_nFirstRecordOnPage >= subInputsgrid_fnc_Recordsperpage( ) )
         {
            INPUTSGRID_nFirstRecordOnPage = (long)(INPUTSGRID_nFirstRecordOnPage-subInputsgrid_fnc_Recordsperpage( ));
         }
         else
         {
            return 2 ;
         }
         GxWebStd.gx_hidden_field( context, "INPUTSGRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(INPUTSGRID_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrInputsgrid_refresh( subInputsgrid_Rows, subOutputsgrid_Rows, AV12transactionFileName, AV13transactionId) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected short subinputsgrid_lastpage( )
      {
         INPUTSGRID_nRecordCount = subInputsgrid_fnc_Recordcount( );
         if ( INPUTSGRID_nRecordCount > subInputsgrid_fnc_Recordsperpage( ) )
         {
            if ( ((long)((INPUTSGRID_nRecordCount) % (subInputsgrid_fnc_Recordsperpage( )))) == 0 )
            {
               INPUTSGRID_nFirstRecordOnPage = (long)(INPUTSGRID_nRecordCount-subInputsgrid_fnc_Recordsperpage( ));
            }
            else
            {
               INPUTSGRID_nFirstRecordOnPage = (long)(INPUTSGRID_nRecordCount-((long)((INPUTSGRID_nRecordCount) % (subInputsgrid_fnc_Recordsperpage( )))));
            }
         }
         else
         {
            INPUTSGRID_nFirstRecordOnPage = 0;
         }
         GxWebStd.gx_hidden_field( context, "INPUTSGRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(INPUTSGRID_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrInputsgrid_refresh( subInputsgrid_Rows, subOutputsgrid_Rows, AV12transactionFileName, AV13transactionId) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected int subinputsgrid_gotopage( int nPageNo )
      {
         if ( nPageNo > 0 )
         {
            INPUTSGRID_nFirstRecordOnPage = (long)(subInputsgrid_fnc_Recordsperpage( )*(nPageNo-1));
         }
         else
         {
            INPUTSGRID_nFirstRecordOnPage = 0;
         }
         GxWebStd.gx_hidden_field( context, "INPUTSGRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(INPUTSGRID_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrInputsgrid_refresh( subInputsgrid_Rows, subOutputsgrid_Rows, AV12transactionFileName, AV13transactionId) ;
         }
         send_integrity_footer_hashes( ) ;
         return (int)(0) ;
      }

      protected int subOutputsgrid_fnc_Pagecount( )
      {
         OUTPUTSGRID_nRecordCount = subOutputsgrid_fnc_Recordcount( );
         if ( ((long)((OUTPUTSGRID_nRecordCount) % (subOutputsgrid_fnc_Recordsperpage( )))) == 0 )
         {
            return (int)(NumberUtil.Int( (long)(Math.Round(OUTPUTSGRID_nRecordCount/ (decimal)(subOutputsgrid_fnc_Recordsperpage( )), 18, MidpointRounding.ToEven)))) ;
         }
         return (int)(NumberUtil.Int( (long)(Math.Round(OUTPUTSGRID_nRecordCount/ (decimal)(subOutputsgrid_fnc_Recordsperpage( )), 18, MidpointRounding.ToEven)))+1) ;
      }

      protected int subOutputsgrid_fnc_Recordcount( )
      {
         return AV10outputs.Count ;
      }

      protected int subOutputsgrid_fnc_Recordsperpage( )
      {
         return (int)(100*1) ;
      }

      protected int subOutputsgrid_fnc_Currentpage( )
      {
         return (int)(NumberUtil.Int( (long)(Math.Round(OUTPUTSGRID_nFirstRecordOnPage/ (decimal)(subOutputsgrid_fnc_Recordsperpage( )), 18, MidpointRounding.ToEven)))+1) ;
      }

      protected short suboutputsgrid_firstpage( )
      {
         OUTPUTSGRID_nFirstRecordOnPage = 0;
         GxWebStd.gx_hidden_field( context, "OUTPUTSGRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(OUTPUTSGRID_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrOutputsgrid_refresh( subInputsgrid_Rows, subOutputsgrid_Rows, AV12transactionFileName, AV13transactionId) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected short suboutputsgrid_nextpage( )
      {
         OUTPUTSGRID_nRecordCount = subOutputsgrid_fnc_Recordcount( );
         if ( ( OUTPUTSGRID_nRecordCount >= subOutputsgrid_fnc_Recordsperpage( ) ) && ( OUTPUTSGRID_nEOF == 0 ) )
         {
            OUTPUTSGRID_nFirstRecordOnPage = (long)(OUTPUTSGRID_nFirstRecordOnPage+subOutputsgrid_fnc_Recordsperpage( ));
         }
         else
         {
            return 2 ;
         }
         GxWebStd.gx_hidden_field( context, "OUTPUTSGRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(OUTPUTSGRID_nFirstRecordOnPage), 15, 0, ".", "")));
         OutputsgridContainer.AddObjectProperty("OUTPUTSGRID_nFirstRecordOnPage", OUTPUTSGRID_nFirstRecordOnPage);
         if ( isFullAjaxMode( ) )
         {
            gxgrOutputsgrid_refresh( subInputsgrid_Rows, subOutputsgrid_Rows, AV12transactionFileName, AV13transactionId) ;
         }
         send_integrity_footer_hashes( ) ;
         return (short)(((OUTPUTSGRID_nEOF==0) ? 0 : 2)) ;
      }

      protected short suboutputsgrid_previouspage( )
      {
         if ( OUTPUTSGRID_nFirstRecordOnPage >= subOutputsgrid_fnc_Recordsperpage( ) )
         {
            OUTPUTSGRID_nFirstRecordOnPage = (long)(OUTPUTSGRID_nFirstRecordOnPage-subOutputsgrid_fnc_Recordsperpage( ));
         }
         else
         {
            return 2 ;
         }
         GxWebStd.gx_hidden_field( context, "OUTPUTSGRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(OUTPUTSGRID_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrOutputsgrid_refresh( subInputsgrid_Rows, subOutputsgrid_Rows, AV12transactionFileName, AV13transactionId) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected short suboutputsgrid_lastpage( )
      {
         OUTPUTSGRID_nRecordCount = subOutputsgrid_fnc_Recordcount( );
         if ( OUTPUTSGRID_nRecordCount > subOutputsgrid_fnc_Recordsperpage( ) )
         {
            if ( ((long)((OUTPUTSGRID_nRecordCount) % (subOutputsgrid_fnc_Recordsperpage( )))) == 0 )
            {
               OUTPUTSGRID_nFirstRecordOnPage = (long)(OUTPUTSGRID_nRecordCount-subOutputsgrid_fnc_Recordsperpage( ));
            }
            else
            {
               OUTPUTSGRID_nFirstRecordOnPage = (long)(OUTPUTSGRID_nRecordCount-((long)((OUTPUTSGRID_nRecordCount) % (subOutputsgrid_fnc_Recordsperpage( )))));
            }
         }
         else
         {
            OUTPUTSGRID_nFirstRecordOnPage = 0;
         }
         GxWebStd.gx_hidden_field( context, "OUTPUTSGRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(OUTPUTSGRID_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrOutputsgrid_refresh( subInputsgrid_Rows, subOutputsgrid_Rows, AV12transactionFileName, AV13transactionId) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected int suboutputsgrid_gotopage( int nPageNo )
      {
         if ( nPageNo > 0 )
         {
            OUTPUTSGRID_nFirstRecordOnPage = (long)(subOutputsgrid_fnc_Recordsperpage( )*(nPageNo-1));
         }
         else
         {
            OUTPUTSGRID_nFirstRecordOnPage = 0;
         }
         GxWebStd.gx_hidden_field( context, "OUTPUTSGRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(OUTPUTSGRID_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrOutputsgrid_refresh( subInputsgrid_Rows, subOutputsgrid_Rows, AV12transactionFileName, AV13transactionId) ;
         }
         send_integrity_footer_hashes( ) ;
         return (int)(0) ;
      }

      protected void before_start_formulas( )
      {
         edtavShowntransactionid_Enabled = 0;
         AssignProp("", false, edtavShowntransactionid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavShowntransactionid_Enabled), 5, 0), true);
         edtavExplorerlink_Enabled = 0;
         AssignProp("", false, edtavExplorerlink_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavExplorerlink_Enabled), 5, 0), true);
         edtavCtlinowner_Enabled = 0;
         edtavCtlinaddress_Enabled = 0;
         edtavCtlinvalue_Enabled = 0;
         edtavCtlinoutpoint_Enabled = 0;
         edtavCtloutn_Enabled = 0;
         edtavCtloutowner_Enabled = 0;
         edtavCtloutaddress_Enabled = 0;
         edtavCtloutvalue_Enabled = 0;
         fix_multi_value_controls( ) ;
      }

      protected void STRUP3O0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E153O2 ();
         context.wbGlbDoneStart = 1;
         /* After Start, stand alone formulas. */
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read saved SDTs. */
            ajax_req_read_hidden_sdt(cgiGet( "Outputs"), AV10outputs);
            ajax_req_read_hidden_sdt(cgiGet( "Inputs"), AV9inputs);
            ajax_req_read_hidden_sdt(cgiGet( "vINPUTS"), AV9inputs);
            ajax_req_read_hidden_sdt(cgiGet( "vOUTPUTS"), AV10outputs);
            /* Read saved values. */
            nRC_GXsfl_25 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_25"), ".", ","), 18, MidpointRounding.ToEven));
            nRC_GXsfl_35 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_35"), ".", ","), 18, MidpointRounding.ToEven));
            INPUTSGRID_nFirstRecordOnPage = (long)(Math.Round(context.localUtil.CToN( cgiGet( "INPUTSGRID_nFirstRecordOnPage"), ".", ","), 18, MidpointRounding.ToEven));
            OUTPUTSGRID_nFirstRecordOnPage = (long)(Math.Round(context.localUtil.CToN( cgiGet( "OUTPUTSGRID_nFirstRecordOnPage"), ".", ","), 18, MidpointRounding.ToEven));
            INPUTSGRID_nEOF = (short)(Math.Round(context.localUtil.CToN( cgiGet( "INPUTSGRID_nEOF"), ".", ","), 18, MidpointRounding.ToEven));
            OUTPUTSGRID_nEOF = (short)(Math.Round(context.localUtil.CToN( cgiGet( "OUTPUTSGRID_nEOF"), ".", ","), 18, MidpointRounding.ToEven));
            nRC_GXsfl_25 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_25"), ".", ","), 18, MidpointRounding.ToEven));
            nGXsfl_25_fel_idx = 0;
            while ( nGXsfl_25_fel_idx < nRC_GXsfl_25 )
            {
               nGXsfl_25_fel_idx = ((subInputsgrid_Islastpage==1)&&(nGXsfl_25_fel_idx+1>subInputsgrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_25_fel_idx+1);
               sGXsfl_25_fel_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_25_fel_idx), 4, 0), 4, "0");
               SubsflControlProps_fel_252( ) ;
               AV16GXV1 = (int)(nGXsfl_25_fel_idx+INPUTSGRID_nFirstRecordOnPage);
               if ( ( AV9inputs.Count >= AV16GXV1 ) && ( AV16GXV1 > 0 ) )
               {
                  AV9inputs.CurrentItem = ((GeneXus.Programs.wallet.SdtTransactionLine)AV9inputs.Item(AV16GXV1));
               }
            }
            if ( nGXsfl_25_fel_idx == 0 )
            {
               nGXsfl_25_idx = 1;
               sGXsfl_25_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_25_idx), 4, 0), 4, "0");
               SubsflControlProps_252( ) ;
            }
            nGXsfl_25_fel_idx = 1;
            nRC_GXsfl_35 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_35"), ".", ","), 18, MidpointRounding.ToEven));
            nGXsfl_35_fel_idx = 0;
            while ( nGXsfl_35_fel_idx < nRC_GXsfl_35 )
            {
               nGXsfl_35_fel_idx = ((subOutputsgrid_Islastpage==1)&&(nGXsfl_35_fel_idx+1>subOutputsgrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_35_fel_idx+1);
               sGXsfl_35_fel_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_35_fel_idx), 4, 0), 4, "0");
               SubsflControlProps_fel_353( ) ;
               AV21GXV6 = (int)(nGXsfl_35_fel_idx+OUTPUTSGRID_nFirstRecordOnPage);
               if ( ( AV10outputs.Count >= AV21GXV6 ) && ( AV21GXV6 > 0 ) )
               {
                  AV10outputs.CurrentItem = ((GeneXus.Programs.wallet.SdtTransactionLine)AV10outputs.Item(AV21GXV6));
               }
            }
            if ( nGXsfl_35_fel_idx == 0 )
            {
               nGXsfl_35_idx = 1;
               sGXsfl_35_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_35_idx), 4, 0), 4, "0");
               SubsflControlProps_353( ) ;
            }
            nGXsfl_35_fel_idx = 1;
            /* Read variables values. */
            AV11shownTransactionId = cgiGet( edtavShowntransactionid_Internalname);
            AssignAttri("", false, "AV11shownTransactionId", AV11shownTransactionId);
            AV6explorerLink = cgiGet( edtavExplorerlink_Internalname);
            AssignAttri("", false, "AV6explorerLink", AV6explorerLink);
            /* Read subfile selected row values. */
            /* Read hidden variables. */
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            /* Check if conditions changed and reset current page numbers */
            /* Check if conditions changed and reset current page numbers */
         }
         else
         {
            dynload_actions( ) ;
         }
      }

      protected void GXStart( )
      {
         /* Execute user event: Start */
         E153O2 ();
         if (returnInSub) return;
      }

      protected void E153O2( )
      {
         /* Start Routine */
         returnInSub = false;
         new GeneXus.Programs.wallet.gettransactiondetail(context ).execute(  AV13transactionId,  AV12transactionFileName, out  AV7header, out  AV9inputs, out  AV10outputs, out  AV5error) ;
         gx_BV25 = true;
         gx_BV35 = true;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV5error)) )
         {
            lblTbheader_Caption = StringUtil.Trim( AV5error);
            AssignProp("", false, lblTbheader_Internalname, "Caption", lblTbheader_Caption, true);
         }
         else
         {
            AV8headerText = StringUtil.Trim( AV7header.gxTpr_Kind) + " on " + StringUtil.Trim( context.localUtil.TToC( AV7header.gxTpr_Datetime, 8, 5, 1, 2, "/", ":", " "));
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV7header.gxTpr_Label)) )
            {
               AV8headerText += " - " + StringUtil.Trim( AV7header.gxTpr_Label);
            }
            if ( ( AV7header.gxTpr_Received > Convert.ToDecimal( 0 )) )
            {
               AV8headerText += ". Received by this wallet: " + StringUtil.Trim( StringUtil.Str( AV7header.gxTpr_Received, 17, 8)) + " BTC";
            }
            else
            {
               AV8headerText += ". Left this wallet: " + StringUtil.Trim( StringUtil.Str( AV7header.gxTpr_Sent, 17, 8)) + " BTC (paid to others + the fee)";
            }
            if ( ( AV7header.gxTpr_Fee > Convert.ToDecimal( 0 )) )
            {
               AV8headerText += ". Fee: " + StringUtil.Trim( StringUtil.Str( AV7header.gxTpr_Fee, 17, 8)) + " BTC";
               if ( ( AV7header.gxTpr_Feerate > Convert.ToDecimal( 0 )) )
               {
                  AV8headerText += " (" + StringUtil.Trim( StringUtil.Str( AV7header.gxTpr_Feerate, 10, 2)) + " sat/vB, " + StringUtil.Trim( StringUtil.Str( (decimal)(AV7header.gxTpr_Virtualsize), 10, 0)) + " vB)";
               }
            }
            AV8headerText += ". Balance after it: " + StringUtil.Trim( StringUtil.Str( AV7header.gxTpr_Balance, 17, 8)) + " BTC. ";
            if ( AV7header.gxTpr_Pending )
            {
               AV8headerText += "Not in a block yet.";
            }
            else
            {
               AV8headerText += "Confirmations: " + StringUtil.Trim( StringUtil.Str( (decimal)(AV7header.gxTpr_Confirmations), 10, 0)) + ".";
            }
            if ( StringUtil.StrCmp(StringUtil.Trim( AV7header.gxTpr_Kind), "Sent, not seen yet") == 0 )
            {
               AV8headerText += " The Electrum server never saw this payment. If it will never reach the network, Forget it: its coins become spendable again.";
               bttForgetpayment_Visible = 1;
               AssignProp("", false, bttForgetpayment_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttForgetpayment_Visible), 5, 0), true);
            }
            else
            {
               bttForgetpayment_Visible = 0;
               AssignProp("", false, bttForgetpayment_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttForgetpayment_Visible), 5, 0), true);
            }
            lblTbheader_Caption = StringUtil.Trim( AV8headerText);
            AssignProp("", false, lblTbheader_Internalname, "Caption", lblTbheader_Caption, true);
            AV11shownTransactionId = StringUtil.Trim( AV13transactionId);
            AssignAttri("", false, "AV11shownTransactionId", AV11shownTransactionId);
            GXt_SdtWalletInfo1 = AV14walletInfo;
            new GeneXus.Programs.wallet.getwalletinfo(context ).execute( out  GXt_SdtWalletInfo1) ;
            AV14walletInfo = GXt_SdtWalletInfo1;
            if ( StringUtil.StrCmp(AV14walletInfo.gxTpr_Networktype, "MainNet") == 0 )
            {
               edtavExplorerlink_Link = "https://blockstream.info/tx/"+StringUtil.Trim( AV13transactionId);
               AssignProp("", false, edtavExplorerlink_Internalname, "Link", edtavExplorerlink_Link, true);
            }
            else
            {
               edtavExplorerlink_Link = "https://blockstream.info/testnet/tx/"+StringUtil.Trim( AV13transactionId);
               AssignProp("", false, edtavExplorerlink_Internalname, "Link", edtavExplorerlink_Link, true);
            }
            AV6explorerLink = "Open in the block explorer";
            AssignAttri("", false, "AV6explorerLink", AV6explorerLink);
            edtavExplorerlink_Linktarget = "_blank";
            AssignProp("", false, edtavExplorerlink_Internalname, "Linktarget", edtavExplorerlink_Linktarget, true);
         }
      }

      protected void E123O2( )
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

      protected void E133O2( )
      {
         /* Extensions\Web\Dialog_Onconfirmclosed Routine */
         returnInSub = false;
         if ( AV15userResponse )
         {
            new GeneXus.Programs.wallet.forgetunseenpayment(context ).execute(  AV12transactionFileName,  AV13transactionId, out  AV5error) ;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV5error)) )
            {
               this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"success",(string)"Payment forgotten","Its coins are spendable again. Update the balance to see them."}, true);
               context.setWebReturnParms(new Object[] {});
               context.setWebReturnParmsMetadata(new Object[] {});
               context.wjLocDisableFrm = 1;
               context.nUserReturn = 1;
               returnInSub = true;
               if (true) return;
            }
            else
            {
               this.executeExternalObjectMethod("", false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"The payment was not forgotten",(string)AV5error}, true);
            }
         }
      }

      private void E163O2( )
      {
         /* Inputsgrid_Load Routine */
         returnInSub = false;
         AV16GXV1 = 1;
         while ( AV16GXV1 <= AV9inputs.Count )
         {
            AV9inputs.CurrentItem = ((GeneXus.Programs.wallet.SdtTransactionLine)AV9inputs.Item(AV16GXV1));
            /* Load Method */
            if ( wbStart != -1 )
            {
               wbStart = 25;
            }
            if ( ( subInputsgrid_Islastpage == 1 ) || ( 100 == 0 ) || ( ( INPUTSGRID_nCurrentRecord >= INPUTSGRID_nFirstRecordOnPage ) && ( INPUTSGRID_nCurrentRecord < INPUTSGRID_nFirstRecordOnPage + subInputsgrid_fnc_Recordsperpage( ) ) ) )
            {
               sendrow_252( ) ;
            }
            INPUTSGRID_nEOF = (short)(((subInputsgrid_Rows==0)||(INPUTSGRID_nCurrentRecord<INPUTSGRID_nFirstRecordOnPage+subInputsgrid_fnc_Recordsperpage( )) ? 1 : 0));
            GxWebStd.gx_hidden_field( context, "INPUTSGRID_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(INPUTSGRID_nEOF), 1, 0, ".", "")));
            INPUTSGRID_nCurrentRecord = (long)(INPUTSGRID_nCurrentRecord+1);
            if ( isFullAjaxMode( ) && ! bGXsfl_25_Refreshing )
            {
               DoAjaxLoad(25, InputsgridRow);
            }
            AV16GXV1 = (int)(AV16GXV1+1);
         }
      }

      private void E143O3( )
      {
         /* Outputsgrid_Load Routine */
         returnInSub = false;
         AV21GXV6 = 1;
         while ( AV21GXV6 <= AV10outputs.Count )
         {
            AV10outputs.CurrentItem = ((GeneXus.Programs.wallet.SdtTransactionLine)AV10outputs.Item(AV21GXV6));
            /* Load Method */
            if ( wbStart != -1 )
            {
               wbStart = 35;
            }
            if ( ( subOutputsgrid_Islastpage == 1 ) || ( 100 == 0 ) || ( ( OUTPUTSGRID_nCurrentRecord >= OUTPUTSGRID_nFirstRecordOnPage ) && ( OUTPUTSGRID_nCurrentRecord < OUTPUTSGRID_nFirstRecordOnPage + subOutputsgrid_fnc_Recordsperpage( ) ) ) )
            {
               sendrow_353( ) ;
            }
            OUTPUTSGRID_nEOF = (short)(((subOutputsgrid_Rows==0)||(OUTPUTSGRID_nCurrentRecord<OUTPUTSGRID_nFirstRecordOnPage+subOutputsgrid_fnc_Recordsperpage( )) ? 1 : 0));
            GxWebStd.gx_hidden_field( context, "OUTPUTSGRID_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(OUTPUTSGRID_nEOF), 1, 0, ".", "")));
            OUTPUTSGRID_nCurrentRecord = (long)(OUTPUTSGRID_nCurrentRecord+1);
            if ( isFullAjaxMode( ) && ! bGXsfl_35_Refreshing )
            {
               DoAjaxLoad(35, OutputsgridRow);
            }
            AV21GXV6 = (int)(AV21GXV6+1);
         }
      }

      public override void setparameters( Object[] obj )
      {
         createObjects();
         initialize();
         AV13transactionId = (string)getParm(obj,0);
         AssignAttri("", false, "AV13transactionId", AV13transactionId);
         GxWebStd.gx_hidden_field( context, "gxhash_vTRANSACTIONID", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV13transactionId, "")), context));
         AV12transactionFileName = (string)getParm(obj,1);
         AssignAttri("", false, "AV12transactionFileName", AV12transactionFileName);
         GxWebStd.gx_hidden_field( context, "gxhash_vTRANSACTIONFILENAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV12transactionFileName, "")), context));
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
         PA3O2( ) ;
         WS3O2( ) ;
         WE3O2( ) ;
         cleanup();
         context.SetWrapped(false);
         context.GX_msglist = BackMsgLst;
         return "";
      }

      public void responsestatic( string sGXDynURL )
      {
      }

      protected void define_styles( )
      {
         AddThemeStyleSheetFile("", context.GetTheme( )+".css", "?"+GetCacheInvalidationToken( ));
         bool outputEnabled = isOutputEnabled( );
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         idxLst = 1;
         while ( idxLst <= Form.Jscriptsrc.Count )
         {
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?2026103013274511", true, true, false);
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
         context.AddJavascriptSource("messages.eng.js", "?"+GetCacheInvalidationToken( ), false, true, false);
         context.AddJavascriptSource("wallet/showwallettransaction.js", "?2026103013274511", false, true, false);
         context.AddJavascriptSource("web-extension/gx-web-extensions.js", "", false, true, false);
         /* End function include_jscripts */
      }

      protected void SubsflControlProps_252( )
      {
         edtavCtlinowner_Internalname = "CTLINOWNER_"+sGXsfl_25_idx;
         edtavCtlinaddress_Internalname = "CTLINADDRESS_"+sGXsfl_25_idx;
         edtavCtlinvalue_Internalname = "CTLINVALUE_"+sGXsfl_25_idx;
         edtavCtlinoutpoint_Internalname = "CTLINOUTPOINT_"+sGXsfl_25_idx;
      }

      protected void SubsflControlProps_fel_252( )
      {
         edtavCtlinowner_Internalname = "CTLINOWNER_"+sGXsfl_25_fel_idx;
         edtavCtlinaddress_Internalname = "CTLINADDRESS_"+sGXsfl_25_fel_idx;
         edtavCtlinvalue_Internalname = "CTLINVALUE_"+sGXsfl_25_fel_idx;
         edtavCtlinoutpoint_Internalname = "CTLINOUTPOINT_"+sGXsfl_25_fel_idx;
      }

      protected void sendrow_252( )
      {
         sGXsfl_25_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_25_idx), 4, 0), 4, "0");
         SubsflControlProps_252( ) ;
         WB3O0( ) ;
         if ( ( 100 * 1 == 0 ) || ( nGXsfl_25_idx <= subInputsgrid_fnc_Recordsperpage( ) * 1 ) )
         {
            InputsgridRow = GXWebRow.GetNew(context,InputsgridContainer);
            if ( subInputsgrid_Backcolorstyle == 0 )
            {
               /* None style subfile background logic. */
               subInputsgrid_Backstyle = 0;
               if ( StringUtil.StrCmp(subInputsgrid_Class, "") != 0 )
               {
                  subInputsgrid_Linesclass = subInputsgrid_Class+"Odd";
               }
            }
            else if ( subInputsgrid_Backcolorstyle == 1 )
            {
               /* Uniform style subfile background logic. */
               subInputsgrid_Backstyle = 0;
               subInputsgrid_Backcolor = subInputsgrid_Allbackcolor;
               if ( StringUtil.StrCmp(subInputsgrid_Class, "") != 0 )
               {
                  subInputsgrid_Linesclass = subInputsgrid_Class+"Uniform";
               }
            }
            else if ( subInputsgrid_Backcolorstyle == 2 )
            {
               /* Header style subfile background logic. */
               subInputsgrid_Backstyle = 1;
               if ( StringUtil.StrCmp(subInputsgrid_Class, "") != 0 )
               {
                  subInputsgrid_Linesclass = subInputsgrid_Class+"Odd";
               }
               subInputsgrid_Backcolor = (int)(0x0);
            }
            else if ( subInputsgrid_Backcolorstyle == 3 )
            {
               /* Report style subfile background logic. */
               subInputsgrid_Backstyle = 1;
               if ( ((int)((nGXsfl_25_idx) % (2))) == 0 )
               {
                  subInputsgrid_Backcolor = (int)(0x0);
                  if ( StringUtil.StrCmp(subInputsgrid_Class, "") != 0 )
                  {
                     subInputsgrid_Linesclass = subInputsgrid_Class+"Even";
                  }
               }
               else
               {
                  subInputsgrid_Backcolor = (int)(0x0);
                  if ( StringUtil.StrCmp(subInputsgrid_Class, "") != 0 )
                  {
                     subInputsgrid_Linesclass = subInputsgrid_Class+"Odd";
                  }
               }
            }
            if ( InputsgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<tr ") ;
               context.WriteHtmlText( " class=\""+"Grid"+"\" style=\""+""+"\"") ;
               context.WriteHtmlText( " gxrow=\""+sGXsfl_25_idx+"\">") ;
            }
            /* Subfile cell */
            if ( InputsgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 26,'',false,'" + sGXsfl_25_idx + "',25)\"";
            ROClassString = "Attribute";
            InputsgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlinowner_Internalname,StringUtil.RTrim( ((GeneXus.Programs.wallet.SdtTransactionLine)AV9inputs.Item(AV16GXV1)).gxTpr_Owner),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,26);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlinowner_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlinowner_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)20,(short)0,(short)0,(short)25,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( InputsgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 27,'',false,'" + sGXsfl_25_idx + "',25)\"";
            ROClassString = "Attribute";
            InputsgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlinaddress_Internalname,StringUtil.RTrim( ((GeneXus.Programs.wallet.SdtTransactionLine)AV9inputs.Item(AV16GXV1)).gxTpr_Address),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,27);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlinaddress_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlinaddress_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)250,(short)0,(short)0,(short)25,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( InputsgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 28,'',false,'" + sGXsfl_25_idx + "',25)\"";
            ROClassString = "Attribute";
            InputsgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlinvalue_Internalname,StringUtil.RTrim( ((GeneXus.Programs.wallet.SdtTransactionLine)AV9inputs.Item(AV16GXV1)).gxTpr_Valuetext),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,28);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlinvalue_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlinvalue_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)20,(short)0,(short)0,(short)25,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( InputsgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 29,'',false,'" + sGXsfl_25_idx + "',25)\"";
            ROClassString = "Attribute";
            InputsgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlinoutpoint_Internalname,((GeneXus.Programs.wallet.SdtTransactionLine)AV9inputs.Item(AV16GXV1)).gxTpr_Outpoint,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,29);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlinoutpoint_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlinoutpoint_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)100,(short)0,(short)0,(short)25,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            send_integrity_lvl_hashes3O2( ) ;
            InputsgridContainer.AddRow(InputsgridRow);
            nGXsfl_25_idx = ((subInputsgrid_Islastpage==1)&&(nGXsfl_25_idx+1>subInputsgrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_25_idx+1);
            sGXsfl_25_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_25_idx), 4, 0), 4, "0");
            SubsflControlProps_252( ) ;
         }
         /* End function sendrow_252 */
      }

      protected void SubsflControlProps_353( )
      {
         edtavCtloutn_Internalname = "CTLOUTN_"+sGXsfl_35_idx;
         edtavCtloutowner_Internalname = "CTLOUTOWNER_"+sGXsfl_35_idx;
         edtavCtloutaddress_Internalname = "CTLOUTADDRESS_"+sGXsfl_35_idx;
         edtavCtloutvalue_Internalname = "CTLOUTVALUE_"+sGXsfl_35_idx;
      }

      protected void SubsflControlProps_fel_353( )
      {
         edtavCtloutn_Internalname = "CTLOUTN_"+sGXsfl_35_fel_idx;
         edtavCtloutowner_Internalname = "CTLOUTOWNER_"+sGXsfl_35_fel_idx;
         edtavCtloutaddress_Internalname = "CTLOUTADDRESS_"+sGXsfl_35_fel_idx;
         edtavCtloutvalue_Internalname = "CTLOUTVALUE_"+sGXsfl_35_fel_idx;
      }

      protected void sendrow_353( )
      {
         sGXsfl_35_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_35_idx), 4, 0), 4, "0");
         SubsflControlProps_353( ) ;
         WB3O0( ) ;
         if ( ( 100 * 1 == 0 ) || ( nGXsfl_35_idx <= subOutputsgrid_fnc_Recordsperpage( ) * 1 ) )
         {
            OutputsgridRow = GXWebRow.GetNew(context,OutputsgridContainer);
            if ( subOutputsgrid_Backcolorstyle == 0 )
            {
               /* None style subfile background logic. */
               subOutputsgrid_Backstyle = 0;
               if ( StringUtil.StrCmp(subOutputsgrid_Class, "") != 0 )
               {
                  subOutputsgrid_Linesclass = subOutputsgrid_Class+"Odd";
               }
            }
            else if ( subOutputsgrid_Backcolorstyle == 1 )
            {
               /* Uniform style subfile background logic. */
               subOutputsgrid_Backstyle = 0;
               subOutputsgrid_Backcolor = subOutputsgrid_Allbackcolor;
               if ( StringUtil.StrCmp(subOutputsgrid_Class, "") != 0 )
               {
                  subOutputsgrid_Linesclass = subOutputsgrid_Class+"Uniform";
               }
            }
            else if ( subOutputsgrid_Backcolorstyle == 2 )
            {
               /* Header style subfile background logic. */
               subOutputsgrid_Backstyle = 1;
               if ( StringUtil.StrCmp(subOutputsgrid_Class, "") != 0 )
               {
                  subOutputsgrid_Linesclass = subOutputsgrid_Class+"Odd";
               }
               subOutputsgrid_Backcolor = (int)(0x0);
            }
            else if ( subOutputsgrid_Backcolorstyle == 3 )
            {
               /* Report style subfile background logic. */
               subOutputsgrid_Backstyle = 1;
               if ( ((int)((nGXsfl_35_idx) % (2))) == 0 )
               {
                  subOutputsgrid_Backcolor = (int)(0x0);
                  if ( StringUtil.StrCmp(subOutputsgrid_Class, "") != 0 )
                  {
                     subOutputsgrid_Linesclass = subOutputsgrid_Class+"Even";
                  }
               }
               else
               {
                  subOutputsgrid_Backcolor = (int)(0x0);
                  if ( StringUtil.StrCmp(subOutputsgrid_Class, "") != 0 )
                  {
                     subOutputsgrid_Linesclass = subOutputsgrid_Class+"Odd";
                  }
               }
            }
            if ( OutputsgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<tr ") ;
               context.WriteHtmlText( " class=\""+"Grid"+"\" style=\""+""+"\"") ;
               context.WriteHtmlText( " gxrow=\""+sGXsfl_35_idx+"\">") ;
            }
            /* Subfile cell */
            if ( OutputsgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 36,'',false,'" + sGXsfl_35_idx + "',35)\"";
            ROClassString = "Attribute";
            OutputsgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtloutn_Internalname,StringUtil.LTrim( StringUtil.NToC( (decimal)(((GeneXus.Programs.wallet.SdtTransactionLine)AV10outputs.Item(AV21GXV6)).gxTpr_N), 10, 0, ".", "")),StringUtil.LTrim( ((edtavCtloutn_Enabled!=0) ? context.localUtil.Format( (decimal)(((GeneXus.Programs.wallet.SdtTransactionLine)AV10outputs.Item(AV21GXV6)).gxTpr_N), "ZZZZZZZZZ9") : context.localUtil.Format( (decimal)(((GeneXus.Programs.wallet.SdtTransactionLine)AV10outputs.Item(AV21GXV6)).gxTpr_N), "ZZZZZZZZZ9")))," dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,',');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,',');"+";gx.evt.onblur(this,36);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtloutn_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtloutn_Enabled,(short)0,(string)"text",(string)"1",(short)0,(string)"px",(short)17,(string)"px",(short)10,(short)0,(short)0,(short)35,(short)0,(short)-1,(short)0,(bool)false,(string)"",(string)"end",(bool)false,(string)""});
            /* Subfile cell */
            if ( OutputsgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 37,'',false,'" + sGXsfl_35_idx + "',35)\"";
            ROClassString = "Attribute";
            OutputsgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtloutowner_Internalname,StringUtil.RTrim( ((GeneXus.Programs.wallet.SdtTransactionLine)AV10outputs.Item(AV21GXV6)).gxTpr_Owner),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,37);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtloutowner_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtloutowner_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)20,(short)0,(short)0,(short)35,(short)0,(short)-1,(short)-1,(bool)false,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( OutputsgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 38,'',false,'" + sGXsfl_35_idx + "',35)\"";
            ROClassString = "Attribute";
            OutputsgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtloutaddress_Internalname,StringUtil.RTrim( ((GeneXus.Programs.wallet.SdtTransactionLine)AV10outputs.Item(AV21GXV6)).gxTpr_Address),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,38);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtloutaddress_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtloutaddress_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)250,(short)0,(short)0,(short)35,(short)0,(short)-1,(short)-1,(bool)false,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( OutputsgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 39,'',false,'" + sGXsfl_35_idx + "',35)\"";
            ROClassString = "Attribute";
            OutputsgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtloutvalue_Internalname,StringUtil.RTrim( ((GeneXus.Programs.wallet.SdtTransactionLine)AV10outputs.Item(AV21GXV6)).gxTpr_Valuetext),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,39);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtloutvalue_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtloutvalue_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)20,(short)0,(short)0,(short)35,(short)0,(short)-1,(short)-1,(bool)false,(string)"",(string)"start",(bool)true,(string)""});
            send_integrity_lvl_hashes3O3( ) ;
            OutputsgridContainer.AddRow(OutputsgridRow);
            nGXsfl_35_idx = ((subOutputsgrid_Islastpage==1)&&(nGXsfl_35_idx+1>subOutputsgrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_35_idx+1);
            sGXsfl_35_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_35_idx), 4, 0), 4, "0");
            SubsflControlProps_353( ) ;
         }
         /* End function sendrow_353 */
      }

      protected void init_web_controls( )
      {
         /* End function init_web_controls */
      }

      protected void StartGridControl25( )
      {
         if ( InputsgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+"InputsgridContainer"+"DivS\" data-gxgridid=\"25\">") ;
            sStyleString = "";
            GxWebStd.gx_table_start( context, subInputsgrid_Internalname, subInputsgrid_Internalname, "", "Grid", 0, "", "", 1, 2, sStyleString, "", "", 0);
            /* Subfile titles */
            context.WriteHtmlText( "<tr") ;
            context.WriteHtmlTextNl( ">") ;
            if ( subInputsgrid_Backcolorstyle == 0 )
            {
               subInputsgrid_Titlebackstyle = 0;
               if ( StringUtil.Len( subInputsgrid_Class) > 0 )
               {
                  subInputsgrid_Linesclass = subInputsgrid_Class+"Title";
               }
            }
            else
            {
               subInputsgrid_Titlebackstyle = 1;
               if ( subInputsgrid_Backcolorstyle == 1 )
               {
                  subInputsgrid_Titlebackcolor = subInputsgrid_Allbackcolor;
                  if ( StringUtil.Len( subInputsgrid_Class) > 0 )
                  {
                     subInputsgrid_Linesclass = subInputsgrid_Class+"UniformTitle";
                  }
               }
               else
               {
                  if ( StringUtil.Len( subInputsgrid_Class) > 0 )
                  {
                     subInputsgrid_Linesclass = subInputsgrid_Class+"Title";
                  }
               }
            }
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Owner") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Address") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Value") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Coin (txid:n)") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlTextNl( "</tr>") ;
            InputsgridContainer.AddObjectProperty("GridName", "Inputsgrid");
         }
         else
         {
            InputsgridContainer.AddObjectProperty("GridName", "Inputsgrid");
            InputsgridContainer.AddObjectProperty("Header", subInputsgrid_Header);
            InputsgridContainer.AddObjectProperty("Class", "Grid");
            InputsgridContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
            InputsgridContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
            InputsgridContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subInputsgrid_Backcolorstyle), 1, 0, ".", "")));
            InputsgridContainer.AddObjectProperty("CmpContext", "");
            InputsgridContainer.AddObjectProperty("InMasterPage", "false");
            InputsgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            InputsgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlinowner_Enabled), 5, 0, ".", "")));
            InputsgridContainer.AddColumnProperties(InputsgridColumn);
            InputsgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            InputsgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlinaddress_Enabled), 5, 0, ".", "")));
            InputsgridContainer.AddColumnProperties(InputsgridColumn);
            InputsgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            InputsgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlinvalue_Enabled), 5, 0, ".", "")));
            InputsgridContainer.AddColumnProperties(InputsgridColumn);
            InputsgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            InputsgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlinoutpoint_Enabled), 5, 0, ".", "")));
            InputsgridContainer.AddColumnProperties(InputsgridColumn);
            InputsgridContainer.AddObjectProperty("Selectedindex", StringUtil.LTrim( StringUtil.NToC( (decimal)(subInputsgrid_Selectedindex), 4, 0, ".", "")));
            InputsgridContainer.AddObjectProperty("Allowselection", StringUtil.LTrim( StringUtil.NToC( (decimal)(subInputsgrid_Allowselection), 1, 0, ".", "")));
            InputsgridContainer.AddObjectProperty("Selectioncolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subInputsgrid_Selectioncolor), 9, 0, ".", "")));
            InputsgridContainer.AddObjectProperty("Allowhover", StringUtil.LTrim( StringUtil.NToC( (decimal)(subInputsgrid_Allowhovering), 1, 0, ".", "")));
            InputsgridContainer.AddObjectProperty("Hovercolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subInputsgrid_Hoveringcolor), 9, 0, ".", "")));
            InputsgridContainer.AddObjectProperty("Allowcollapsing", StringUtil.LTrim( StringUtil.NToC( (decimal)(subInputsgrid_Allowcollapsing), 1, 0, ".", "")));
            InputsgridContainer.AddObjectProperty("Collapsed", StringUtil.LTrim( StringUtil.NToC( (decimal)(subInputsgrid_Collapsed), 1, 0, ".", "")));
         }
      }

      protected void StartGridControl35( )
      {
         if ( OutputsgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+"OutputsgridContainer"+"DivS\" data-gxgridid=\"35\">") ;
            sStyleString = "";
            GxWebStd.gx_table_start( context, subOutputsgrid_Internalname, subOutputsgrid_Internalname, "", "Grid", 0, "", "", 1, 2, sStyleString, "", "", 0);
            /* Subfile titles */
            context.WriteHtmlText( "<tr") ;
            context.WriteHtmlTextNl( ">") ;
            if ( subOutputsgrid_Backcolorstyle == 0 )
            {
               subOutputsgrid_Titlebackstyle = 0;
               if ( StringUtil.Len( subOutputsgrid_Class) > 0 )
               {
                  subOutputsgrid_Linesclass = subOutputsgrid_Class+"Title";
               }
            }
            else
            {
               subOutputsgrid_Titlebackstyle = 1;
               if ( subOutputsgrid_Backcolorstyle == 1 )
               {
                  subOutputsgrid_Titlebackcolor = subOutputsgrid_Allbackcolor;
                  if ( StringUtil.Len( subOutputsgrid_Class) > 0 )
                  {
                     subOutputsgrid_Linesclass = subOutputsgrid_Class+"UniformTitle";
                  }
               }
               else
               {
                  if ( StringUtil.Len( subOutputsgrid_Class) > 0 )
                  {
                     subOutputsgrid_Linesclass = subOutputsgrid_Class+"Title";
                  }
               }
            }
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "n") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Owner") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Address") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Value") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlTextNl( "</tr>") ;
            OutputsgridContainer.AddObjectProperty("GridName", "Outputsgrid");
         }
         else
         {
            OutputsgridContainer.AddObjectProperty("GridName", "Outputsgrid");
            OutputsgridContainer.AddObjectProperty("Header", subOutputsgrid_Header);
            OutputsgridContainer.AddObjectProperty("Class", "Grid");
            OutputsgridContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
            OutputsgridContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
            OutputsgridContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subOutputsgrid_Backcolorstyle), 1, 0, ".", "")));
            OutputsgridContainer.AddObjectProperty("CmpContext", "");
            OutputsgridContainer.AddObjectProperty("InMasterPage", "false");
            OutputsgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            OutputsgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtloutn_Enabled), 5, 0, ".", "")));
            OutputsgridContainer.AddColumnProperties(OutputsgridColumn);
            OutputsgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            OutputsgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtloutowner_Enabled), 5, 0, ".", "")));
            OutputsgridContainer.AddColumnProperties(OutputsgridColumn);
            OutputsgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            OutputsgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtloutaddress_Enabled), 5, 0, ".", "")));
            OutputsgridContainer.AddColumnProperties(OutputsgridColumn);
            OutputsgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            OutputsgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtloutvalue_Enabled), 5, 0, ".", "")));
            OutputsgridContainer.AddColumnProperties(OutputsgridColumn);
            OutputsgridContainer.AddObjectProperty("Selectedindex", StringUtil.LTrim( StringUtil.NToC( (decimal)(subOutputsgrid_Selectedindex), 4, 0, ".", "")));
            OutputsgridContainer.AddObjectProperty("Allowselection", StringUtil.LTrim( StringUtil.NToC( (decimal)(subOutputsgrid_Allowselection), 1, 0, ".", "")));
            OutputsgridContainer.AddObjectProperty("Selectioncolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subOutputsgrid_Selectioncolor), 9, 0, ".", "")));
            OutputsgridContainer.AddObjectProperty("Allowhover", StringUtil.LTrim( StringUtil.NToC( (decimal)(subOutputsgrid_Allowhovering), 1, 0, ".", "")));
            OutputsgridContainer.AddObjectProperty("Hovercolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subOutputsgrid_Hoveringcolor), 9, 0, ".", "")));
            OutputsgridContainer.AddObjectProperty("Allowcollapsing", StringUtil.LTrim( StringUtil.NToC( (decimal)(subOutputsgrid_Allowcollapsing), 1, 0, ".", "")));
            OutputsgridContainer.AddObjectProperty("Collapsed", StringUtil.LTrim( StringUtil.NToC( (decimal)(subOutputsgrid_Collapsed), 1, 0, ".", "")));
         }
      }

      protected void init_default_properties( )
      {
         lblTbheader_Internalname = "TBHEADER";
         bttForgetpayment_Internalname = "FORGETPAYMENT";
         bttClose_Internalname = "CLOSE";
         edtavShowntransactionid_Internalname = "vSHOWNTRANSACTIONID";
         edtavExplorerlink_Internalname = "vEXPLORERLINK";
         lblTbinputs_Internalname = "TBINPUTS";
         edtavCtlinowner_Internalname = "CTLINOWNER";
         edtavCtlinaddress_Internalname = "CTLINADDRESS";
         edtavCtlinvalue_Internalname = "CTLINVALUE";
         edtavCtlinoutpoint_Internalname = "CTLINOUTPOINT";
         lblTboutputs_Internalname = "TBOUTPUTS";
         edtavCtloutn_Internalname = "CTLOUTN";
         edtavCtloutowner_Internalname = "CTLOUTOWNER";
         edtavCtloutaddress_Internalname = "CTLOUTADDRESS";
         edtavCtloutvalue_Internalname = "CTLOUTVALUE";
         divMaintable_Internalname = "MAINTABLE";
         Form.Internalname = "FORM";
         subInputsgrid_Internalname = "INPUTSGRID";
         subOutputsgrid_Internalname = "OUTPUTSGRID";
      }

      public override void initialize_properties( )
      {
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
         if ( context.isSpaRequest( ) )
         {
            disableJsOutput();
         }
         init_default_properties( ) ;
         subOutputsgrid_Allowcollapsing = 0;
         subOutputsgrid_Allowselection = 0;
         subOutputsgrid_Header = "";
         subInputsgrid_Allowcollapsing = 0;
         subInputsgrid_Allowselection = 0;
         subInputsgrid_Header = "";
         edtavCtloutvalue_Jsonclick = "";
         edtavCtloutvalue_Enabled = 0;
         edtavCtloutaddress_Jsonclick = "";
         edtavCtloutaddress_Enabled = 0;
         edtavCtloutowner_Jsonclick = "";
         edtavCtloutowner_Enabled = 0;
         edtavCtloutn_Jsonclick = "";
         edtavCtloutn_Enabled = 0;
         subOutputsgrid_Class = "Grid";
         subOutputsgrid_Backcolorstyle = 0;
         edtavCtlinoutpoint_Jsonclick = "";
         edtavCtlinoutpoint_Enabled = 0;
         edtavCtlinvalue_Jsonclick = "";
         edtavCtlinvalue_Enabled = 0;
         edtavCtlinaddress_Jsonclick = "";
         edtavCtlinaddress_Enabled = 0;
         edtavCtlinowner_Jsonclick = "";
         edtavCtlinowner_Enabled = 0;
         subInputsgrid_Class = "Grid";
         subInputsgrid_Backcolorstyle = 0;
         edtavExplorerlink_Jsonclick = "";
         edtavExplorerlink_Linktarget = "";
         edtavExplorerlink_Link = "";
         edtavExplorerlink_Enabled = 1;
         edtavShowntransactionid_Jsonclick = "";
         edtavShowntransactionid_Enabled = 1;
         bttForgetpayment_Visible = 1;
         lblTbheader_Caption = "Transaction";
         edtavCtloutvalue_Enabled = -1;
         edtavCtloutaddress_Enabled = -1;
         edtavCtloutowner_Enabled = -1;
         edtavCtloutn_Enabled = -1;
         edtavCtlinoutpoint_Enabled = -1;
         edtavCtlinvalue_Enabled = -1;
         edtavCtlinaddress_Enabled = -1;
         edtavCtlinowner_Enabled = -1;
         subOutputsgrid_Rows = 100;
         subInputsgrid_Rows = 100;
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"INPUTSGRID_nFirstRecordOnPage","type":"int"},{"av":"INPUTSGRID_nEOF","type":"int"},{"av":"AV9inputs","fld":"vINPUTS","grid":25,"type":""},{"av":"nGXsfl_25_idx","ctrl":"GRID","prop":"GridCurrRow","grid":25},{"av":"nRC_GXsfl_25","ctrl":"INPUTSGRID","prop":"GridRC","grid":25,"type":"int"},{"av":"OUTPUTSGRID_nFirstRecordOnPage","type":"int"},{"av":"OUTPUTSGRID_nEOF","type":"int"},{"av":"AV10outputs","fld":"vOUTPUTS","grid":35,"type":""},{"av":"nGXsfl_35_idx","ctrl":"GRID","prop":"GridCurrRow","grid":35},{"av":"nRC_GXsfl_35","ctrl":"OUTPUTSGRID","prop":"GridRC","grid":35,"type":"int"},{"av":"subInputsgrid_Rows","ctrl":"INPUTSGRID","prop":"Rows"},{"av":"subOutputsgrid_Rows","ctrl":"OUTPUTSGRID","prop":"Rows"},{"av":"AV12transactionFileName","fld":"vTRANSACTIONFILENAME","hsh":true,"type":"char"},{"av":"AV13transactionId","fld":"vTRANSACTIONID","hsh":true,"type":"char"}]}""");
         setEventMetadata("'CLOSE'","""{"handler":"E123O2","iparms":[]}""");
         setEventMetadata("'FORGET PAYMENT'","""{"handler":"E113O1","iparms":[]}""");
         setEventMetadata("GX.EXTENSIONS.WEB.DIALOGS.ONCONFIRMCLOSED","""{"handler":"E133O2","iparms":[{"av":"AV15userResponse","fld":"vUSERRESPONSE","type":"boolean"},{"av":"AV12transactionFileName","fld":"vTRANSACTIONFILENAME","hsh":true,"type":"char"},{"av":"AV13transactionId","fld":"vTRANSACTIONID","hsh":true,"type":"char"}]}""");
         setEventMetadata("INPUTSGRID_FIRSTPAGE","""{"handler":"subinputsgrid_firstpage","iparms":[{"av":"INPUTSGRID_nFirstRecordOnPage","type":"int"},{"av":"INPUTSGRID_nEOF","type":"int"},{"av":"AV9inputs","fld":"vINPUTS","grid":25,"type":""},{"av":"nGXsfl_25_idx","ctrl":"GRID","prop":"GridCurrRow","grid":25},{"av":"nRC_GXsfl_25","ctrl":"INPUTSGRID","prop":"GridRC","grid":25,"type":"int"},{"av":"subInputsgrid_Rows","ctrl":"INPUTSGRID","prop":"Rows"},{"av":"subOutputsgrid_Rows","ctrl":"OUTPUTSGRID","prop":"Rows"},{"av":"AV12transactionFileName","fld":"vTRANSACTIONFILENAME","hsh":true,"type":"char"},{"av":"AV13transactionId","fld":"vTRANSACTIONID","hsh":true,"type":"char"},{"av":"AV10outputs","fld":"vOUTPUTS","grid":35,"type":""},{"av":"nGXsfl_35_idx","ctrl":"GRID","prop":"GridCurrRow","grid":35},{"av":"OUTPUTSGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_35","ctrl":"OUTPUTSGRID","prop":"GridRC","grid":35,"type":"int"}]}""");
         setEventMetadata("INPUTSGRID_PREVPAGE","""{"handler":"subinputsgrid_previouspage","iparms":[{"av":"INPUTSGRID_nFirstRecordOnPage","type":"int"},{"av":"INPUTSGRID_nEOF","type":"int"},{"av":"AV9inputs","fld":"vINPUTS","grid":25,"type":""},{"av":"nGXsfl_25_idx","ctrl":"GRID","prop":"GridCurrRow","grid":25},{"av":"nRC_GXsfl_25","ctrl":"INPUTSGRID","prop":"GridRC","grid":25,"type":"int"},{"av":"subInputsgrid_Rows","ctrl":"INPUTSGRID","prop":"Rows"},{"av":"subOutputsgrid_Rows","ctrl":"OUTPUTSGRID","prop":"Rows"},{"av":"AV12transactionFileName","fld":"vTRANSACTIONFILENAME","hsh":true,"type":"char"},{"av":"AV13transactionId","fld":"vTRANSACTIONID","hsh":true,"type":"char"},{"av":"AV10outputs","fld":"vOUTPUTS","grid":35,"type":""},{"av":"nGXsfl_35_idx","ctrl":"GRID","prop":"GridCurrRow","grid":35},{"av":"OUTPUTSGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_35","ctrl":"OUTPUTSGRID","prop":"GridRC","grid":35,"type":"int"}]}""");
         setEventMetadata("INPUTSGRID_NEXTPAGE","""{"handler":"subinputsgrid_nextpage","iparms":[{"av":"INPUTSGRID_nFirstRecordOnPage","type":"int"},{"av":"INPUTSGRID_nEOF","type":"int"},{"av":"AV9inputs","fld":"vINPUTS","grid":25,"type":""},{"av":"nGXsfl_25_idx","ctrl":"GRID","prop":"GridCurrRow","grid":25},{"av":"nRC_GXsfl_25","ctrl":"INPUTSGRID","prop":"GridRC","grid":25,"type":"int"},{"av":"subInputsgrid_Rows","ctrl":"INPUTSGRID","prop":"Rows"},{"av":"subOutputsgrid_Rows","ctrl":"OUTPUTSGRID","prop":"Rows"},{"av":"AV12transactionFileName","fld":"vTRANSACTIONFILENAME","hsh":true,"type":"char"},{"av":"AV13transactionId","fld":"vTRANSACTIONID","hsh":true,"type":"char"},{"av":"AV10outputs","fld":"vOUTPUTS","grid":35,"type":""},{"av":"nGXsfl_35_idx","ctrl":"GRID","prop":"GridCurrRow","grid":35},{"av":"OUTPUTSGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_35","ctrl":"OUTPUTSGRID","prop":"GridRC","grid":35,"type":"int"}]}""");
         setEventMetadata("INPUTSGRID_LASTPAGE","""{"handler":"subinputsgrid_lastpage","iparms":[{"av":"INPUTSGRID_nFirstRecordOnPage","type":"int"},{"av":"INPUTSGRID_nEOF","type":"int"},{"av":"AV9inputs","fld":"vINPUTS","grid":25,"type":""},{"av":"nGXsfl_25_idx","ctrl":"GRID","prop":"GridCurrRow","grid":25},{"av":"nRC_GXsfl_25","ctrl":"INPUTSGRID","prop":"GridRC","grid":25,"type":"int"},{"av":"subInputsgrid_Rows","ctrl":"INPUTSGRID","prop":"Rows"},{"av":"subOutputsgrid_Rows","ctrl":"OUTPUTSGRID","prop":"Rows"},{"av":"AV12transactionFileName","fld":"vTRANSACTIONFILENAME","hsh":true,"type":"char"},{"av":"AV13transactionId","fld":"vTRANSACTIONID","hsh":true,"type":"char"},{"av":"AV10outputs","fld":"vOUTPUTS","grid":35,"type":""},{"av":"nGXsfl_35_idx","ctrl":"GRID","prop":"GridCurrRow","grid":35},{"av":"OUTPUTSGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_35","ctrl":"OUTPUTSGRID","prop":"GridRC","grid":35,"type":"int"}]}""");
         setEventMetadata("OUTPUTSGRID_FIRSTPAGE","""{"handler":"suboutputsgrid_firstpage","iparms":[{"av":"OUTPUTSGRID_nFirstRecordOnPage","type":"int"},{"av":"OUTPUTSGRID_nEOF","type":"int"},{"av":"AV10outputs","fld":"vOUTPUTS","grid":35,"type":""},{"av":"nGXsfl_35_idx","ctrl":"GRID","prop":"GridCurrRow","grid":35},{"av":"nRC_GXsfl_35","ctrl":"OUTPUTSGRID","prop":"GridRC","grid":35,"type":"int"},{"av":"subInputsgrid_Rows","ctrl":"INPUTSGRID","prop":"Rows"},{"av":"subOutputsgrid_Rows","ctrl":"OUTPUTSGRID","prop":"Rows"},{"av":"AV12transactionFileName","fld":"vTRANSACTIONFILENAME","hsh":true,"type":"char"},{"av":"AV13transactionId","fld":"vTRANSACTIONID","hsh":true,"type":"char"},{"av":"AV9inputs","fld":"vINPUTS","grid":25,"type":""},{"av":"nGXsfl_25_idx","ctrl":"GRID","prop":"GridCurrRow","grid":25},{"av":"INPUTSGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_25","ctrl":"INPUTSGRID","prop":"GridRC","grid":25,"type":"int"}]}""");
         setEventMetadata("OUTPUTSGRID_PREVPAGE","""{"handler":"suboutputsgrid_previouspage","iparms":[{"av":"OUTPUTSGRID_nFirstRecordOnPage","type":"int"},{"av":"OUTPUTSGRID_nEOF","type":"int"},{"av":"AV10outputs","fld":"vOUTPUTS","grid":35,"type":""},{"av":"nGXsfl_35_idx","ctrl":"GRID","prop":"GridCurrRow","grid":35},{"av":"nRC_GXsfl_35","ctrl":"OUTPUTSGRID","prop":"GridRC","grid":35,"type":"int"},{"av":"subInputsgrid_Rows","ctrl":"INPUTSGRID","prop":"Rows"},{"av":"subOutputsgrid_Rows","ctrl":"OUTPUTSGRID","prop":"Rows"},{"av":"AV12transactionFileName","fld":"vTRANSACTIONFILENAME","hsh":true,"type":"char"},{"av":"AV13transactionId","fld":"vTRANSACTIONID","hsh":true,"type":"char"},{"av":"AV9inputs","fld":"vINPUTS","grid":25,"type":""},{"av":"nGXsfl_25_idx","ctrl":"GRID","prop":"GridCurrRow","grid":25},{"av":"INPUTSGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_25","ctrl":"INPUTSGRID","prop":"GridRC","grid":25,"type":"int"}]}""");
         setEventMetadata("OUTPUTSGRID_NEXTPAGE","""{"handler":"suboutputsgrid_nextpage","iparms":[{"av":"OUTPUTSGRID_nFirstRecordOnPage","type":"int"},{"av":"OUTPUTSGRID_nEOF","type":"int"},{"av":"AV10outputs","fld":"vOUTPUTS","grid":35,"type":""},{"av":"nGXsfl_35_idx","ctrl":"GRID","prop":"GridCurrRow","grid":35},{"av":"nRC_GXsfl_35","ctrl":"OUTPUTSGRID","prop":"GridRC","grid":35,"type":"int"},{"av":"subInputsgrid_Rows","ctrl":"INPUTSGRID","prop":"Rows"},{"av":"subOutputsgrid_Rows","ctrl":"OUTPUTSGRID","prop":"Rows"},{"av":"AV12transactionFileName","fld":"vTRANSACTIONFILENAME","hsh":true,"type":"char"},{"av":"AV13transactionId","fld":"vTRANSACTIONID","hsh":true,"type":"char"},{"av":"AV9inputs","fld":"vINPUTS","grid":25,"type":""},{"av":"nGXsfl_25_idx","ctrl":"GRID","prop":"GridCurrRow","grid":25},{"av":"INPUTSGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_25","ctrl":"INPUTSGRID","prop":"GridRC","grid":25,"type":"int"}]}""");
         setEventMetadata("OUTPUTSGRID_LASTPAGE","""{"handler":"suboutputsgrid_lastpage","iparms":[{"av":"OUTPUTSGRID_nFirstRecordOnPage","type":"int"},{"av":"OUTPUTSGRID_nEOF","type":"int"},{"av":"AV10outputs","fld":"vOUTPUTS","grid":35,"type":""},{"av":"nGXsfl_35_idx","ctrl":"GRID","prop":"GridCurrRow","grid":35},{"av":"nRC_GXsfl_35","ctrl":"OUTPUTSGRID","prop":"GridRC","grid":35,"type":"int"},{"av":"subInputsgrid_Rows","ctrl":"INPUTSGRID","prop":"Rows"},{"av":"subOutputsgrid_Rows","ctrl":"OUTPUTSGRID","prop":"Rows"},{"av":"AV12transactionFileName","fld":"vTRANSACTIONFILENAME","hsh":true,"type":"char"},{"av":"AV13transactionId","fld":"vTRANSACTIONID","hsh":true,"type":"char"},{"av":"AV9inputs","fld":"vINPUTS","grid":25,"type":""},{"av":"nGXsfl_25_idx","ctrl":"GRID","prop":"GridCurrRow","grid":25},{"av":"INPUTSGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_25","ctrl":"INPUTSGRID","prop":"GridRC","grid":25,"type":"int"}]}""");
         setEventMetadata("NULL","""{"handler":"Validv_Gxv5","iparms":[]}""");
         setEventMetadata("NULL","""{"handler":"Validv_Gxv10","iparms":[]}""");
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
         wcpOAV13transactionId = "";
         wcpOAV12transactionFileName = "";
         gxfirstwebparm = "";
         gxfirstwebparm_bkp = "";
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         AV10outputs = new GXBaseCollection<GeneXus.Programs.wallet.SdtTransactionLine>( context, "TransactionLine", "distributedcryptography");
         AV9inputs = new GXBaseCollection<GeneXus.Programs.wallet.SdtTransactionLine>( context, "TransactionLine", "distributedcryptography");
         GX_FocusControl = "";
         sPrefix = "";
         lblTbheader_Jsonclick = "";
         TempTags = "";
         ClassString = "";
         StyleString = "";
         bttForgetpayment_Jsonclick = "";
         bttClose_Jsonclick = "";
         AV11shownTransactionId = "";
         AV6explorerLink = "";
         lblTbinputs_Jsonclick = "";
         InputsgridContainer = new GXWebGrid( context);
         sStyleString = "";
         lblTboutputs_Jsonclick = "";
         OutputsgridContainer = new GXWebGrid( context);
         Form = new GXWebForm();
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         AV7header = new GeneXus.Programs.wallet.SdtWalletTransactionItem(context);
         AV5error = "";
         AV8headerText = "";
         AV14walletInfo = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         GXt_SdtWalletInfo1 = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         InputsgridRow = new GXWebRow();
         OutputsgridRow = new GXWebRow();
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         subInputsgrid_Linesclass = "";
         ROClassString = "";
         subOutputsgrid_Linesclass = "";
         InputsgridColumn = new GXWebColumn();
         OutputsgridColumn = new GXWebColumn();
         /* GeneXus formulas. */
         edtavShowntransactionid_Enabled = 0;
         edtavExplorerlink_Enabled = 0;
         edtavCtlinowner_Enabled = 0;
         edtavCtlinaddress_Enabled = 0;
         edtavCtlinvalue_Enabled = 0;
         edtavCtlinoutpoint_Enabled = 0;
         edtavCtloutn_Enabled = 0;
         edtavCtloutowner_Enabled = 0;
         edtavCtloutaddress_Enabled = 0;
         edtavCtloutvalue_Enabled = 0;
      }

      private short nRcdExists_3 ;
      private short nIsMod_3 ;
      private short INPUTSGRID_nEOF ;
      private short OUTPUTSGRID_nEOF ;
      private short nGotPars ;
      private short GxWebError ;
      private short wbEnd ;
      private short wbStart ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short subInputsgrid_Backcolorstyle ;
      private short subOutputsgrid_Backcolorstyle ;
      private short nGXWrapped ;
      private short subInputsgrid_Backstyle ;
      private short subOutputsgrid_Backstyle ;
      private short subInputsgrid_Titlebackstyle ;
      private short subInputsgrid_Allowselection ;
      private short subInputsgrid_Allowhovering ;
      private short subInputsgrid_Allowcollapsing ;
      private short subInputsgrid_Collapsed ;
      private short subOutputsgrid_Titlebackstyle ;
      private short subOutputsgrid_Allowselection ;
      private short subOutputsgrid_Allowhovering ;
      private short subOutputsgrid_Allowcollapsing ;
      private short subOutputsgrid_Collapsed ;
      private int nRC_GXsfl_25 ;
      private int nRC_GXsfl_35 ;
      private int subInputsgrid_Rows ;
      private int subOutputsgrid_Rows ;
      private int nGXsfl_25_idx=1 ;
      private int nGXsfl_35_idx=1 ;
      private int edtavShowntransactionid_Enabled ;
      private int edtavExplorerlink_Enabled ;
      private int edtavCtlinowner_Enabled ;
      private int edtavCtlinaddress_Enabled ;
      private int edtavCtlinvalue_Enabled ;
      private int edtavCtlinoutpoint_Enabled ;
      private int edtavCtloutn_Enabled ;
      private int edtavCtloutowner_Enabled ;
      private int edtavCtloutaddress_Enabled ;
      private int edtavCtloutvalue_Enabled ;
      private int bttForgetpayment_Visible ;
      private int AV16GXV1 ;
      private int AV21GXV6 ;
      private int subInputsgrid_Islastpage ;
      private int subOutputsgrid_Islastpage ;
      private int INPUTSGRID_nGridOutOfScope ;
      private int OUTPUTSGRID_nGridOutOfScope ;
      private int nGXsfl_25_fel_idx=1 ;
      private int nGXsfl_35_fel_idx=1 ;
      private int idxLst ;
      private int subInputsgrid_Backcolor ;
      private int subInputsgrid_Allbackcolor ;
      private int subOutputsgrid_Backcolor ;
      private int subOutputsgrid_Allbackcolor ;
      private int subInputsgrid_Titlebackcolor ;
      private int subInputsgrid_Selectedindex ;
      private int subInputsgrid_Selectioncolor ;
      private int subInputsgrid_Hoveringcolor ;
      private int subOutputsgrid_Titlebackcolor ;
      private int subOutputsgrid_Selectedindex ;
      private int subOutputsgrid_Selectioncolor ;
      private int subOutputsgrid_Hoveringcolor ;
      private long INPUTSGRID_nFirstRecordOnPage ;
      private long OUTPUTSGRID_nFirstRecordOnPage ;
      private long INPUTSGRID_nCurrentRecord ;
      private long OUTPUTSGRID_nCurrentRecord ;
      private long INPUTSGRID_nRecordCount ;
      private long OUTPUTSGRID_nRecordCount ;
      private string AV13transactionId ;
      private string AV12transactionFileName ;
      private string wcpOAV13transactionId ;
      private string wcpOAV12transactionFileName ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sGXsfl_25_idx="0001" ;
      private string sGXsfl_35_idx="0001" ;
      private string edtavShowntransactionid_Internalname ;
      private string edtavExplorerlink_Internalname ;
      private string edtavCtlinowner_Internalname ;
      private string edtavCtlinaddress_Internalname ;
      private string edtavCtlinvalue_Internalname ;
      private string edtavCtlinoutpoint_Internalname ;
      private string edtavCtloutn_Internalname ;
      private string edtavCtloutowner_Internalname ;
      private string edtavCtloutaddress_Internalname ;
      private string edtavCtloutvalue_Internalname ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string GX_FocusControl ;
      private string sPrefix ;
      private string divMaintable_Internalname ;
      private string lblTbheader_Internalname ;
      private string lblTbheader_Caption ;
      private string lblTbheader_Jsonclick ;
      private string TempTags ;
      private string ClassString ;
      private string StyleString ;
      private string bttForgetpayment_Internalname ;
      private string bttForgetpayment_Jsonclick ;
      private string bttClose_Internalname ;
      private string bttClose_Jsonclick ;
      private string edtavShowntransactionid_Jsonclick ;
      private string AV6explorerLink ;
      private string edtavExplorerlink_Link ;
      private string edtavExplorerlink_Linktarget ;
      private string edtavExplorerlink_Jsonclick ;
      private string lblTbinputs_Internalname ;
      private string lblTbinputs_Jsonclick ;
      private string sStyleString ;
      private string subInputsgrid_Internalname ;
      private string lblTboutputs_Internalname ;
      private string lblTboutputs_Jsonclick ;
      private string subOutputsgrid_Internalname ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string sGXsfl_25_fel_idx="0001" ;
      private string sGXsfl_35_fel_idx="0001" ;
      private string AV5error ;
      private string subInputsgrid_Class ;
      private string subInputsgrid_Linesclass ;
      private string ROClassString ;
      private string edtavCtlinowner_Jsonclick ;
      private string edtavCtlinaddress_Jsonclick ;
      private string edtavCtlinvalue_Jsonclick ;
      private string edtavCtlinoutpoint_Jsonclick ;
      private string subOutputsgrid_Class ;
      private string subOutputsgrid_Linesclass ;
      private string edtavCtloutn_Jsonclick ;
      private string edtavCtloutowner_Jsonclick ;
      private string edtavCtloutaddress_Jsonclick ;
      private string edtavCtloutvalue_Jsonclick ;
      private string subInputsgrid_Header ;
      private string subOutputsgrid_Header ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool bGXsfl_25_Refreshing=false ;
      private bool bGXsfl_35_Refreshing=false ;
      private bool AV15userResponse ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool gx_BV25 ;
      private bool gx_BV35 ;
      private string AV11shownTransactionId ;
      private string AV8headerText ;
      private GXWebGrid InputsgridContainer ;
      private GXWebGrid OutputsgridContainer ;
      private GXWebRow InputsgridRow ;
      private GXWebRow OutputsgridRow ;
      private GXWebColumn InputsgridColumn ;
      private GXWebColumn OutputsgridColumn ;
      private GXWebForm Form ;
      private IGxDataStore dsDefault ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtTransactionLine> AV10outputs ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtTransactionLine> AV9inputs ;
      private GeneXus.Programs.wallet.SdtWalletTransactionItem AV7header ;
      private GeneXus.Programs.wallet.SdtWalletInfo AV14walletInfo ;
      private GeneXus.Programs.wallet.SdtWalletInfo GXt_SdtWalletInfo1 ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
