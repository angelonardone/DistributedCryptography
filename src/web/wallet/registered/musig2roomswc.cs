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
namespace GeneXus.Programs.wallet.registered {
   public class musig2roomswc : GXWebComponent
   {
      public musig2roomswc( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         if ( StringUtil.Len( (string)(sPrefix)) == 0 )
         {
            context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
         }
      }

      public musig2roomswc( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( Guid aP0_groupId )
      {
         this.AV6groupId = aP0_groupId;
         ExecuteImpl();
      }

      protected override void ExecutePrivate( )
      {
         isStatic = false;
         webExecute();
      }

      public override void SetPrefix( string sPPrefix )
      {
         sPrefix = sPPrefix;
      }

      protected override void createObjects( )
      {
         chkavSendallcoins = new GXCheckbox();
         cmbavUserfee = new GXCombobox();
      }

      protected void INITWEB( )
      {
         initialize_properties( ) ;
         if ( StringUtil.Len( (string)(sPrefix)) == 0 )
         {
            if ( nGotPars == 0 )
            {
               entryPointCalled = false;
               gxfirstwebparm = GetFirstPar( "groupId");
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
               else if ( StringUtil.StrCmp(gxfirstwebparm, "dyncomponent") == 0 )
               {
                  setAjaxEventMode();
                  if ( ! IsValidAjaxCall( true) )
                  {
                     GxWebError = 1;
                     return  ;
                  }
                  nDynComponent = 1;
                  sCompPrefix = GetPar( "sCompPrefix");
                  sSFPrefix = GetPar( "sSFPrefix");
                  AV6groupId = StringUtil.StrToGuid( GetPar( "groupId"));
                  AssignAttri(sPrefix, false, "AV6groupId", AV6groupId.ToString());
                  setjustcreated();
                  componentprepare(new Object[] {(string)sCompPrefix,(string)sSFPrefix,(Guid)AV6groupId});
                  componentstart();
                  context.httpAjaxContext.ajax_rspStartCmp(sPrefix);
                  componentdraw();
                  context.httpAjaxContext.ajax_rspEndCmp();
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
                  gxfirstwebparm = GetFirstPar( "groupId");
               }
               else if ( StringUtil.StrCmp(gxfirstwebparm, "gxfullajaxEvt") == 0 )
               {
                  if ( ! IsValidAjaxCall( true) )
                  {
                     GxWebError = 1;
                     return  ;
                  }
                  gxfirstwebparm = GetFirstPar( "groupId");
               }
               else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxNewRow_"+"Gridrooms") == 0 )
               {
                  gxnrGridrooms_newrow_invoke( ) ;
                  return  ;
               }
               else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxGridRefresh_"+"Gridrooms") == 0 )
               {
                  gxgrGridrooms_refresh_invoke( ) ;
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
               if ( toggleJsOutput )
               {
                  if ( context.isSpaRequest( ) )
                  {
                     enableJsOutput();
                  }
               }
            }
         }
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            if ( ! context.IsLocalStorageSupported( ) )
            {
               context.PushCurrentUrl();
            }
         }
      }

      protected void gxnrGridrooms_newrow_invoke( )
      {
         nRC_GXsfl_96 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_96"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_96_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_96_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_96_idx = GetPar( "sGXsfl_96_idx");
         sPrefix = GetPar( "sPrefix");
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxnrGridrooms_newrow( ) ;
         /* End function gxnrGridrooms_newrow_invoke */
      }

      protected void gxgrGridrooms_refresh_invoke( )
      {
         subGridrooms_Rows = (int)(Math.Round(NumberUtil.Val( GetPar( "subGridrooms_Rows"), "."), 18, MidpointRounding.ToEven));
         AV6groupId = StringUtil.StrToGuid( GetPar( "groupId"));
         AV30ownerAlone = StringUtil.StrToBool( GetPar( "ownerAlone"));
         AV43selectedRoomId = StringUtil.StrToGuid( GetPar( "selectedRoomId"));
         ajax_req_read_hidden_sdt(GetNextPar( ), AV42selected);
         ajax_req_read_hidden_sdt(GetNextPar( ), AV7lines);
         AV16sendAllCoins = StringUtil.StrToBool( GetPar( "sendAllCoins"));
         sPrefix = GetPar( "sPrefix");
         init_default_properties( ) ;
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrGridrooms_refresh( subGridrooms_Rows, AV6groupId, AV30ownerAlone, AV43selectedRoomId, AV42selected, AV7lines, AV16sendAllCoins, sPrefix) ;
         AddString( context.getJSONResponse( )) ;
         /* End function gxgrGridrooms_refresh_invoke */
      }

      public override void webExecute( )
      {
         createObjects();
         initialize();
         INITWEB( ) ;
         if ( ! isAjaxCallMode( ) )
         {
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               ValidateSpaRequest();
            }
            PA3T2( ) ;
            if ( ( GxWebError == 0 ) && ! isAjaxCallMode( ) )
            {
               /* GeneXus formulas. */
               edtavCtlroomid_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlroomid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlroomid_Enabled), 5, 0), !bGXsfl_96_Refreshing);
               edtavMark_Enabled = 0;
               AssignProp(sPrefix, false, edtavMark_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavMark_Enabled), 5, 0), !bGXsfl_96_Refreshing);
               edtavCtlcreated_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlcreated_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlcreated_Enabled), 5, 0), !bGXsfl_96_Refreshing);
               edtavCtlopenedby_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlopenedby_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlopenedby_Enabled), 5, 0), !bGXsfl_96_Refreshing);
               edtavCtldescription_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtldescription_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtldescription_Enabled), 5, 0), !bGXsfl_96_Refreshing);
               edtavCtlamounttext_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlamounttext_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlamounttext_Enabled), 5, 0), !bGXsfl_96_Refreshing);
               edtavCtlstateshort_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlstateshort_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlstateshort_Enabled), 5, 0), !bGXsfl_96_Refreshing);
               edtavOpen_Enabled = 0;
               AssignProp(sPrefix, false, edtavOpen_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavOpen_Enabled), 5, 0), !bGXsfl_96_Refreshing);
               edtavChatlogview_Enabled = 0;
               AssignProp(sPrefix, false, edtavChatlogview_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavChatlogview_Enabled), 5, 0), true);
               WS3T2( ) ;
               if ( ! isAjaxCallMode( ) )
               {
                  if ( nDynComponent == 0 )
                  {
                     throw new System.Net.WebException("WebComponent is not allowed to run") ;
                  }
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
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            if ( context.isSpaRequest( ) )
            {
               enableOutput();
            }
            context.WriteHtmlText( "<title>") ;
            context.SendWebValue( "MuSig2 payments of a group: signing sessions (grid + the selected one), and the owner of a Delegated MultiSig 2 paying alone") ;
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
         }
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
         if ( StringUtil.Len( sPrefix) == 0 )
         {
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
            context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wallet.registered.musig2roomswc", new object[] {UrlEncode(AV6groupId.ToString())}, new string[] {"groupId"}) +"\">") ;
            GxWebStd.gx_hidden_field( context, "_EventName", "");
            GxWebStd.gx_hidden_field( context, "_EventGridId", "");
            GxWebStd.gx_hidden_field( context, "_EventRowId", "");
            context.WriteHtmlText( "<div style=\"height:0;overflow:hidden\"><input type=\"submit\" title=\"submit\"  disabled></div>") ;
            AssignProp(sPrefix, false, "FORM", "Class", "form-horizontal Form", true);
         }
         else
         {
            bool toggleHtmlOutput = isOutputEnabled( );
            if ( StringUtil.StringSearch( sPrefix, "MP", 1) == 1 )
            {
               if ( context.isSpaRequest( ) )
               {
                  disableOutput();
               }
            }
            context.WriteHtmlText( "<div") ;
            GxWebStd.ClassAttribute( context, "gxwebcomponent-body"+" "+(String.IsNullOrEmpty(StringUtil.RTrim( Form.Class)) ? "form-horizontal Form" : Form.Class)+"-fx");
            context.WriteHtmlText( ">") ;
            if ( toggleHtmlOutput )
            {
               if ( StringUtil.StringSearch( sPrefix, "MP", 1) == 1 )
               {
                  if ( context.isSpaRequest( ) )
                  {
                     enableOutput();
                  }
               }
            }
            toggleJsOutput = isJsOutputEnabled( );
            if ( context.isSpaRequest( ) )
            {
               disableJsOutput();
            }
         }
         if ( StringUtil.StringSearch( sPrefix, "MP", 1) == 1 )
         {
            if ( context.isSpaRequest( ) )
            {
               disableOutput();
            }
         }
      }

      protected void send_integrity_footer_hashes( )
      {
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vOWNERALONE", AV30ownerAlone);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vOWNERALONE", GetSecureSignedToken( sPrefix, AV30ownerAlone, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vSELECTED", AV42selected);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vSELECTED", AV42selected);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vSELECTED", GetSecureSignedToken( sPrefix, AV42selected, context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"Lines", AV7lines);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"Lines", AV7lines);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"nRC_GXsfl_96", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_96), 8, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"wcpOAV6groupId", wcpOAV6groupId.ToString());
         GxWebStd.gx_hidden_field( context, sPrefix+"vGROUPID", AV6groupId.ToString());
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vOWNERALONE", AV30ownerAlone);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vOWNERALONE", GetSecureSignedToken( sPrefix, AV30ownerAlone, context));
         GxWebStd.gx_hidden_field( context, sPrefix+"vSELECTEDROOMID", AV43selectedRoomId.ToString());
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vSELECTED", AV42selected);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vSELECTED", AV42selected);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vSELECTED", GetSecureSignedToken( sPrefix, AV42selected, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vLINES", AV7lines);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vLINES", AV7lines);
         }
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vCHECKED", AV22checked);
         GxWebStd.gx_hidden_field( context, sPrefix+"vECONOMICALFEE", StringUtil.LTrim( StringUtil.NToC( AV24economicalFee, 16, 8, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"vECONOMICALBLOCKS", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV23economicalBlocks), 10, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"vSTANDARDFEE", StringUtil.LTrim( StringUtil.NToC( AV32standardFee, 16, 8, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"vSTANDARBLOCKS", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV31standarBlocks), 10, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"vFASTESTFEE", StringUtil.LTrim( StringUtil.NToC( AV27fastestFee, 16, 8, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"vFASTESTBLOCKS", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV26fastestBlocks), 10, 0, ".", "")));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vBROADCAST", AV21broadcast);
         GxWebStd.gx_hidden_field( context, sPrefix+"vERRORTITLE", StringUtil.RTrim( AV25errorTitle));
         GxWebStd.gx_hidden_field( context, sPrefix+"vERROR", AV5error);
         GxWebStd.gx_hidden_field( context, sPrefix+"vSIGNROOMID", AV10signRoomId.ToString());
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vFORMOPEN", AV13formOpen);
         GxWebStd.gx_hidden_field( context, sPrefix+"GRIDROOMS_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRIDROOMS_nFirstRecordOnPage), 15, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"GRIDROOMS_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRIDROOMS_nEOF), 1, 0, ".", "")));
      }

      protected void RenderHtmlCloseForm3T2( )
      {
         SendCloseFormHiddens( ) ;
         if ( ( StringUtil.Len( sPrefix) != 0 ) && ( context.isAjaxRequest( ) || context.isSpaRequest( ) ) )
         {
            componentjscripts();
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"GX_FocusControl", GX_FocusControl);
         define_styles( ) ;
         SendSecurityToken(sPrefix);
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            SendAjaxEncryptionKey();
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
         else
         {
            SendWebComponentState();
            context.WriteHtmlText( "</div>") ;
            if ( toggleJsOutput )
            {
               if ( context.isSpaRequest( ) )
               {
                  enableJsOutput();
               }
            }
         }
      }

      public override string GetPgmname( )
      {
         return "Wallet.registered.MuSig2RoomsWC" ;
      }

      public override string GetPgmdesc( )
      {
         return "MuSig2 payments of a group: signing sessions (grid + the selected one), and the owner of a Delegated MultiSig 2 paying alone" ;
      }

      protected void WB3T0( )
      {
         if ( context.isAjaxRequest( ) )
         {
            disableOutput();
         }
         if ( ! wbLoad )
         {
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               RenderHtmlHeaders( ) ;
            }
            RenderHtmlOpenForm( ) ;
            if ( StringUtil.Len( sPrefix) != 0 )
            {
               GxWebStd.gx_hidden_field( context, sPrefix+"_CMPPGM", "wallet.registered.musig2roomswc");
            }
            GxWebStd.gx_msg_list( context, "", context.GX_msglist.DisplayMode, "", "", sPrefix, "false");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "Section", "start", "top", " "+"data-gx-base-lib=\"none\""+" "+"data-abstract-form"+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divMaintable_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divNewtable_Internalname, divNewtable_Visible, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTbnew_Internalname, lblTbnew_Caption, "", "", lblTbnew_Jsonclick, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 0, "HLP_Wallet/registered/MuSig2RoomsWC.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavSendallcoins_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavSendallcoins_Internalname, "Send total balance", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 14,'" + sPrefix + "',false,'" + sGXsfl_96_idx + "',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavSendallcoins_Internalname, StringUtil.BoolToStr( AV16sendAllCoins), "", "Send total balance", 1, chkavSendallcoins.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(14, this, 'true', 'false',"+"'"+sPrefix+"'"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,14);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavSendcoins_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavSendcoins_Internalname, "Amount to send (in BTC)", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 19,'" + sPrefix + "',false,'" + sGXsfl_96_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavSendcoins_Internalname, StringUtil.LTrim( StringUtil.NToC( AV17sendCoins, 16, 8, ".", "")), StringUtil.LTrim( ((edtavSendcoins_Enabled!=0) ? context.localUtil.Format( AV17sendCoins, "ZZZZZZ9.99999999") : context.localUtil.Format( AV17sendCoins, "ZZZZZZ9.99999999"))), TempTags+" onchange=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onblur(this,19);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavSendcoins_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavSendcoins_Enabled, 0, "text", "", 16, "chr", 1, "row", 16, 0, 0, 0, 0, -1, 0, true, "NBitcoin\\BTC", "end", false, "", "HLP_Wallet/registered/MuSig2RoomsWC.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavSendto_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavSendto_Internalname, "Send to address", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Multiple line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 24,'" + sPrefix + "',false,'" + sGXsfl_96_idx + "',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_html_textarea( context, edtavSendto_Internalname, StringUtil.RTrim( AV18sendTo), "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,24);\"", 0, 1, edtavSendto_Enabled, 0, 80, "chr", 4, "row", 0, StyleString, ClassString, "", "", "250", -1, 0, "", "", -1, true, "NBitcoin\\scriptPubKey_address", "'"+sPrefix+"'"+",false,"+"'"+""+"'", 0, "", "HLP_Wallet/registered/MuSig2RoomsWC.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavDescription_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavDescription_Internalname, "Description", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Multiple line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 29,'" + sPrefix + "',false,'" + sGXsfl_96_idx + "',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_html_textarea( context, edtavDescription_Internalname, AV12description, "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,29);\"", 0, 1, edtavDescription_Enabled, 0, 80, "chr", 4, "row", 0, StyleString, ClassString, "", "", "250", -1, 0, "", "", -1, true, "", "'"+sPrefix+"'"+",false,"+"'"+""+"'", 0, "", "HLP_Wallet/registered/MuSig2RoomsWC.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divOwnerfeetable_Internalname, divOwnerfeetable_Visible, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavUserfee_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavUserfee_Internalname, "Select Fee", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 37,'" + sPrefix + "',false,'" + sGXsfl_96_idx + "',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavUserfee, cmbavUserfee_Internalname, StringUtil.Trim( StringUtil.Str( AV33userFee, 16, 8)), 1, cmbavUserfee_Jsonclick, 0, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "decimal", "", 1, cmbavUserfee.Enabled, 0, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,37);\"", "", true, 0, "HLP_Wallet/registered/MuSig2RoomsWC.htm");
            cmbavUserfee.CurrentValue = StringUtil.Trim( StringUtil.Str( AV33userFee, 16, 8));
            AssignProp(sPrefix, false, cmbavUserfee_Internalname, "Values", (string)(cmbavUserfee.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavManualfee_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavManualfee_Internalname, "Fee (BTC)", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 42,'" + sPrefix + "',false,'" + sGXsfl_96_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavManualfee_Internalname, StringUtil.LTrim( StringUtil.NToC( AV29manualFee, 16, 8, ".", "")), StringUtil.LTrim( ((edtavManualfee_Enabled!=0) ? context.localUtil.Format( AV29manualFee, "ZZZZZZ9.99999999") : context.localUtil.Format( AV29manualFee, "ZZZZZZ9.99999999"))), TempTags+" onchange=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_decimal( this, ',','.','8');"+";gx.evt.onblur(this,42);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavManualfee_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavManualfee_Enabled, 0, "text", "", 16, "chr", 1, "row", 16, 0, 0, 0, 0, -1, 0, true, "NBitcoin\\BTC", "end", false, "", "HLP_Wallet/registered/MuSig2RoomsWC.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavOwnerpassword_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavOwnerpassword_Internalname, "Enter password", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 47,'" + sPrefix + "',false,'" + sGXsfl_96_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavOwnerpassword_Internalname, StringUtil.RTrim( AV38ownerPassword), StringUtil.RTrim( context.localUtil.Format( AV38ownerPassword, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,47);\""+" "+"data-gx-password-reveal"+" ", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavOwnerpassword_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavOwnerpassword_Enabled, 0, "text", "", 80, "chr", 1, "row", 100, -1, 0, 0, 0, 0, -1, true, "", "start", true, "", "HLP_Wallet/registered/MuSig2RoomsWC.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 50,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttPayalone_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(96), 2, 0)+","+"null"+");", "Pay", bttPayalone_Jsonclick, 5, "Pay", "", StyleString, ClassString, 1, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'PAY ALONE\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/MuSig2RoomsWC.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            wb_table1_53_3T2( true) ;
         }
         else
         {
            wb_table1_53_3T2( false) ;
         }
         return  ;
      }

      protected void wb_table1_53_3T2e( bool wbgen )
      {
         if ( wbgen )
         {
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divSigntable_Internalname, divSigntable_Visible, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTbsign_Internalname, lblTbsign_Caption, "", "", lblTbsign_Jsonclick, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 0, "HLP_Wallet/registered/MuSig2RoomsWC.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavPassword_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavPassword_Internalname, "Enter password", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 71,'" + sPrefix + "',false,'" + sGXsfl_96_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavPassword_Internalname, StringUtil.RTrim( AV14password), StringUtil.RTrim( context.localUtil.Format( AV14password, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,71);\""+" "+"data-gx-password-reveal"+" ", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavPassword_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavPassword_Enabled, 0, "text", "", 80, "chr", 1, "row", 100, -1, 0, 0, 0, 0, -1, true, "", "start", true, "", "HLP_Wallet/registered/MuSig2RoomsWC.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            wb_table2_74_3T2( true) ;
         }
         else
         {
            wb_table2_74_3T2( false) ;
         }
         return  ;
      }

      protected void wb_table2_74_3T2e( bool wbgen )
      {
         if ( wbgen )
         {
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divListtable_Internalname, divListtable_Visible, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTbempty_Internalname, lblTbempty_Caption, "", "", lblTbempty_Jsonclick, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 0, "HLP_Wallet/registered/MuSig2RoomsWC.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            wb_table3_88_3T2( true) ;
         }
         else
         {
            wb_table3_88_3T2( false) ;
         }
         return  ;
      }

      protected void wb_table3_88_3T2e( bool wbgen )
      {
         if ( wbgen )
         {
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /*  Grid Control  */
            GridroomsContainer.SetWrapped(nGXWrapped);
            StartGridControl96( ) ;
         }
         if ( wbEnd == 96 )
         {
            wbEnd = 0;
            nRC_GXsfl_96 = (int)(nGXsfl_96_idx-1);
            if ( GridroomsContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "</table>") ;
               context.WriteHtmlText( "</div>") ;
            }
            else
            {
               GridroomsContainer.AddObjectProperty("GRIDROOMS_nEOF", GRIDROOMS_nEOF);
               GridroomsContainer.AddObjectProperty("GRIDROOMS_nFirstRecordOnPage", GRIDROOMS_nFirstRecordOnPage);
               AV46GXV1 = nGXsfl_96_idx;
               sStyleString = "";
               context.WriteHtmlText( "<div id=\""+sPrefix+"GridroomsContainer"+"Div\" "+sStyleString+">"+"</div>") ;
               context.httpAjaxContext.ajax_rsp_assign_grid(sPrefix+"_"+"Gridrooms", GridroomsContainer, subGridrooms_Internalname);
               if ( ! isAjaxCallMode( ) && ! context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, sPrefix+"GridroomsContainerData", GridroomsContainer.ToJavascriptSource());
               }
               if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, sPrefix+"GridroomsContainerData"+"V", GridroomsContainer.GridValuesHidden());
               }
               else
               {
                  context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+sPrefix+"GridroomsContainerData"+"V"+"\" value='"+GridroomsContainer.GridValuesHidden()+"'/>") ;
               }
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divRoomtable_Internalname, divRoomtable_Visible, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTbroomtitle_Internalname, lblTbroomtitle_Caption, "", "", lblTbroomtitle_Jsonclick, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 0, "HLP_Wallet/registered/MuSig2RoomsWC.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTbroomstate_Internalname, lblTbroomstate_Caption, "", "", lblTbroomstate_Jsonclick, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 0, "HLP_Wallet/registered/MuSig2RoomsWC.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTbroompeople_Internalname, lblTbroompeople_Caption, "", "", lblTbroompeople_Jsonclick, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 0, "HLP_Wallet/registered/MuSig2RoomsWC.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTbroomwaiting_Internalname, lblTbroomwaiting_Caption, "", "", lblTbroomwaiting_Jsonclick, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 0, "HLP_Wallet/registered/MuSig2RoomsWC.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            wb_table4_122_3T2( true) ;
         }
         else
         {
            wb_table4_122_3T2( false) ;
         }
         return  ;
      }

      protected void wb_table4_122_3T2e( bool wbgen )
      {
         if ( wbgen )
         {
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavChatlogview_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavChatlogview_Internalname, "Chat of this session", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Multiple line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 136,'" + sPrefix + "',false,'" + sGXsfl_96_idx + "',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_html_textarea( context, edtavChatlogview_Internalname, AV39chatLogView, "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,136);\"", 0, 1, edtavChatlogview_Enabled, 0, 80, "chr", 10, "row", 0, StyleString, ClassString, "", "", "2097152", -1, 0, "", "", -1, true, "", "'"+sPrefix+"'"+",false,"+"'"+""+"'", 0, "", "HLP_Wallet/registered/MuSig2RoomsWC.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            wb_table5_139_3T2( true) ;
         }
         else
         {
            wb_table5_139_3T2( false) ;
         }
         return  ;
      }

      protected void wb_table5_139_3T2e( bool wbgen )
      {
         if ( wbgen )
         {
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         if ( wbEnd == 96 )
         {
            wbEnd = 0;
            if ( isFullAjaxMode( ) )
            {
               if ( GridroomsContainer.GetWrapped() == 1 )
               {
                  context.WriteHtmlText( "</table>") ;
                  context.WriteHtmlText( "</div>") ;
               }
               else
               {
                  GridroomsContainer.AddObjectProperty("GRIDROOMS_nEOF", GRIDROOMS_nEOF);
                  GridroomsContainer.AddObjectProperty("GRIDROOMS_nFirstRecordOnPage", GRIDROOMS_nFirstRecordOnPage);
                  AV46GXV1 = nGXsfl_96_idx;
                  sStyleString = "";
                  context.WriteHtmlText( "<div id=\""+sPrefix+"GridroomsContainer"+"Div\" "+sStyleString+">"+"</div>") ;
                  context.httpAjaxContext.ajax_rsp_assign_grid(sPrefix+"_"+"Gridrooms", GridroomsContainer, subGridrooms_Internalname);
                  if ( ! isAjaxCallMode( ) && ! context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, sPrefix+"GridroomsContainerData", GridroomsContainer.ToJavascriptSource());
                  }
                  if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, sPrefix+"GridroomsContainerData"+"V", GridroomsContainer.GridValuesHidden());
                  }
                  else
                  {
                     context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+sPrefix+"GridroomsContainerData"+"V"+"\" value='"+GridroomsContainer.GridValuesHidden()+"'/>") ;
                  }
               }
            }
         }
         wbLoad = true;
      }

      protected void START3T2( )
      {
         wbLoad = false;
         wbEnd = 0;
         wbStart = 0;
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            if ( ! context.isSpaRequest( ) )
            {
               if ( context.ExposeMetadata( ) )
               {
                  Form.Meta.addItem("generator", "GeneXus .NET 18_0_16-189595", 0) ;
               }
            }
            Form.Meta.addItem("description", "MuSig2 payments of a group: signing sessions (grid + the selected one), and the owner of a Delegated MultiSig 2 paying alone", 0) ;
            context.wjLoc = "";
            context.nUserReturn = 0;
            context.wbHandled = 0;
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               sXEvt = cgiGet( "_EventName");
               if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) )
               {
               }
            }
         }
         wbErr = false;
         if ( ( StringUtil.Len( sPrefix) == 0 ) || ( nDraw == 1 ) )
         {
            if ( nDoneStart == 0 )
            {
               STRUP3T0( ) ;
            }
         }
      }

      protected void WS3T2( )
      {
         START3T2( ) ;
         EVT3T2( ) ;
      }

      protected void EVT3T2( )
      {
         sXEvt = cgiGet( "_EventName");
         if ( ( ( ( StringUtil.Len( sPrefix) == 0 ) ) || ( StringUtil.StringSearch( sXEvt, sPrefix, 1) > 0 ) ) && ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) )
         {
            if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) && ! wbErr )
            {
               /* Read Web Panel buttons. */
               if ( context.wbHandled == 0 )
               {
                  if ( StringUtil.Len( sPrefix) == 0 )
                  {
                     sEvt = cgiGet( "_EventName");
                     EvtGridId = cgiGet( "_EventGridId");
                     EvtRowId = cgiGet( "_EventRowId");
                  }
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
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP3T0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'OPEN ROOM'") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP3T0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Open room' */
                                    E113T2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'ESTIMATE FEE'") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP3T0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Estimate fee' */
                                    E123T2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'PAY ALONE'") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP3T0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Pay alone' */
                                    E133T2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'REFRESH ROOMS'") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP3T0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Refresh rooms' */
                                    E143T2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'HERE'") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP3T0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Here' */
                                    E153T2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'SIGN NOW'") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP3T0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Sign now' */
                                    E163T2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'CONFIRM SIGN'") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP3T0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Confirm sign' */
                                    E173T2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'CANCEL FORM'") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP3T0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Cancel form' */
                                    E183T2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'LEAVE'") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP3T0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Leave' */
                                    E193T2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'SEND CHAT'") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP3T0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Send chat' */
                                    E203T2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "GLOBALEVENTS.RECEIVECHAT") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP3T0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    E213T2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP3T0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    GX_FocusControl = edtavCtlroomid_Internalname;
                                    AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "GRIDROOMSPAGING") == 0 )
                           {
                              context.wbHandled = 1;
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP3T0( ) ;
                              }
                              sEvt = cgiGet( sPrefix+"GRIDROOMSPAGING");
                              if ( StringUtil.StrCmp(sEvt, "FIRST") == 0 )
                              {
                                 subgridrooms_firstpage( ) ;
                              }
                              else if ( StringUtil.StrCmp(sEvt, "PREV") == 0 )
                              {
                                 subgridrooms_previouspage( ) ;
                              }
                              else if ( StringUtil.StrCmp(sEvt, "NEXT") == 0 )
                              {
                                 subgridrooms_nextpage( ) ;
                              }
                              else if ( StringUtil.StrCmp(sEvt, "LAST") == 0 )
                              {
                                 subgridrooms_lastpage( ) ;
                              }
                              dynload_actions( ) ;
                           }
                        }
                        else
                        {
                           sEvtType = StringUtil.Right( sEvt, 4);
                           sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-4));
                           if ( ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "START") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 7), "REFRESH") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 14), "GRIDROOMS.LOAD") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 6), "'OPEN'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "ENTER") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 6), "CANCEL") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 6), "'OPEN'") == 0 ) )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP3T0( ) ;
                              }
                              nGXsfl_96_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                              sGXsfl_96_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_96_idx), 4, 0), 4, "0");
                              SubsflControlProps_962( ) ;
                              AV46GXV1 = (int)(nGXsfl_96_idx+GRIDROOMS_nFirstRecordOnPage);
                              if ( ( AV7lines.Count >= AV46GXV1 ) && ( AV46GXV1 > 0 ) )
                              {
                                 AV7lines.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtMuSig2RoomLine)AV7lines.Item(AV46GXV1));
                                 AV45mark = cgiGet( edtavMark_Internalname);
                                 AssignAttri(sPrefix, false, edtavMark_Internalname, AV45mark);
                                 AV41open = cgiGet( edtavOpen_Internalname);
                                 AssignAttri(sPrefix, false, edtavOpen_Internalname, AV41open);
                              }
                              sEvtType = StringUtil.Right( sEvt, 1);
                              if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                              {
                                 sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                                 if ( StringUtil.StrCmp(sEvt, "START") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtlroomid_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: Start */
                                          E223T2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "REFRESH") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtlroomid_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: Refresh */
                                          E233T2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "GRIDROOMS.LOAD") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtlroomid_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: Gridrooms.Load */
                                          E243T2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'OPEN'") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtlroomid_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: 'Open' */
                                          E253T2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "ENTER") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          if ( ! wbErr )
                                          {
                                             Rfr0gs = false;
                                             if ( ! Rfr0gs )
                                             {
                                             }
                                             dynload_actions( ) ;
                                          }
                                       }
                                    }
                                    /* No code required for Cancel button. It is implemented as the Reset button. */
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                                 {
                                    if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                                    {
                                       STRUP3T0( ) ;
                                    }
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtlroomid_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                       }
                                    }
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

      protected void WE3T2( )
      {
         if ( ! GxWebStd.gx_redirect( context) )
         {
            Rfr0gs = true;
            Refresh( ) ;
            if ( ! GxWebStd.gx_redirect( context) )
            {
               RenderHtmlCloseForm3T2( ) ;
            }
         }
      }

      protected void PA3T2( )
      {
         if ( nDonePA == 0 )
         {
            if ( StringUtil.Len( sPrefix) != 0 )
            {
               initialize_properties( ) ;
            }
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
               {
                  gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
               }
            }
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            toggleJsOutput = isJsOutputEnabled( );
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               if ( context.isSpaRequest( ) )
               {
                  disableJsOutput();
               }
            }
            init_web_controls( ) ;
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               if ( toggleJsOutput )
               {
                  if ( context.isSpaRequest( ) )
                  {
                     enableJsOutput();
                  }
               }
            }
            if ( ! context.isAjaxRequest( ) )
            {
               GX_FocusControl = chkavSendallcoins_Internalname;
               AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
            }
            nDonePA = 1;
         }
      }

      protected void dynload_actions( )
      {
         /* End function dynload_actions */
      }

      protected void gxnrGridrooms_newrow( )
      {
         GxWebStd.set_html_headers( context, 0, "", "");
         SubsflControlProps_962( ) ;
         while ( nGXsfl_96_idx <= nRC_GXsfl_96 )
         {
            sendrow_962( ) ;
            nGXsfl_96_idx = ((subGridrooms_Islastpage==1)&&(nGXsfl_96_idx+1>subGridrooms_fnc_Recordsperpage( )) ? 1 : nGXsfl_96_idx+1);
            sGXsfl_96_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_96_idx), 4, 0), 4, "0");
            SubsflControlProps_962( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( GridroomsContainer)) ;
         /* End function gxnrGridrooms_newrow */
      }

      protected void gxgrGridrooms_refresh( int subGridrooms_Rows ,
                                            Guid AV6groupId ,
                                            bool AV30ownerAlone ,
                                            Guid AV43selectedRoomId ,
                                            GeneXus.Programs.wallet.registered.SdtMuSig2RoomLine AV42selected ,
                                            GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSig2RoomLine> AV7lines ,
                                            bool AV16sendAllCoins ,
                                            string sPrefix )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         GRIDROOMS_nCurrentRecord = 0;
         RF3T2( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         /* End function gxgrGridrooms_refresh */
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
         AV16sendAllCoins = StringUtil.StrToBool( StringUtil.BoolToStr( AV16sendAllCoins));
         AssignAttri(sPrefix, false, "AV16sendAllCoins", AV16sendAllCoins);
         if ( cmbavUserfee.ItemCount > 0 )
         {
            AV33userFee = NumberUtil.Val( cmbavUserfee.getValidValue(StringUtil.Trim( StringUtil.Str( AV33userFee, 16, 8))), ".");
            AssignAttri(sPrefix, false, "AV33userFee", StringUtil.LTrimStr( AV33userFee, 16, 8));
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavUserfee.CurrentValue = StringUtil.Trim( StringUtil.Str( AV33userFee, 16, 8));
            AssignProp(sPrefix, false, cmbavUserfee_Internalname, "Values", cmbavUserfee.ToJavascriptSource(), true);
         }
      }

      public void Refresh( )
      {
         send_integrity_hashes( ) ;
         RF3T2( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         edtavCtlroomid_Enabled = 0;
         edtavMark_Enabled = 0;
         edtavCtlcreated_Enabled = 0;
         edtavCtlopenedby_Enabled = 0;
         edtavCtldescription_Enabled = 0;
         edtavCtlamounttext_Enabled = 0;
         edtavCtlstateshort_Enabled = 0;
         edtavOpen_Enabled = 0;
         edtavChatlogview_Enabled = 0;
         AssignProp(sPrefix, false, edtavChatlogview_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavChatlogview_Enabled), 5, 0), true);
      }

      protected void RF3T2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            GridroomsContainer.ClearRows();
         }
         wbStart = 96;
         /* Execute user event: Refresh */
         E233T2 ();
         nGXsfl_96_idx = 1;
         sGXsfl_96_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_96_idx), 4, 0), 4, "0");
         SubsflControlProps_962( ) ;
         bGXsfl_96_Refreshing = true;
         GridroomsContainer.AddObjectProperty("GridName", "Gridrooms");
         GridroomsContainer.AddObjectProperty("CmpContext", sPrefix);
         GridroomsContainer.AddObjectProperty("InMasterPage", "false");
         GridroomsContainer.AddObjectProperty("Class", "Grid");
         GridroomsContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
         GridroomsContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
         GridroomsContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridrooms_Backcolorstyle), 1, 0, ".", "")));
         GridroomsContainer.PageSize = subGridrooms_fnc_Recordsperpage( );
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            SubsflControlProps_962( ) ;
            /* Execute user event: Gridrooms.Load */
            E243T2 ();
            if ( ( subGridrooms_Islastpage == 0 ) && ( GRIDROOMS_nCurrentRecord > 0 ) && ( GRIDROOMS_nGridOutOfScope == 0 ) && ( nGXsfl_96_idx == 1 ) )
            {
               GRIDROOMS_nCurrentRecord = 0;
               GRIDROOMS_nGridOutOfScope = 1;
               subgridrooms_firstpage( ) ;
               /* Execute user event: Gridrooms.Load */
               E243T2 ();
            }
            wbEnd = 96;
            WB3T0( ) ;
         }
         bGXsfl_96_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes3T2( )
      {
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vOWNERALONE", AV30ownerAlone);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vOWNERALONE", GetSecureSignedToken( sPrefix, AV30ownerAlone, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vSELECTED", AV42selected);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vSELECTED", AV42selected);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vSELECTED", GetSecureSignedToken( sPrefix, AV42selected, context));
      }

      protected int subGridrooms_fnc_Pagecount( )
      {
         GRIDROOMS_nRecordCount = subGridrooms_fnc_Recordcount( );
         if ( ((long)((GRIDROOMS_nRecordCount) % (subGridrooms_fnc_Recordsperpage( )))) == 0 )
         {
            return (int)(NumberUtil.Int( (long)(Math.Round(GRIDROOMS_nRecordCount/ (decimal)(subGridrooms_fnc_Recordsperpage( )), 18, MidpointRounding.ToEven)))) ;
         }
         return (int)(NumberUtil.Int( (long)(Math.Round(GRIDROOMS_nRecordCount/ (decimal)(subGridrooms_fnc_Recordsperpage( )), 18, MidpointRounding.ToEven)))+1) ;
      }

      protected int subGridrooms_fnc_Recordcount( )
      {
         return AV7lines.Count ;
      }

      protected int subGridrooms_fnc_Recordsperpage( )
      {
         return (int)(10*1) ;
      }

      protected int subGridrooms_fnc_Currentpage( )
      {
         return (int)(NumberUtil.Int( (long)(Math.Round(GRIDROOMS_nFirstRecordOnPage/ (decimal)(subGridrooms_fnc_Recordsperpage( )), 18, MidpointRounding.ToEven)))+1) ;
      }

      protected short subgridrooms_firstpage( )
      {
         GRIDROOMS_nFirstRecordOnPage = 0;
         GxWebStd.gx_hidden_field( context, sPrefix+"GRIDROOMS_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRIDROOMS_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrGridrooms_refresh( subGridrooms_Rows, AV6groupId, AV30ownerAlone, AV43selectedRoomId, AV42selected, AV7lines, AV16sendAllCoins, sPrefix) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected short subgridrooms_nextpage( )
      {
         GRIDROOMS_nRecordCount = subGridrooms_fnc_Recordcount( );
         if ( ( GRIDROOMS_nRecordCount >= subGridrooms_fnc_Recordsperpage( ) ) && ( GRIDROOMS_nEOF == 0 ) )
         {
            GRIDROOMS_nFirstRecordOnPage = (long)(GRIDROOMS_nFirstRecordOnPage+subGridrooms_fnc_Recordsperpage( ));
         }
         else
         {
            return 2 ;
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"GRIDROOMS_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRIDROOMS_nFirstRecordOnPage), 15, 0, ".", "")));
         GridroomsContainer.AddObjectProperty("GRIDROOMS_nFirstRecordOnPage", GRIDROOMS_nFirstRecordOnPage);
         if ( isFullAjaxMode( ) )
         {
            gxgrGridrooms_refresh( subGridrooms_Rows, AV6groupId, AV30ownerAlone, AV43selectedRoomId, AV42selected, AV7lines, AV16sendAllCoins, sPrefix) ;
         }
         send_integrity_footer_hashes( ) ;
         return (short)(((GRIDROOMS_nEOF==0) ? 0 : 2)) ;
      }

      protected short subgridrooms_previouspage( )
      {
         if ( GRIDROOMS_nFirstRecordOnPage >= subGridrooms_fnc_Recordsperpage( ) )
         {
            GRIDROOMS_nFirstRecordOnPage = (long)(GRIDROOMS_nFirstRecordOnPage-subGridrooms_fnc_Recordsperpage( ));
         }
         else
         {
            return 2 ;
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"GRIDROOMS_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRIDROOMS_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrGridrooms_refresh( subGridrooms_Rows, AV6groupId, AV30ownerAlone, AV43selectedRoomId, AV42selected, AV7lines, AV16sendAllCoins, sPrefix) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected short subgridrooms_lastpage( )
      {
         GRIDROOMS_nRecordCount = subGridrooms_fnc_Recordcount( );
         if ( GRIDROOMS_nRecordCount > subGridrooms_fnc_Recordsperpage( ) )
         {
            if ( ((long)((GRIDROOMS_nRecordCount) % (subGridrooms_fnc_Recordsperpage( )))) == 0 )
            {
               GRIDROOMS_nFirstRecordOnPage = (long)(GRIDROOMS_nRecordCount-subGridrooms_fnc_Recordsperpage( ));
            }
            else
            {
               GRIDROOMS_nFirstRecordOnPage = (long)(GRIDROOMS_nRecordCount-((long)((GRIDROOMS_nRecordCount) % (subGridrooms_fnc_Recordsperpage( )))));
            }
         }
         else
         {
            GRIDROOMS_nFirstRecordOnPage = 0;
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"GRIDROOMS_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRIDROOMS_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrGridrooms_refresh( subGridrooms_Rows, AV6groupId, AV30ownerAlone, AV43selectedRoomId, AV42selected, AV7lines, AV16sendAllCoins, sPrefix) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected int subgridrooms_gotopage( int nPageNo )
      {
         if ( nPageNo > 0 )
         {
            GRIDROOMS_nFirstRecordOnPage = (long)(subGridrooms_fnc_Recordsperpage( )*(nPageNo-1));
         }
         else
         {
            GRIDROOMS_nFirstRecordOnPage = 0;
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"GRIDROOMS_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRIDROOMS_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrGridrooms_refresh( subGridrooms_Rows, AV6groupId, AV30ownerAlone, AV43selectedRoomId, AV42selected, AV7lines, AV16sendAllCoins, sPrefix) ;
         }
         send_integrity_footer_hashes( ) ;
         return (int)(0) ;
      }

      protected void before_start_formulas( )
      {
         edtavCtlroomid_Enabled = 0;
         edtavMark_Enabled = 0;
         edtavCtlcreated_Enabled = 0;
         edtavCtlopenedby_Enabled = 0;
         edtavCtldescription_Enabled = 0;
         edtavCtlamounttext_Enabled = 0;
         edtavCtlstateshort_Enabled = 0;
         edtavOpen_Enabled = 0;
         edtavChatlogview_Enabled = 0;
         AssignProp(sPrefix, false, edtavChatlogview_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavChatlogview_Enabled), 5, 0), true);
         fix_multi_value_controls( ) ;
      }

      protected void STRUP3T0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E223T2 ();
         context.wbGlbDoneStart = 1;
         nDoneStart = 1;
         /* After Start, stand alone formulas. */
         sXEvt = cgiGet( "_EventName");
         if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) )
         {
            /* Read saved SDTs. */
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"Lines"), AV7lines);
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"vLINES"), AV7lines);
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"vSELECTED"), AV42selected);
            /* Read saved values. */
            nRC_GXsfl_96 = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"nRC_GXsfl_96"), ".", ","), 18, MidpointRounding.ToEven));
            wcpOAV6groupId = StringUtil.StrToGuid( cgiGet( sPrefix+"wcpOAV6groupId"));
            AV13formOpen = StringUtil.StrToBool( cgiGet( sPrefix+"vFORMOPEN"));
            AssignAttri(sPrefix, false, "AV13formOpen", AV13formOpen);
            AV43selectedRoomId = StringUtil.StrToGuid( cgiGet( sPrefix+"vSELECTEDROOMID"));
            AV10signRoomId = StringUtil.StrToGuid( cgiGet( sPrefix+"vSIGNROOMID"));
            AV30ownerAlone = StringUtil.StrToBool( cgiGet( sPrefix+"vOWNERALONE"));
            AssignAttri(sPrefix, false, "AV30ownerAlone", AV30ownerAlone);
            GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vOWNERALONE", GetSecureSignedToken( sPrefix, AV30ownerAlone, context));
            GRIDROOMS_nFirstRecordOnPage = (long)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"GRIDROOMS_nFirstRecordOnPage"), ".", ","), 18, MidpointRounding.ToEven));
            GRIDROOMS_nEOF = (short)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"GRIDROOMS_nEOF"), ".", ","), 18, MidpointRounding.ToEven));
            nRC_GXsfl_96 = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"nRC_GXsfl_96"), ".", ","), 18, MidpointRounding.ToEven));
            nGXsfl_96_fel_idx = 0;
            while ( nGXsfl_96_fel_idx < nRC_GXsfl_96 )
            {
               nGXsfl_96_fel_idx = ((subGridrooms_Islastpage==1)&&(nGXsfl_96_fel_idx+1>subGridrooms_fnc_Recordsperpage( )) ? 1 : nGXsfl_96_fel_idx+1);
               sGXsfl_96_fel_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_96_fel_idx), 4, 0), 4, "0");
               SubsflControlProps_fel_962( ) ;
               AV46GXV1 = (int)(nGXsfl_96_fel_idx+GRIDROOMS_nFirstRecordOnPage);
               if ( ( AV7lines.Count >= AV46GXV1 ) && ( AV46GXV1 > 0 ) )
               {
                  AV7lines.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtMuSig2RoomLine)AV7lines.Item(AV46GXV1));
                  AV45mark = cgiGet( edtavMark_Internalname);
                  AV41open = cgiGet( edtavOpen_Internalname);
               }
            }
            if ( nGXsfl_96_fel_idx == 0 )
            {
               nGXsfl_96_idx = 1;
               sGXsfl_96_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_96_idx), 4, 0), 4, "0");
               SubsflControlProps_962( ) ;
            }
            nGXsfl_96_fel_idx = 1;
            /* Read variables values. */
            AV16sendAllCoins = StringUtil.StrToBool( cgiGet( chkavSendallcoins_Internalname));
            AssignAttri(sPrefix, false, "AV16sendAllCoins", AV16sendAllCoins);
            if ( ( ( context.localUtil.CToN( cgiGet( edtavSendcoins_Internalname), ".", ",") < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavSendcoins_Internalname), ".", ",") > 9999999.99999999m ) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vSENDCOINS");
               GX_FocusControl = edtavSendcoins_Internalname;
               AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV17sendCoins = 0;
               AssignAttri(sPrefix, false, "AV17sendCoins", StringUtil.LTrimStr( AV17sendCoins, 16, 8));
            }
            else
            {
               AV17sendCoins = context.localUtil.CToN( cgiGet( edtavSendcoins_Internalname), ".", ",");
               AssignAttri(sPrefix, false, "AV17sendCoins", StringUtil.LTrimStr( AV17sendCoins, 16, 8));
            }
            AV18sendTo = cgiGet( edtavSendto_Internalname);
            AssignAttri(sPrefix, false, "AV18sendTo", AV18sendTo);
            AV12description = cgiGet( edtavDescription_Internalname);
            AssignAttri(sPrefix, false, "AV12description", AV12description);
            cmbavUserfee.Name = cmbavUserfee_Internalname;
            cmbavUserfee.CurrentValue = cgiGet( cmbavUserfee_Internalname);
            AV33userFee = NumberUtil.Val( cgiGet( cmbavUserfee_Internalname), ".");
            AssignAttri(sPrefix, false, "AV33userFee", StringUtil.LTrimStr( AV33userFee, 16, 8));
            if ( ( ( context.localUtil.CToN( cgiGet( edtavManualfee_Internalname), ".", ",") < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavManualfee_Internalname), ".", ",") > 9999999.99999999m ) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vMANUALFEE");
               GX_FocusControl = edtavManualfee_Internalname;
               AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV29manualFee = 0;
               AssignAttri(sPrefix, false, "AV29manualFee", StringUtil.LTrimStr( AV29manualFee, 16, 8));
            }
            else
            {
               AV29manualFee = context.localUtil.CToN( cgiGet( edtavManualfee_Internalname), ".", ",");
               AssignAttri(sPrefix, false, "AV29manualFee", StringUtil.LTrimStr( AV29manualFee, 16, 8));
            }
            AV38ownerPassword = cgiGet( edtavOwnerpassword_Internalname);
            AssignAttri(sPrefix, false, "AV38ownerPassword", AV38ownerPassword);
            AV14password = cgiGet( edtavPassword_Internalname);
            AssignAttri(sPrefix, false, "AV14password", AV14password);
            AV39chatLogView = cgiGet( edtavChatlogview_Internalname);
            AssignAttri(sPrefix, false, "AV39chatLogView", AV39chatLogView);
            AV36chatText = cgiGet( edtavChattext_Internalname);
            AssignAttri(sPrefix, false, "AV36chatText", AV36chatText);
            /* Read subfile selected row values. */
            /* Read hidden variables. */
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
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
         E223T2 ();
         if (returnInSub) return;
      }

      protected void E223T2( )
      {
         /* Start Routine */
         returnInSub = false;
         new GeneXus.Programs.wallet.registered.ismusig2owneralone(context ).execute(  AV6groupId, out  AV30ownerAlone) ;
         AssignAttri(sPrefix, false, "AV30ownerAlone", AV30ownerAlone);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vOWNERALONE", GetSecureSignedToken( sPrefix, AV30ownerAlone, context));
         if ( AV30ownerAlone )
         {
            bttNewpayment_Caption = "Pay alone";
            AssignProp(sPrefix, false, bttNewpayment_Internalname, "Caption", bttNewpayment_Caption, true);
         }
         /* Execute user subroutine: 'SHOW LIST' */
         S112 ();
         if (returnInSub) return;
      }

      protected void E233T2( )
      {
         if ( gx_refresh_fired )
         {
            return  ;
         }
         gx_refresh_fired = true;
         /* Refresh Routine */
         returnInSub = false;
         new GeneXus.Programs.wallet.registered.getmusig2roomlines(context ).execute(  AV6groupId, out  AV7lines, out  AV5error) ;
         gx_BV96 = true;
         AssignAttri(sPrefix, false, "AV5error", AV5error);
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV5error)) )
         {
            this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"The payments can not be read",(string)AV5error}, true);
         }
         if ( AV30ownerAlone )
         {
            lblTbempty_Caption = "You are the owner: Pay alone sends a payment with your signature only. The members pay together, online at the same time, in the payment sessions below.";
            AssignProp(sPrefix, false, lblTbempty_Internalname, "Caption", lblTbempty_Caption, true);
         }
         else
         {
            lblTbempty_Caption = "Every payment of this group is a session signed online at the same time by the members, each from their own wallet wherever they are. New payment opens one; select a session to join it, chat and sign.";
            AssignProp(sPrefix, false, lblTbempty_Internalname, "Caption", lblTbempty_Caption, true);
         }
         AV40found = false;
         AV53GXV8 = 1;
         while ( AV53GXV8 <= AV7lines.Count )
         {
            AV37oneLine = ((GeneXus.Programs.wallet.registered.SdtMuSig2RoomLine)AV7lines.Item(AV53GXV8));
            if ( AV37oneLine.gxTpr_Roomid == AV43selectedRoomId )
            {
               AV42selected = AV37oneLine;
               AV40found = true;
               if (true) break;
            }
            AV53GXV8 = (int)(AV53GXV8+1);
         }
         if ( ! AV40found )
         {
            AV54GXV9 = 1;
            while ( AV54GXV9 <= AV7lines.Count )
            {
               AV37oneLine = ((GeneXus.Programs.wallet.registered.SdtMuSig2RoomLine)AV7lines.Item(AV54GXV9));
               if ( AV37oneLine.gxTpr_Isopen )
               {
                  AV42selected = AV37oneLine;
                  AV40found = true;
                  if (true) break;
               }
               AV54GXV9 = (int)(AV54GXV9+1);
            }
         }
         if ( ! AV40found && ( AV7lines.Count > 0 ) )
         {
            AV42selected = ((GeneXus.Programs.wallet.registered.SdtMuSig2RoomLine)AV7lines.Item(1));
            AV40found = true;
         }
         if ( AV40found )
         {
            AV43selectedRoomId = AV42selected.gxTpr_Roomid;
            AssignAttri(sPrefix, false, "AV43selectedRoomId", AV43selectedRoomId.ToString());
            divRoomtable_Visible = 1;
            AssignProp(sPrefix, false, divRoomtable_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divRoomtable_Visible), 5, 0), true);
            lblTbroomtitle_Caption = "Payment session: "+StringUtil.Trim( AV42selected.gxTpr_Description)+" - "+StringUtil.Trim( AV42selected.gxTpr_Amounttext)+" to "+StringUtil.Trim( AV42selected.gxTpr_Sendto)+" (opened by "+StringUtil.Trim( AV42selected.gxTpr_Openedby)+", "+StringUtil.Trim( AV42selected.gxTpr_Created)+")";
            AssignProp(sPrefix, false, lblTbroomtitle_Internalname, "Caption", lblTbroomtitle_Caption, true);
            lblTbroomstate_Caption = AV42selected.gxTpr_Statetext;
            AssignProp(sPrefix, false, lblTbroomstate_Internalname, "Caption", lblTbroomstate_Caption, true);
            lblTbroompeople_Caption = AV42selected.gxTpr_People;
            AssignProp(sPrefix, false, lblTbroompeople_Internalname, "Caption", lblTbroompeople_Caption, true);
            lblTbroomwaiting_Caption = AV42selected.gxTpr_Waiting;
            AssignProp(sPrefix, false, lblTbroomwaiting_Internalname, "Caption", lblTbroomwaiting_Caption, true);
            bttHere_Visible = (AV42selected.gxTpr_Canhere ? 1 : 0);
            AssignProp(sPrefix, false, bttHere_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttHere_Visible), 5, 0), true);
            bttSignnow_Visible = (AV42selected.gxTpr_Cansignnow ? 1 : 0);
            AssignProp(sPrefix, false, bttSignnow_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttSignnow_Visible), 5, 0), true);
            bttSign_Visible = (AV42selected.gxTpr_Cansign ? 1 : 0);
            AssignProp(sPrefix, false, bttSign_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttSign_Visible), 5, 0), true);
            bttAbort_Visible = (AV42selected.gxTpr_Canabort ? 1 : 0);
            AssignProp(sPrefix, false, bttAbort_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttAbort_Visible), 5, 0), true);
            AV39chatLogView = AV42selected.gxTpr_Chatlog;
            AssignAttri(sPrefix, false, "AV39chatLogView", AV39chatLogView);
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV42selected.gxTpr_Chatlog)) )
            {
               AV39chatLogView = "(no messages yet)";
               AssignAttri(sPrefix, false, "AV39chatLogView", AV39chatLogView);
            }
            tblChatinputtable_Visible = (AV42selected.gxTpr_Isopen ? 1 : 0);
            AssignProp(sPrefix, false, tblChatinputtable_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(tblChatinputtable_Visible), 5, 0), true);
         }
         else
         {
            divRoomtable_Visible = 0;
            AssignProp(sPrefix, false, divRoomtable_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divRoomtable_Visible), 5, 0), true);
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV7lines", AV7lines);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV42selected", AV42selected);
      }

      private void E243T2( )
      {
         /* Gridrooms_Load Routine */
         returnInSub = false;
         AV46GXV1 = 1;
         while ( AV46GXV1 <= AV7lines.Count )
         {
            AV7lines.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtMuSig2RoomLine)AV7lines.Item(AV46GXV1));
            if ( ((GeneXus.Programs.wallet.registered.SdtMuSig2RoomLine)(AV7lines.CurrentItem)).gxTpr_Roomid == AV43selectedRoomId )
            {
               AV45mark = "?";
               AssignAttri(sPrefix, false, edtavMark_Internalname, AV45mark);
               AV41open = "";
               AssignAttri(sPrefix, false, edtavOpen_Internalname, AV41open);
            }
            else
            {
               AV45mark = "";
               AssignAttri(sPrefix, false, edtavMark_Internalname, AV45mark);
               AV41open = "View";
               AssignAttri(sPrefix, false, edtavOpen_Internalname, AV41open);
            }
            /* Load Method */
            if ( wbStart != -1 )
            {
               wbStart = 96;
            }
            if ( ( subGridrooms_Islastpage == 1 ) || ( 10 == 0 ) || ( ( GRIDROOMS_nCurrentRecord >= GRIDROOMS_nFirstRecordOnPage ) && ( GRIDROOMS_nCurrentRecord < GRIDROOMS_nFirstRecordOnPage + subGridrooms_fnc_Recordsperpage( ) ) ) )
            {
               sendrow_962( ) ;
            }
            GRIDROOMS_nEOF = (short)(((subGridrooms_Rows==0)||(GRIDROOMS_nCurrentRecord<GRIDROOMS_nFirstRecordOnPage+subGridrooms_fnc_Recordsperpage( )) ? 1 : 0));
            GxWebStd.gx_hidden_field( context, sPrefix+"GRIDROOMS_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRIDROOMS_nEOF), 1, 0, ".", "")));
            GRIDROOMS_nCurrentRecord = (long)(GRIDROOMS_nCurrentRecord+1);
            if ( isFullAjaxMode( ) && ! bGXsfl_96_Refreshing )
            {
               DoAjaxLoad(96, GridroomsRow);
            }
            AV46GXV1 = (int)(AV46GXV1+1);
         }
         /*  Sending Event outputs  */
      }

      protected void E253T2( )
      {
         AV46GXV1 = (int)(nGXsfl_96_idx+GRIDROOMS_nFirstRecordOnPage);
         if ( ( AV46GXV1 > 0 ) && ( AV7lines.Count >= AV46GXV1 ) )
         {
            AV7lines.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtMuSig2RoomLine)AV7lines.Item(AV46GXV1));
         }
         /* 'Open' Routine */
         returnInSub = false;
         AV43selectedRoomId = ((GeneXus.Programs.wallet.registered.SdtMuSig2RoomLine)(AV7lines.CurrentItem)).gxTpr_Roomid;
         AssignAttri(sPrefix, false, "AV43selectedRoomId", AV43selectedRoomId.ToString());
         context.DoAjaxRefreshCmp(sPrefix);
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV7lines", AV7lines);
         nGXsfl_96_bak_idx = nGXsfl_96_idx;
         gxgrGridrooms_refresh( subGridrooms_Rows, AV6groupId, AV30ownerAlone, AV43selectedRoomId, AV42selected, AV7lines, AV16sendAllCoins, sPrefix) ;
         nGXsfl_96_idx = nGXsfl_96_bak_idx;
         sGXsfl_96_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_96_idx), 4, 0), 4, "0");
         SubsflControlProps_962( ) ;
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV42selected", AV42selected);
      }

      protected void E113T2( )
      {
         /* 'Open room' Routine */
         returnInSub = false;
         /* Execute user subroutine: 'CHECK PAYMENT' */
         S132 ();
         if (returnInSub) return;
         if ( AV22checked )
         {
            new GeneXus.Programs.wallet.registered.startmusig2room(context ).execute(  AV6groupId,  AV16sendAllCoins,  AV17sendCoins,  AV18sendTo,  AV12description, out  AV15roomId, out  AV5error) ;
            AssignAttri(sPrefix, false, "AV5error", AV5error);
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV5error)) )
            {
               AV43selectedRoomId = AV15roomId;
               AssignAttri(sPrefix, false, "AV43selectedRoomId", AV43selectedRoomId.ToString());
               this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"success",(string)"Payment session opened",(string)"The members see it in their own wallet for 15 minutes"}, true);
               /* Execute user subroutine: 'SHOW LIST' */
               S112 ();
               if (returnInSub) return;
            }
            else
            {
               this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"The payment session could not be opened",(string)AV5error}, true);
            }
         }
         /*  Sending Event outputs  */
         if ( gx_BV96 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV7lines", AV7lines);
            nGXsfl_96_bak_idx = nGXsfl_96_idx;
            gxgrGridrooms_refresh( subGridrooms_Rows, AV6groupId, AV30ownerAlone, AV43selectedRoomId, AV42selected, AV7lines, AV16sendAllCoins, sPrefix) ;
            nGXsfl_96_idx = nGXsfl_96_bak_idx;
            sGXsfl_96_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_96_idx), 4, 0), 4, "0");
            SubsflControlProps_962( ) ;
         }
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV42selected", AV42selected);
      }

      protected void E123T2( )
      {
         /* 'Estimate fee' Routine */
         returnInSub = false;
         /* Execute user subroutine: 'CHECK PAYMENT' */
         S132 ();
         if (returnInSub) return;
         if ( AV22checked )
         {
            new GeneXus.Programs.wallet.registered.estimatemusig2vsize(context ).execute(  AV16sendAllCoins,  AV17sendCoins,  AV18sendTo,  AV12description, out  AV34virtualSize, out  AV5error) ;
            AssignAttri(sPrefix, false, "AV5error", AV5error);
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV5error)) )
            {
               GXt_char1 = AV5error;
               GXt_int2 = (short)(AV23economicalBlocks);
               new GeneXus.Programs.wallet.getestimatesmartfee(context ).execute(  AV34virtualSize,  60,  "economical", out  AV24economicalFee, out  GXt_int2, out  GXt_char1) ;
               AV23economicalBlocks = GXt_int2;
               AssignAttri(sPrefix, false, "AV24economicalFee", StringUtil.LTrimStr( AV24economicalFee, 16, 8));
               AssignAttri(sPrefix, false, "AV23economicalBlocks", StringUtil.LTrimStr( (decimal)(AV23economicalBlocks), 10, 0));
               AV5error = GXt_char1;
               AssignAttri(sPrefix, false, "AV5error", AV5error);
            }
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV5error)) )
            {
               GXt_char1 = AV5error;
               GXt_int2 = (short)(AV31standarBlocks);
               new GeneXus.Programs.wallet.getestimatesmartfee(context ).execute(  AV34virtualSize,  6,  "conservative", out  AV32standardFee, out  GXt_int2, out  GXt_char1) ;
               AV31standarBlocks = GXt_int2;
               AssignAttri(sPrefix, false, "AV32standardFee", StringUtil.LTrimStr( AV32standardFee, 16, 8));
               AssignAttri(sPrefix, false, "AV31standarBlocks", StringUtil.LTrimStr( (decimal)(AV31standarBlocks), 10, 0));
               AV5error = GXt_char1;
               AssignAttri(sPrefix, false, "AV5error", AV5error);
            }
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV5error)) )
            {
               GXt_char1 = AV5error;
               GXt_int2 = (short)(AV26fastestBlocks);
               new GeneXus.Programs.wallet.getestimatesmartfee(context ).execute(  AV34virtualSize,  1,  "conservative", out  AV27fastestFee, out  GXt_int2, out  GXt_char1) ;
               AV26fastestBlocks = GXt_int2;
               AssignAttri(sPrefix, false, "AV27fastestFee", StringUtil.LTrimStr( AV27fastestFee, 16, 8));
               AssignAttri(sPrefix, false, "AV26fastestBlocks", StringUtil.LTrimStr( (decimal)(AV26fastestBlocks), 10, 0));
               AV5error = GXt_char1;
               AssignAttri(sPrefix, false, "AV5error", AV5error);
            }
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV5error)) )
            {
               this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"The fee could not be calculated",(string)AV5error}, true);
            }
            else
            {
               cmbavUserfee.addItem(StringUtil.Trim( StringUtil.Str( (decimal)(0), 16, 8)), "Select Estimated Transaction Fee", 0);
               cmbavUserfee.addItem(StringUtil.Trim( StringUtil.Str( AV24economicalFee, 16, 8)), StringUtil.Trim( StringUtil.Str( AV24economicalFee, 16, 8))+" in about "+StringUtil.Trim( StringUtil.Str( (decimal)(AV23economicalBlocks), 10, 0))+" Blocks", 0);
               cmbavUserfee.addItem(StringUtil.Trim( StringUtil.Str( AV32standardFee, 16, 8)), StringUtil.Trim( StringUtil.Str( AV32standardFee, 16, 8))+" in about "+StringUtil.Trim( StringUtil.Str( (decimal)(AV31standarBlocks), 10, 0))+" Blocks", 0);
               cmbavUserfee.addItem(StringUtil.Trim( StringUtil.Str( AV27fastestFee, 16, 8)), StringUtil.Trim( StringUtil.Str( AV27fastestFee, 16, 8))+" in about "+StringUtil.Trim( StringUtil.Str( (decimal)(AV26fastestBlocks), 10, 0))+" Blocks", 0);
               divOwnerfeetable_Visible = 1;
               AssignProp(sPrefix, false, divOwnerfeetable_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divOwnerfeetable_Visible), 5, 0), true);
               bttEstimatefee_Visible = 0;
               AssignProp(sPrefix, false, bttEstimatefee_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttEstimatefee_Visible), 5, 0), true);
            }
         }
         /*  Sending Event outputs  */
         cmbavUserfee.CurrentValue = StringUtil.Trim( StringUtil.Str( AV33userFee, 16, 8));
         AssignProp(sPrefix, false, cmbavUserfee_Internalname, "Values", cmbavUserfee.ToJavascriptSource(), true);
      }

      protected void E133T2( )
      {
         /* 'Pay alone' Routine */
         returnInSub = false;
         if ( ( AV29manualFee <= Convert.ToDecimal( 0 )) )
         {
            this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"warning",(string)"Please select an Estimated Transaction Fee to pay",(string)""}, true);
         }
         else
         {
            GXt_int2 = AV28groupType;
            new GeneXus.Programs.wallet.registered.setgroupeditwallet(context ).execute(  AV6groupId, out  GXt_int2) ;
            AV28groupType = GXt_int2;
            new GeneXus.Programs.wallet.cleanprivatekeys(context ).execute( ) ;
            GXt_char1 = AV5error;
            new GeneXus.Programs.wallet.approvespendingkeys(context ).execute(  AV38ownerPassword, out  GXt_char1) ;
            AV5error = GXt_char1;
            AssignAttri(sPrefix, false, "AV5error", AV5error);
            AV38ownerPassword = "";
            AssignAttri(sPrefix, false, "AV38ownerPassword", AV38ownerPassword);
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV5error)) )
            {
               new GeneXus.Programs.wallet.registered.ownerspendmusig2(context ).execute(  AV16sendAllCoins,  AV17sendCoins,  AV18sendTo,  AV12description,  AV29manualFee, out  AV21broadcast, out  AV25errorTitle, out  AV5error) ;
               AssignAttri(sPrefix, false, "AV21broadcast", AV21broadcast);
               AssignAttri(sPrefix, false, "AV25errorTitle", AV25errorTitle);
               AssignAttri(sPrefix, false, "AV5error", AV5error);
            }
            new GeneXus.Programs.wallet.cleanprivatekeys(context ).execute( ) ;
            if ( AV21broadcast )
            {
               this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"success",(string)"Transaction broadcast",(string)"The transaction was signed and submitted to the network"}, true);
               /* Execute user subroutine: 'SHOW LIST' */
               S112 ();
               if (returnInSub) return;
            }
            else
            {
               this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"Not sent: "+AV25errorTitle,(string)AV5error}, true);
            }
         }
         /*  Sending Event outputs  */
         if ( gx_BV96 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV7lines", AV7lines);
            nGXsfl_96_bak_idx = nGXsfl_96_idx;
            gxgrGridrooms_refresh( subGridrooms_Rows, AV6groupId, AV30ownerAlone, AV43selectedRoomId, AV42selected, AV7lines, AV16sendAllCoins, sPrefix) ;
            nGXsfl_96_idx = nGXsfl_96_bak_idx;
            sGXsfl_96_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_96_idx), 4, 0), 4, "0");
            SubsflControlProps_962( ) ;
         }
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV42selected", AV42selected);
      }

      protected void E143T2( )
      {
         /* 'Refresh rooms' Routine */
         returnInSub = false;
         context.DoAjaxRefreshCmp(sPrefix);
         /*  Sending Event outputs  */
         if ( gx_BV96 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV7lines", AV7lines);
            nGXsfl_96_bak_idx = nGXsfl_96_idx;
            gxgrGridrooms_refresh( subGridrooms_Rows, AV6groupId, AV30ownerAlone, AV43selectedRoomId, AV42selected, AV7lines, AV16sendAllCoins, sPrefix) ;
            nGXsfl_96_idx = nGXsfl_96_bak_idx;
            sGXsfl_96_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_96_idx), 4, 0), 4, "0");
            SubsflControlProps_962( ) ;
         }
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV42selected", AV42selected);
      }

      protected void E153T2( )
      {
         /* 'Here' Routine */
         returnInSub = false;
         new GeneXus.Programs.wallet.registered.musig2roomhere(context ).execute(  AV6groupId,  AV43selectedRoomId, out  AV5error) ;
         AssignAttri(sPrefix, false, "AV5error", AV5error);
         /* Execute user subroutine: 'SHOW ERROR' */
         S142 ();
         if (returnInSub) return;
         context.DoAjaxRefreshCmp(sPrefix);
         /*  Sending Event outputs  */
         if ( gx_BV96 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV7lines", AV7lines);
            nGXsfl_96_bak_idx = nGXsfl_96_idx;
            gxgrGridrooms_refresh( subGridrooms_Rows, AV6groupId, AV30ownerAlone, AV43selectedRoomId, AV42selected, AV7lines, AV16sendAllCoins, sPrefix) ;
            nGXsfl_96_idx = nGXsfl_96_bak_idx;
            sGXsfl_96_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_96_idx), 4, 0), 4, "0");
            SubsflControlProps_962( ) ;
         }
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV42selected", AV42selected);
      }

      protected void E163T2( )
      {
         /* 'Sign now' Routine */
         returnInSub = false;
         new GeneXus.Programs.wallet.registered.musig2roomsignnow(context ).execute(  AV6groupId,  AV43selectedRoomId, out  AV5error) ;
         AssignAttri(sPrefix, false, "AV5error", AV5error);
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV5error)) )
         {
            this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"success",(string)"Signing started",(string)"Every signer can now press Sign with password: 3 minutes"}, true);
         }
         /* Execute user subroutine: 'SHOW ERROR' */
         S142 ();
         if (returnInSub) return;
         context.DoAjaxRefreshCmp(sPrefix);
         /*  Sending Event outputs  */
         if ( gx_BV96 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV7lines", AV7lines);
            nGXsfl_96_bak_idx = nGXsfl_96_idx;
            gxgrGridrooms_refresh( subGridrooms_Rows, AV6groupId, AV30ownerAlone, AV43selectedRoomId, AV42selected, AV7lines, AV16sendAllCoins, sPrefix) ;
            nGXsfl_96_idx = nGXsfl_96_bak_idx;
            sGXsfl_96_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_96_idx), 4, 0), 4, "0");
            SubsflControlProps_962( ) ;
         }
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV42selected", AV42selected);
      }

      protected void E173T2( )
      {
         /* 'Confirm sign' Routine */
         returnInSub = false;
         new GeneXus.Programs.wallet.cleanprivatekeys(context ).execute( ) ;
         GXt_char1 = AV5error;
         new GeneXus.Programs.wallet.approvespendingkeys(context ).execute(  AV14password, out  GXt_char1) ;
         AV5error = GXt_char1;
         AssignAttri(sPrefix, false, "AV5error", AV5error);
         AV14password = "";
         AssignAttri(sPrefix, false, "AV14password", AV14password);
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV5error)) )
         {
            new GeneXus.Programs.wallet.registered.musig2roomsign(context ).execute(  AV6groupId,  AV10signRoomId, out  AV44txId, out  AV5error) ;
            AssignAttri(sPrefix, false, "AV5error", AV5error);
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV5error)) )
            {
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV44txId)) )
               {
                  this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"success",(string)"Signature sent","Your signature was sent to the other signers. The payment is sent to the network with the last signature"}, true);
               }
               else
               {
                  this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"success",(string)"Transaction broadcast","The payment was signed by every signer and submitted to the network: "+StringUtil.Trim( AV44txId)}, true);
               }
            }
         }
         new GeneXus.Programs.wallet.cleanprivatekeys(context ).execute( ) ;
         /* Execute user subroutine: 'SHOW ERROR' */
         S142 ();
         if (returnInSub) return;
         /* Execute user subroutine: 'SHOW LIST' */
         S112 ();
         if (returnInSub) return;
         /*  Sending Event outputs  */
         if ( gx_BV96 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV7lines", AV7lines);
            nGXsfl_96_bak_idx = nGXsfl_96_idx;
            gxgrGridrooms_refresh( subGridrooms_Rows, AV6groupId, AV30ownerAlone, AV43selectedRoomId, AV42selected, AV7lines, AV16sendAllCoins, sPrefix) ;
            nGXsfl_96_idx = nGXsfl_96_bak_idx;
            sGXsfl_96_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_96_idx), 4, 0), 4, "0");
            SubsflControlProps_962( ) ;
         }
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV42selected", AV42selected);
      }

      protected void E183T2( )
      {
         /* 'Cancel form' Routine */
         returnInSub = false;
         AV14password = "";
         AssignAttri(sPrefix, false, "AV14password", AV14password);
         AV38ownerPassword = "";
         AssignAttri(sPrefix, false, "AV38ownerPassword", AV38ownerPassword);
         /* Execute user subroutine: 'SHOW LIST' */
         S112 ();
         if (returnInSub) return;
         /*  Sending Event outputs  */
         if ( gx_BV96 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV7lines", AV7lines);
            nGXsfl_96_bak_idx = nGXsfl_96_idx;
            gxgrGridrooms_refresh( subGridrooms_Rows, AV6groupId, AV30ownerAlone, AV43selectedRoomId, AV42selected, AV7lines, AV16sendAllCoins, sPrefix) ;
            nGXsfl_96_idx = nGXsfl_96_bak_idx;
            sGXsfl_96_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_96_idx), 4, 0), 4, "0");
            SubsflControlProps_962( ) ;
         }
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV42selected", AV42selected);
      }

      protected void E193T2( )
      {
         /* 'Leave' Routine */
         returnInSub = false;
         new GeneXus.Programs.wallet.registered.musig2roomabort(context ).execute(  AV6groupId,  AV43selectedRoomId,  "left the payment session", out  AV5error) ;
         AssignAttri(sPrefix, false, "AV5error", AV5error);
         /* Execute user subroutine: 'SHOW ERROR' */
         S142 ();
         if (returnInSub) return;
         context.DoAjaxRefreshCmp(sPrefix);
         /*  Sending Event outputs  */
         if ( gx_BV96 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV7lines", AV7lines);
            nGXsfl_96_bak_idx = nGXsfl_96_idx;
            gxgrGridrooms_refresh( subGridrooms_Rows, AV6groupId, AV30ownerAlone, AV43selectedRoomId, AV42selected, AV7lines, AV16sendAllCoins, sPrefix) ;
            nGXsfl_96_idx = nGXsfl_96_bak_idx;
            sGXsfl_96_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_96_idx), 4, 0), 4, "0");
            SubsflControlProps_962( ) ;
         }
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV42selected", AV42selected);
      }

      protected void E203T2( )
      {
         /* 'Send chat' Routine */
         returnInSub = false;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV36chatText))) )
         {
            new GeneXus.Programs.wallet.registered.musig2roomchat(context ).execute(  AV6groupId,  AV43selectedRoomId,  AV36chatText, out  AV5error) ;
            AssignAttri(sPrefix, false, "AV5error", AV5error);
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV5error)) )
            {
               AV36chatText = "";
               AssignAttri(sPrefix, false, "AV36chatText", AV36chatText);
            }
            /* Execute user subroutine: 'SHOW ERROR' */
            S142 ();
            if (returnInSub) return;
            context.DoAjaxRefreshCmp(sPrefix);
         }
         /*  Sending Event outputs  */
         if ( gx_BV96 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV7lines", AV7lines);
            nGXsfl_96_bak_idx = nGXsfl_96_idx;
            gxgrGridrooms_refresh( subGridrooms_Rows, AV6groupId, AV30ownerAlone, AV43selectedRoomId, AV42selected, AV7lines, AV16sendAllCoins, sPrefix) ;
            nGXsfl_96_idx = nGXsfl_96_bak_idx;
            sGXsfl_96_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_96_idx), 4, 0), 4, "0");
            SubsflControlProps_962( ) ;
         }
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV42selected", AV42selected);
      }

      protected void E213T2( )
      {
         /* GlobalEvents_Receivechat Routine */
         returnInSub = false;
         if ( ! AV13formOpen )
         {
            context.DoAjaxRefreshCmp(sPrefix);
         }
         /*  Sending Event outputs  */
         if ( gx_BV96 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV7lines", AV7lines);
            nGXsfl_96_bak_idx = nGXsfl_96_idx;
            gxgrGridrooms_refresh( subGridrooms_Rows, AV6groupId, AV30ownerAlone, AV43selectedRoomId, AV42selected, AV7lines, AV16sendAllCoins, sPrefix) ;
            nGXsfl_96_idx = nGXsfl_96_bak_idx;
            sGXsfl_96_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_96_idx), 4, 0), 4, "0");
            SubsflControlProps_962( ) ;
         }
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV42selected", AV42selected);
      }

      protected void S122( )
      {
         /* 'SHOW FORM' Routine */
         returnInSub = false;
         AV13formOpen = true;
         AssignAttri(sPrefix, false, "AV13formOpen", AV13formOpen);
         divListtable_Visible = 0;
         AssignProp(sPrefix, false, divListtable_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divListtable_Visible), 5, 0), true);
         this.executeExternalObjectMethod(sPrefix, false, "gx.extensions.web.interop", "runJS", new Object[] {"window.scrollTo(0,0);"}, false);
      }

      protected void S112( )
      {
         /* 'SHOW LIST' Routine */
         returnInSub = false;
         AV13formOpen = false;
         AssignAttri(sPrefix, false, "AV13formOpen", AV13formOpen);
         divNewtable_Visible = 0;
         AssignProp(sPrefix, false, divNewtable_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divNewtable_Visible), 5, 0), true);
         divSigntable_Visible = 0;
         AssignProp(sPrefix, false, divSigntable_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divSigntable_Visible), 5, 0), true);
         divListtable_Visible = 1;
         AssignProp(sPrefix, false, divListtable_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divListtable_Visible), 5, 0), true);
         context.DoAjaxRefreshCmp(sPrefix);
      }

      protected void S132( )
      {
         /* 'CHECK PAYMENT' Routine */
         returnInSub = false;
         AV22checked = false;
         AssignAttri(sPrefix, false, "AV22checked", AV22checked);
         GXt_SdtWalletInfo3 = AV20walletInfo;
         new GeneXus.Programs.wallet.getwalletinfo(context ).execute( out  GXt_SdtWalletInfo3) ;
         AV20walletInfo = GXt_SdtWalletInfo3;
         GXt_int2 = AV28groupType;
         new GeneXus.Programs.wallet.registered.setgroupeditwallet(context ).execute(  AV6groupId, out  GXt_int2) ;
         AV28groupType = GXt_int2;
         GXt_decimal4 = AV19totalBalance;
         new GeneXus.Programs.wallet.getbalancefromhistorywithbalance(context ).execute( out  GXt_decimal4) ;
         AV19totalBalance = GXt_decimal4;
         if ( AV16sendAllCoins )
         {
            AV17sendCoins = AV19totalBalance;
            AssignAttri(sPrefix, false, "AV17sendCoins", StringUtil.LTrimStr( AV17sendCoins, 16, 8));
         }
         if ( ( AV19totalBalance <= Convert.ToDecimal( 0 )) )
         {
            this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"warning",(string)"The group has no balance in your wallet yet",(string)"Press Rescan Balance in the Wallet Balance tab of the group first: the one who starts a payment chooses its coins"}, true);
         }
         else if ( ( (Convert.ToDecimal(0)==AV17sendCoins) || ( AV17sendCoins <= Convert.ToDecimal( 0 )) ) && ! AV16sendAllCoins )
         {
            this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"warning",(string)"You have to select an amount to send",(string)""}, true);
         }
         else if ( ( AV17sendCoins >= AV19totalBalance ) && ! AV16sendAllCoins )
         {
            this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"warning",(string)"You don't have enough balance",(string)""}, true);
         }
         else if ( String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV12description))) )
         {
            this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"You have to enter a description",(string)"The description is mandatory on multisignature transactions"}, true);
         }
         else
         {
            GXt_char1 = AV5error;
            new GeneXus.Programs.nbitcoin.isaddressvalid(context ).execute(  AV18sendTo,  AV20walletInfo.gxTpr_Networktype, out  GXt_char1) ;
            AV5error = GXt_char1;
            AssignAttri(sPrefix, false, "AV5error", AV5error);
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV5error)) )
            {
               this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"Please check the Send to address: ",(string)AV5error}, true);
            }
            else
            {
               AV22checked = true;
               AssignAttri(sPrefix, false, "AV22checked", AV22checked);
            }
         }
      }

      protected void S142( )
      {
         /* 'SHOW ERROR' Routine */
         returnInSub = false;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV5error)) )
         {
            this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"Payment session",(string)AV5error}, true);
         }
      }

      protected void wb_table5_139_3T2( bool wbgen )
      {
         if ( wbgen )
         {
            /* Table start */
            sStyleString = "";
            if ( tblChatinputtable_Visible == 0 )
            {
               sStyleString += "display:none;";
            }
            GxWebStd.gx_table_start( context, tblChatinputtable_Internalname, tblChatinputtable_Internalname, "", "Table", 0, "", "", 1, 2, sStyleString, "", "", 0);
            context.WriteHtmlText( "<tr>") ;
            context.WriteHtmlText( "<td>") ;
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group gx-default-form-group", "start", "top", ""+" data-gx-for=\""+edtavChattext_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavChattext_Internalname, "Message", "gx-form-item AttributeLabel", 1, true, "width: 25%;");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 75, "%", 0, "px", "gx-form-item gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 144,'" + sPrefix + "',false,'" + sGXsfl_96_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavChattext_Internalname, StringUtil.RTrim( AV36chatText), StringUtil.RTrim( context.localUtil.Format( AV36chatText, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,144);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavChattext_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavChattext_Enabled, 0, "text", "", 80, "chr", 1, "row", 100, 0, 0, 0, 0, -1, -1, true, "", "start", true, "", "HLP_Wallet/registered/MuSig2RoomsWC.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</td>") ;
            context.WriteHtmlText( "<td>") ;
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 146,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttSendchat_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(96), 2, 0)+","+"null"+");", "Send", bttSendchat_Jsonclick, 5, "Send", "", StyleString, ClassString, 1, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'SEND CHAT\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/MuSig2RoomsWC.htm");
            context.WriteHtmlText( "</td>") ;
            context.WriteHtmlText( "</tr>") ;
            /* End of table */
            context.WriteHtmlText( "</table>") ;
            wb_table5_139_3T2e( true) ;
         }
         else
         {
            wb_table5_139_3T2e( false) ;
         }
      }

      protected void wb_table4_122_3T2( bool wbgen )
      {
         if ( wbgen )
         {
            /* Table start */
            sStyleString = "";
            GxWebStd.gx_table_start( context, tblRoombuttons_Internalname, tblRoombuttons_Internalname, "", "Table", 0, "", "", 1, 2, sStyleString, "", "", 0);
            context.WriteHtmlText( "<tr>") ;
            context.WriteHtmlText( "<td>") ;
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 125,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttHere_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(96), 2, 0)+","+"null"+");", "I'm here", bttHere_Jsonclick, 5, "I'm here", "", StyleString, ClassString, bttHere_Visible, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'HERE\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/MuSig2RoomsWC.htm");
            context.WriteHtmlText( "</td>") ;
            context.WriteHtmlText( "<td>") ;
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 127,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttSignnow_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(96), 2, 0)+","+"null"+");", "Start signing", bttSignnow_Jsonclick, 5, "Start signing", "", StyleString, ClassString, bttSignnow_Visible, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'SIGN NOW\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/MuSig2RoomsWC.htm");
            context.WriteHtmlText( "</td>") ;
            context.WriteHtmlText( "<td>") ;
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 129,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttSign_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(96), 2, 0)+","+"null"+");", "Sign with password", bttSign_Jsonclick, 7, "Sign with password", "", StyleString, ClassString, bttSign_Visible, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+"e263t1_client"+"'", TempTags, "", 2, "HLP_Wallet/registered/MuSig2RoomsWC.htm");
            context.WriteHtmlText( "</td>") ;
            context.WriteHtmlText( "<td>") ;
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 131,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttAbort_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(96), 2, 0)+","+"null"+");", "Leave", bttAbort_Jsonclick, 5, "Leave", "", StyleString, ClassString, bttAbort_Visible, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'LEAVE\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/MuSig2RoomsWC.htm");
            context.WriteHtmlText( "</td>") ;
            context.WriteHtmlText( "</tr>") ;
            /* End of table */
            context.WriteHtmlText( "</table>") ;
            wb_table4_122_3T2e( true) ;
         }
         else
         {
            wb_table4_122_3T2e( false) ;
         }
      }

      protected void wb_table3_88_3T2( bool wbgen )
      {
         if ( wbgen )
         {
            /* Table start */
            sStyleString = "";
            GxWebStd.gx_table_start( context, tblButtonstable_Internalname, tblButtonstable_Internalname, "", "Table", 0, "", "", 1, 2, sStyleString, "", "", 0);
            context.WriteHtmlText( "<tr>") ;
            context.WriteHtmlText( "<td>") ;
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 91,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttNewpayment_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(96), 2, 0)+","+"null"+");", bttNewpayment_Caption, bttNewpayment_Jsonclick, 7, "New payment", "", StyleString, ClassString, 1, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+"e273t1_client"+"'", TempTags, "", 2, "HLP_Wallet/registered/MuSig2RoomsWC.htm");
            context.WriteHtmlText( "</td>") ;
            context.WriteHtmlText( "<td>") ;
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 93,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttRefreshrooms_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(96), 2, 0)+","+"null"+");", "Refresh", bttRefreshrooms_Jsonclick, 5, "Refresh", "", StyleString, ClassString, 1, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'REFRESH ROOMS\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/MuSig2RoomsWC.htm");
            context.WriteHtmlText( "</td>") ;
            context.WriteHtmlText( "</tr>") ;
            /* End of table */
            context.WriteHtmlText( "</table>") ;
            wb_table3_88_3T2e( true) ;
         }
         else
         {
            wb_table3_88_3T2e( false) ;
         }
      }

      protected void wb_table2_74_3T2( bool wbgen )
      {
         if ( wbgen )
         {
            /* Table start */
            sStyleString = "";
            GxWebStd.gx_table_start( context, tblSignbuttons_Internalname, tblSignbuttons_Internalname, "", "Table", 0, "", "", 1, 2, sStyleString, "", "", 0);
            context.WriteHtmlText( "<tr>") ;
            context.WriteHtmlText( "<td>") ;
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 77,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttConfirmsign_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(96), 2, 0)+","+"null"+");", "Sign", bttConfirmsign_Jsonclick, 5, "Sign", "", StyleString, ClassString, 1, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'CONFIRM SIGN\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/MuSig2RoomsWC.htm");
            context.WriteHtmlText( "</td>") ;
            context.WriteHtmlText( "<td>") ;
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 79,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttCancelsign_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(96), 2, 0)+","+"null"+");", "Cancel", bttCancelsign_Jsonclick, 5, "Cancel", "", StyleString, ClassString, 1, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'CANCEL FORM\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/MuSig2RoomsWC.htm");
            context.WriteHtmlText( "</td>") ;
            context.WriteHtmlText( "</tr>") ;
            /* End of table */
            context.WriteHtmlText( "</table>") ;
            wb_table2_74_3T2e( true) ;
         }
         else
         {
            wb_table2_74_3T2e( false) ;
         }
      }

      protected void wb_table1_53_3T2( bool wbgen )
      {
         if ( wbgen )
         {
            /* Table start */
            sStyleString = "";
            GxWebStd.gx_table_start( context, tblNewbuttons_Internalname, tblNewbuttons_Internalname, "", "Table", 0, "", "", 1, 2, sStyleString, "", "", 0);
            context.WriteHtmlText( "<tr>") ;
            context.WriteHtmlText( "<td>") ;
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 56,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttOpenroom_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(96), 2, 0)+","+"null"+");", "Open payment session", bttOpenroom_Jsonclick, 5, "Open payment session", "", StyleString, ClassString, bttOpenroom_Visible, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'OPEN ROOM\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/MuSig2RoomsWC.htm");
            context.WriteHtmlText( "</td>") ;
            context.WriteHtmlText( "<td>") ;
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 58,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttEstimatefee_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(96), 2, 0)+","+"null"+");", "Estimate fee", bttEstimatefee_Jsonclick, 5, "Estimate fee", "", StyleString, ClassString, bttEstimatefee_Visible, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'ESTIMATE FEE\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/MuSig2RoomsWC.htm");
            context.WriteHtmlText( "</td>") ;
            context.WriteHtmlText( "<td>") ;
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 60,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttCancelnew_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(96), 2, 0)+","+"null"+");", "Cancel", bttCancelnew_Jsonclick, 5, "Cancel", "", StyleString, ClassString, 1, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'CANCEL FORM\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/MuSig2RoomsWC.htm");
            context.WriteHtmlText( "</td>") ;
            context.WriteHtmlText( "</tr>") ;
            /* End of table */
            context.WriteHtmlText( "</table>") ;
            wb_table1_53_3T2e( true) ;
         }
         else
         {
            wb_table1_53_3T2e( false) ;
         }
      }

      public override void setparameters( Object[] obj )
      {
         createObjects();
         initialize();
         AV6groupId = (Guid)getParm(obj,0);
         AssignAttri(sPrefix, false, "AV6groupId", AV6groupId.ToString());
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
         PA3T2( ) ;
         WS3T2( ) ;
         WE3T2( ) ;
         cleanup();
         context.SetWrapped(false);
         SaveComponentMsgList(sPrefix);
         context.GX_msglist = BackMsgLst;
         return "";
      }

      public void responsestatic( string sGXDynURL )
      {
      }

      public override void componentbind( Object[] obj )
      {
         if ( IsUrlCreated( ) )
         {
            return  ;
         }
         sCtrlAV6groupId = (string)((string)getParm(obj,0));
      }

      public override void componentrestorestate( string sPPrefix ,
                                                  string sPSFPrefix )
      {
         sPrefix = sPPrefix + sPSFPrefix;
         PA3T2( ) ;
         WCParametersGet( ) ;
      }

      public override void componentprepare( Object[] obj )
      {
         wbLoad = false;
         sCompPrefix = (string)getParm(obj,0);
         sSFPrefix = (string)getParm(obj,1);
         sPrefix = sCompPrefix + sSFPrefix;
         AddComponentObject(sPrefix, "wallet\\registered\\musig2roomswc", GetJustCreated( ));
         if ( ( nDoneStart == 0 ) && ( nDynComponent == 0 ) )
         {
            INITWEB( ) ;
         }
         else
         {
            init_default_properties( ) ;
            init_web_controls( ) ;
         }
         PA3T2( ) ;
         if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) && ( context.wbGlbDoneStart == 0 ) )
         {
            WCParametersGet( ) ;
         }
         else
         {
            AV6groupId = (Guid)getParm(obj,2);
            AssignAttri(sPrefix, false, "AV6groupId", AV6groupId.ToString());
         }
         wcpOAV6groupId = StringUtil.StrToGuid( cgiGet( sPrefix+"wcpOAV6groupId"));
         if ( ! GetJustCreated( ) && ( ( AV6groupId != wcpOAV6groupId ) ) )
         {
            setjustcreated();
         }
         wcpOAV6groupId = AV6groupId;
      }

      protected void WCParametersGet( )
      {
         /* Read Component Parameters. */
         sCtrlAV6groupId = cgiGet( sPrefix+"AV6groupId_CTRL");
         if ( StringUtil.Len( sCtrlAV6groupId) > 0 )
         {
            AV6groupId = StringUtil.StrToGuid( cgiGet( sCtrlAV6groupId));
            AssignAttri(sPrefix, false, "AV6groupId", AV6groupId.ToString());
         }
         else
         {
            AV6groupId = StringUtil.StrToGuid( cgiGet( sPrefix+"AV6groupId_PARM"));
         }
      }

      public override void componentprocess( string sPPrefix ,
                                             string sPSFPrefix ,
                                             string sCompEvt )
      {
         sCompPrefix = sPPrefix;
         sSFPrefix = sPSFPrefix;
         sPrefix = sCompPrefix + sSFPrefix;
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         INITWEB( ) ;
         nDraw = 0;
         PA3T2( ) ;
         sEvt = sCompEvt;
         WCParametersGet( ) ;
         WS3T2( ) ;
         if ( isFullAjaxMode( ) )
         {
            componentdraw();
         }
         SaveComponentMsgList(sPrefix);
         context.GX_msglist = BackMsgLst;
      }

      public override void componentstart( )
      {
         if ( nDoneStart == 0 )
         {
            WCStart( ) ;
         }
      }

      protected void WCStart( )
      {
         nDraw = 1;
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         WS3T2( ) ;
         SaveComponentMsgList(sPrefix);
         context.GX_msglist = BackMsgLst;
      }

      protected void WCParametersSet( )
      {
         GxWebStd.gx_hidden_field( context, sPrefix+"AV6groupId_PARM", AV6groupId.ToString());
         if ( StringUtil.Len( StringUtil.RTrim( sCtrlAV6groupId)) > 0 )
         {
            GxWebStd.gx_hidden_field( context, sPrefix+"AV6groupId_CTRL", StringUtil.RTrim( sCtrlAV6groupId));
         }
      }

      public override void componentdraw( )
      {
         if ( nDoneStart == 0 )
         {
            WCStart( ) ;
         }
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         WCParametersSet( ) ;
         WE3T2( ) ;
         SaveComponentMsgList(sPrefix);
         context.GX_msglist = BackMsgLst;
      }

      public override string getstring( string sGXControl )
      {
         string sCtrlName;
         if ( StringUtil.StrCmp(StringUtil.Substring( sGXControl, 1, 1), "&") == 0 )
         {
            sCtrlName = StringUtil.Substring( sGXControl, 2, StringUtil.Len( sGXControl)-1);
         }
         else
         {
            sCtrlName = sGXControl;
         }
         return cgiGet( sPrefix+"v"+StringUtil.Upper( sCtrlName)) ;
      }

      public override void componentjscripts( )
      {
         include_jscripts( ) ;
      }

      public override void componentthemes( )
      {
         define_styles( ) ;
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?202610301325352", true, true, false);
            idxLst = (int)(idxLst+1);
         }
         if ( ! outputEnabled )
         {
            if ( context.isSpaRequest( ) )
            {
               disableOutput();
            }
         }
         CloseStyles();
         /* End function define_styles */
      }

      protected void include_jscripts( )
      {
         context.AddJavascriptSource("wallet/registered/musig2roomswc.js", "?202610301325352", false, true, false);
         context.AddJavascriptSource("web-extension/gx-web-extensions.js", "", false, true, false);
         /* End function include_jscripts */
      }

      protected void SubsflControlProps_962( )
      {
         edtavCtlroomid_Internalname = sPrefix+"CTLROOMID_"+sGXsfl_96_idx;
         edtavMark_Internalname = sPrefix+"vMARK_"+sGXsfl_96_idx;
         edtavCtlcreated_Internalname = sPrefix+"CTLCREATED_"+sGXsfl_96_idx;
         edtavCtlopenedby_Internalname = sPrefix+"CTLOPENEDBY_"+sGXsfl_96_idx;
         edtavCtldescription_Internalname = sPrefix+"CTLDESCRIPTION_"+sGXsfl_96_idx;
         edtavCtlamounttext_Internalname = sPrefix+"CTLAMOUNTTEXT_"+sGXsfl_96_idx;
         edtavCtlstateshort_Internalname = sPrefix+"CTLSTATESHORT_"+sGXsfl_96_idx;
         edtavOpen_Internalname = sPrefix+"vOPEN_"+sGXsfl_96_idx;
      }

      protected void SubsflControlProps_fel_962( )
      {
         edtavCtlroomid_Internalname = sPrefix+"CTLROOMID_"+sGXsfl_96_fel_idx;
         edtavMark_Internalname = sPrefix+"vMARK_"+sGXsfl_96_fel_idx;
         edtavCtlcreated_Internalname = sPrefix+"CTLCREATED_"+sGXsfl_96_fel_idx;
         edtavCtlopenedby_Internalname = sPrefix+"CTLOPENEDBY_"+sGXsfl_96_fel_idx;
         edtavCtldescription_Internalname = sPrefix+"CTLDESCRIPTION_"+sGXsfl_96_fel_idx;
         edtavCtlamounttext_Internalname = sPrefix+"CTLAMOUNTTEXT_"+sGXsfl_96_fel_idx;
         edtavCtlstateshort_Internalname = sPrefix+"CTLSTATESHORT_"+sGXsfl_96_fel_idx;
         edtavOpen_Internalname = sPrefix+"vOPEN_"+sGXsfl_96_fel_idx;
      }

      protected void sendrow_962( )
      {
         sGXsfl_96_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_96_idx), 4, 0), 4, "0");
         SubsflControlProps_962( ) ;
         WB3T0( ) ;
         if ( ( 10 * 1 == 0 ) || ( nGXsfl_96_idx <= subGridrooms_fnc_Recordsperpage( ) * 1 ) )
         {
            GridroomsRow = GXWebRow.GetNew(context,GridroomsContainer);
            if ( subGridrooms_Backcolorstyle == 0 )
            {
               /* None style subfile background logic. */
               subGridrooms_Backstyle = 0;
               if ( StringUtil.StrCmp(subGridrooms_Class, "") != 0 )
               {
                  subGridrooms_Linesclass = subGridrooms_Class+"Odd";
               }
            }
            else if ( subGridrooms_Backcolorstyle == 1 )
            {
               /* Uniform style subfile background logic. */
               subGridrooms_Backstyle = 0;
               subGridrooms_Backcolor = subGridrooms_Allbackcolor;
               if ( StringUtil.StrCmp(subGridrooms_Class, "") != 0 )
               {
                  subGridrooms_Linesclass = subGridrooms_Class+"Uniform";
               }
            }
            else if ( subGridrooms_Backcolorstyle == 2 )
            {
               /* Header style subfile background logic. */
               subGridrooms_Backstyle = 1;
               if ( StringUtil.StrCmp(subGridrooms_Class, "") != 0 )
               {
                  subGridrooms_Linesclass = subGridrooms_Class+"Odd";
               }
               subGridrooms_Backcolor = (int)(0x0);
            }
            else if ( subGridrooms_Backcolorstyle == 3 )
            {
               /* Report style subfile background logic. */
               subGridrooms_Backstyle = 1;
               if ( ((int)((nGXsfl_96_idx) % (2))) == 0 )
               {
                  subGridrooms_Backcolor = (int)(0x0);
                  if ( StringUtil.StrCmp(subGridrooms_Class, "") != 0 )
                  {
                     subGridrooms_Linesclass = subGridrooms_Class+"Even";
                  }
               }
               else
               {
                  subGridrooms_Backcolor = (int)(0x0);
                  if ( StringUtil.StrCmp(subGridrooms_Class, "") != 0 )
                  {
                     subGridrooms_Linesclass = subGridrooms_Class+"Odd";
                  }
               }
            }
            if ( GridroomsContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<tr ") ;
               context.WriteHtmlText( " class=\""+"Grid"+"\" style=\""+""+"\"") ;
               context.WriteHtmlText( " gxrow=\""+sGXsfl_96_idx+"\">") ;
            }
            /* Subfile cell */
            if ( GridroomsContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+""+"\""+" style=\""+"display:none;"+"\">") ;
            }
            /* Single line edit */
            ROClassString = "Attribute";
            GridroomsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlroomid_Internalname,((GeneXus.Programs.wallet.registered.SdtMuSig2RoomLine)AV7lines.Item(AV46GXV1)).gxTpr_Roomid.ToString(),((GeneXus.Programs.wallet.registered.SdtMuSig2RoomLine)AV7lines.Item(AV46GXV1)).gxTpr_Roomid.ToString(),""+" onchange=\""+""+";gx.evt.onchange(this, event)\" ",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlroomid_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)0,(int)edtavCtlroomid_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)36,(short)0,(short)0,(short)96,(short)0,(short)0,(short)0,(bool)true,(string)"",(string)"",(bool)false,(string)""});
            /* Subfile cell */
            if ( GridroomsContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 98,'" + sPrefix + "',false,'" + sGXsfl_96_idx + "',96)\"";
            ROClassString = "Attribute";
            GridroomsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavMark_Internalname,(string)AV45mark,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,98);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavMark_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavMark_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)4,(short)0,(short)0,(short)96,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( GridroomsContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 99,'" + sPrefix + "',false,'" + sGXsfl_96_idx + "',96)\"";
            ROClassString = "Attribute";
            GridroomsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlcreated_Internalname,((GeneXus.Programs.wallet.registered.SdtMuSig2RoomLine)AV7lines.Item(AV46GXV1)).gxTpr_Created,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,99);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlcreated_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlcreated_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)40,(short)0,(short)0,(short)96,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( GridroomsContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 100,'" + sPrefix + "',false,'" + sGXsfl_96_idx + "',96)\"";
            ROClassString = "Attribute";
            GridroomsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlopenedby_Internalname,((GeneXus.Programs.wallet.registered.SdtMuSig2RoomLine)AV7lines.Item(AV46GXV1)).gxTpr_Openedby,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,100);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlopenedby_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlopenedby_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)250,(short)0,(short)0,(short)96,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( GridroomsContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 101,'" + sPrefix + "',false,'" + sGXsfl_96_idx + "',96)\"";
            ROClassString = "Attribute";
            GridroomsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtldescription_Internalname,((GeneXus.Programs.wallet.registered.SdtMuSig2RoomLine)AV7lines.Item(AV46GXV1)).gxTpr_Description,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,101);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtldescription_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtldescription_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)250,(short)0,(short)0,(short)96,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( GridroomsContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 102,'" + sPrefix + "',false,'" + sGXsfl_96_idx + "',96)\"";
            ROClassString = "Attribute";
            GridroomsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlamounttext_Internalname,((GeneXus.Programs.wallet.registered.SdtMuSig2RoomLine)AV7lines.Item(AV46GXV1)).gxTpr_Amounttext,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,102);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlamounttext_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlamounttext_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)60,(short)0,(short)0,(short)96,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( GridroomsContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 103,'" + sPrefix + "',false,'" + sGXsfl_96_idx + "',96)\"";
            ROClassString = "Attribute";
            GridroomsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlstateshort_Internalname,((GeneXus.Programs.wallet.registered.SdtMuSig2RoomLine)AV7lines.Item(AV46GXV1)).gxTpr_Stateshort,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,103);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlstateshort_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlstateshort_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)60,(short)0,(short)0,(short)96,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( GridroomsContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 104,'" + sPrefix + "',false,'" + sGXsfl_96_idx + "',96)\"";
            ROClassString = "Attribute";
            GridroomsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavOpen_Internalname,(string)AV41open,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,104);\"","'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'OPEN\\'."+sGXsfl_96_idx+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavOpen_Jsonclick,(short)5,(string)"Attribute",(string)"",(string)ROClassString,(string)"no-wrap",(string)"",(short)-1,(int)edtavOpen_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)20,(short)0,(short)0,(short)96,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            send_integrity_lvl_hashes3T2( ) ;
            GridroomsContainer.AddRow(GridroomsRow);
            nGXsfl_96_idx = ((subGridrooms_Islastpage==1)&&(nGXsfl_96_idx+1>subGridrooms_fnc_Recordsperpage( )) ? 1 : nGXsfl_96_idx+1);
            sGXsfl_96_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_96_idx), 4, 0), 4, "0");
            SubsflControlProps_962( ) ;
         }
         /* End function sendrow_962 */
      }

      protected void init_web_controls( )
      {
         chkavSendallcoins.Name = "vSENDALLCOINS";
         chkavSendallcoins.WebTags = "";
         chkavSendallcoins.Caption = "Send total balance";
         AssignProp(sPrefix, false, chkavSendallcoins_Internalname, "TitleCaption", chkavSendallcoins.Caption, true);
         chkavSendallcoins.CheckedValue = "false";
         cmbavUserfee.Name = "vUSERFEE";
         cmbavUserfee.WebTags = "";
         if ( cmbavUserfee.ItemCount > 0 )
         {
         }
         /* End function init_web_controls */
      }

      protected void StartGridControl96( )
      {
         if ( GridroomsContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+sPrefix+"GridroomsContainer"+"DivS\" data-gxgridid=\"96\">") ;
            sStyleString = "";
            GxWebStd.gx_table_start( context, subGridrooms_Internalname, subGridrooms_Internalname, "", "Grid", 0, "", "", 1, 2, sStyleString, "", "", 0);
            /* Subfile titles */
            context.WriteHtmlText( "<tr") ;
            context.WriteHtmlTextNl( ">") ;
            if ( subGridrooms_Backcolorstyle == 0 )
            {
               subGridrooms_Titlebackstyle = 0;
               if ( StringUtil.Len( subGridrooms_Class) > 0 )
               {
                  subGridrooms_Linesclass = subGridrooms_Class+"Title";
               }
            }
            else
            {
               subGridrooms_Titlebackstyle = 1;
               if ( subGridrooms_Backcolorstyle == 1 )
               {
                  subGridrooms_Titlebackcolor = subGridrooms_Allbackcolor;
                  if ( StringUtil.Len( subGridrooms_Class) > 0 )
                  {
                     subGridrooms_Linesclass = subGridrooms_Class+"UniformTitle";
                  }
               }
               else
               {
                  if ( StringUtil.Len( subGridrooms_Class) > 0 )
                  {
                     subGridrooms_Linesclass = subGridrooms_Class+"Title";
                  }
               }
            }
            context.WriteHtmlText( "<th align=\""+""+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+"display:none;"+""+"\" "+">") ;
            context.SendWebValue( "room Id") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Opened") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "By") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Description") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Amount") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "State") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlTextNl( "</tr>") ;
            GridroomsContainer.AddObjectProperty("GridName", "Gridrooms");
         }
         else
         {
            GridroomsContainer.AddObjectProperty("GridName", "Gridrooms");
            GridroomsContainer.AddObjectProperty("Header", subGridrooms_Header);
            GridroomsContainer.AddObjectProperty("Class", "Grid");
            GridroomsContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
            GridroomsContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
            GridroomsContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridrooms_Backcolorstyle), 1, 0, ".", "")));
            GridroomsContainer.AddObjectProperty("CmpContext", sPrefix);
            GridroomsContainer.AddObjectProperty("InMasterPage", "false");
            GridroomsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridroomsColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlroomid_Enabled), 5, 0, ".", "")));
            GridroomsContainer.AddColumnProperties(GridroomsColumn);
            GridroomsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridroomsColumn.AddObjectProperty("Value", GXUtil.ValueEncode( AV45mark));
            GridroomsColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavMark_Enabled), 5, 0, ".", "")));
            GridroomsContainer.AddColumnProperties(GridroomsColumn);
            GridroomsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridroomsColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlcreated_Enabled), 5, 0, ".", "")));
            GridroomsContainer.AddColumnProperties(GridroomsColumn);
            GridroomsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridroomsColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlopenedby_Enabled), 5, 0, ".", "")));
            GridroomsContainer.AddColumnProperties(GridroomsColumn);
            GridroomsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridroomsColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtldescription_Enabled), 5, 0, ".", "")));
            GridroomsContainer.AddColumnProperties(GridroomsColumn);
            GridroomsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridroomsColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlamounttext_Enabled), 5, 0, ".", "")));
            GridroomsContainer.AddColumnProperties(GridroomsColumn);
            GridroomsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridroomsColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlstateshort_Enabled), 5, 0, ".", "")));
            GridroomsContainer.AddColumnProperties(GridroomsColumn);
            GridroomsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridroomsColumn.AddObjectProperty("Value", GXUtil.ValueEncode( AV41open));
            GridroomsColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavOpen_Enabled), 5, 0, ".", "")));
            GridroomsContainer.AddColumnProperties(GridroomsColumn);
            GridroomsContainer.AddObjectProperty("Selectedindex", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridrooms_Selectedindex), 4, 0, ".", "")));
            GridroomsContainer.AddObjectProperty("Allowselection", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridrooms_Allowselection), 1, 0, ".", "")));
            GridroomsContainer.AddObjectProperty("Selectioncolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridrooms_Selectioncolor), 9, 0, ".", "")));
            GridroomsContainer.AddObjectProperty("Allowhover", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridrooms_Allowhovering), 1, 0, ".", "")));
            GridroomsContainer.AddObjectProperty("Hovercolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridrooms_Hoveringcolor), 9, 0, ".", "")));
            GridroomsContainer.AddObjectProperty("Allowcollapsing", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridrooms_Allowcollapsing), 1, 0, ".", "")));
            GridroomsContainer.AddObjectProperty("Collapsed", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridrooms_Collapsed), 1, 0, ".", "")));
         }
      }

      protected void init_default_properties( )
      {
         lblTbnew_Internalname = sPrefix+"TBNEW";
         chkavSendallcoins_Internalname = sPrefix+"vSENDALLCOINS";
         edtavSendcoins_Internalname = sPrefix+"vSENDCOINS";
         edtavSendto_Internalname = sPrefix+"vSENDTO";
         edtavDescription_Internalname = sPrefix+"vDESCRIPTION";
         cmbavUserfee_Internalname = sPrefix+"vUSERFEE";
         edtavManualfee_Internalname = sPrefix+"vMANUALFEE";
         edtavOwnerpassword_Internalname = sPrefix+"vOWNERPASSWORD";
         bttPayalone_Internalname = sPrefix+"PAYALONE";
         divOwnerfeetable_Internalname = sPrefix+"OWNERFEETABLE";
         bttOpenroom_Internalname = sPrefix+"OPENROOM";
         bttEstimatefee_Internalname = sPrefix+"ESTIMATEFEE";
         bttCancelnew_Internalname = sPrefix+"CANCELNEW";
         tblNewbuttons_Internalname = sPrefix+"NEWBUTTONS";
         divNewtable_Internalname = sPrefix+"NEWTABLE";
         lblTbsign_Internalname = sPrefix+"TBSIGN";
         edtavPassword_Internalname = sPrefix+"vPASSWORD";
         bttConfirmsign_Internalname = sPrefix+"CONFIRMSIGN";
         bttCancelsign_Internalname = sPrefix+"CANCELSIGN";
         tblSignbuttons_Internalname = sPrefix+"SIGNBUTTONS";
         divSigntable_Internalname = sPrefix+"SIGNTABLE";
         lblTbempty_Internalname = sPrefix+"TBEMPTY";
         bttNewpayment_Internalname = sPrefix+"NEWPAYMENT";
         bttRefreshrooms_Internalname = sPrefix+"REFRESHROOMS";
         tblButtonstable_Internalname = sPrefix+"BUTTONSTABLE";
         edtavCtlroomid_Internalname = sPrefix+"CTLROOMID";
         edtavMark_Internalname = sPrefix+"vMARK";
         edtavCtlcreated_Internalname = sPrefix+"CTLCREATED";
         edtavCtlopenedby_Internalname = sPrefix+"CTLOPENEDBY";
         edtavCtldescription_Internalname = sPrefix+"CTLDESCRIPTION";
         edtavCtlamounttext_Internalname = sPrefix+"CTLAMOUNTTEXT";
         edtavCtlstateshort_Internalname = sPrefix+"CTLSTATESHORT";
         edtavOpen_Internalname = sPrefix+"vOPEN";
         lblTbroomtitle_Internalname = sPrefix+"TBROOMTITLE";
         lblTbroomstate_Internalname = sPrefix+"TBROOMSTATE";
         lblTbroompeople_Internalname = sPrefix+"TBROOMPEOPLE";
         lblTbroomwaiting_Internalname = sPrefix+"TBROOMWAITING";
         bttHere_Internalname = sPrefix+"HERE";
         bttSignnow_Internalname = sPrefix+"SIGNNOW";
         bttSign_Internalname = sPrefix+"SIGN";
         bttAbort_Internalname = sPrefix+"ABORT";
         tblRoombuttons_Internalname = sPrefix+"ROOMBUTTONS";
         edtavChatlogview_Internalname = sPrefix+"vCHATLOGVIEW";
         edtavChattext_Internalname = sPrefix+"vCHATTEXT";
         bttSendchat_Internalname = sPrefix+"SENDCHAT";
         tblChatinputtable_Internalname = sPrefix+"CHATINPUTTABLE";
         divRoomtable_Internalname = sPrefix+"ROOMTABLE";
         divListtable_Internalname = sPrefix+"LISTTABLE";
         divMaintable_Internalname = sPrefix+"MAINTABLE";
         Form.Internalname = sPrefix+"FORM";
         subGridrooms_Internalname = sPrefix+"GRIDROOMS";
      }

      public override void initialize_properties( )
      {
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
         }
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            if ( context.isSpaRequest( ) )
            {
               disableJsOutput();
            }
         }
         init_default_properties( ) ;
         subGridrooms_Allowcollapsing = 0;
         subGridrooms_Allowselection = 0;
         subGridrooms_Header = "";
         chkavSendallcoins.Caption = "Send total balance";
         edtavOpen_Jsonclick = "";
         edtavOpen_Enabled = 1;
         edtavCtlstateshort_Jsonclick = "";
         edtavCtlstateshort_Enabled = 0;
         edtavCtlamounttext_Jsonclick = "";
         edtavCtlamounttext_Enabled = 0;
         edtavCtldescription_Jsonclick = "";
         edtavCtldescription_Enabled = 0;
         edtavCtlopenedby_Jsonclick = "";
         edtavCtlopenedby_Enabled = 0;
         edtavCtlcreated_Jsonclick = "";
         edtavCtlcreated_Enabled = 0;
         edtavMark_Jsonclick = "";
         edtavMark_Enabled = 1;
         edtavCtlroomid_Jsonclick = "";
         edtavCtlroomid_Enabled = 0;
         subGridrooms_Class = "Grid";
         subGridrooms_Backcolorstyle = 0;
         bttEstimatefee_Visible = 1;
         bttOpenroom_Visible = 1;
         bttAbort_Visible = 1;
         bttSign_Visible = 1;
         bttSignnow_Visible = 1;
         bttHere_Visible = 1;
         edtavChattext_Jsonclick = "";
         edtavChattext_Enabled = 1;
         tblChatinputtable_Visible = 1;
         bttNewpayment_Caption = "New payment";
         edtavChatlogview_Enabled = 1;
         lblTbroomwaiting_Caption = "TBRoomWaiting";
         lblTbroompeople_Caption = "TBRoomPeople";
         lblTbroomstate_Caption = "TBRoomState";
         lblTbroomtitle_Caption = "TBRoomTitle";
         divRoomtable_Visible = 1;
         lblTbempty_Caption = "TBEmpty";
         divListtable_Visible = 1;
         edtavPassword_Jsonclick = "";
         edtavPassword_Enabled = 1;
         lblTbsign_Caption = "TBSign";
         divSigntable_Visible = 1;
         edtavOwnerpassword_Jsonclick = "";
         edtavOwnerpassword_Enabled = 1;
         edtavManualfee_Jsonclick = "";
         edtavManualfee_Enabled = 1;
         cmbavUserfee_Jsonclick = "";
         cmbavUserfee.Enabled = 1;
         divOwnerfeetable_Visible = 1;
         edtavDescription_Enabled = 1;
         edtavSendto_Enabled = 1;
         edtavSendcoins_Jsonclick = "";
         edtavSendcoins_Enabled = 1;
         chkavSendallcoins.Enabled = 1;
         lblTbnew_Caption = "TBNew";
         divNewtable_Visible = 1;
         edtavCtlstateshort_Enabled = -1;
         edtavCtlamounttext_Enabled = -1;
         edtavCtldescription_Enabled = -1;
         edtavCtlopenedby_Enabled = -1;
         edtavCtlcreated_Enabled = -1;
         edtavCtlroomid_Enabled = -1;
         subGridrooms_Rows = 10;
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            if ( context.isSpaRequest( ) )
            {
               enableJsOutput();
            }
         }
      }

      public override bool SupportAjaxEvent( )
      {
         return true ;
      }

      public override void InitializeDynEvents( )
      {
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"GRIDROOMS_nFirstRecordOnPage","type":"int"},{"av":"GRIDROOMS_nEOF","type":"int"},{"av":"subGridrooms_Rows","ctrl":"GRIDROOMS","prop":"Rows"},{"av":"AV7lines","fld":"vLINES","grid":96,"type":""},{"av":"nGXsfl_96_idx","ctrl":"GRID","prop":"GridCurrRow","grid":96},{"av":"nRC_GXsfl_96","ctrl":"GRIDROOMS","prop":"GridRC","grid":96,"type":"int"},{"av":"sPrefix","type":"char"},{"av":"AV6groupId","fld":"vGROUPID","type":"guid"},{"av":"AV43selectedRoomId","fld":"vSELECTEDROOMID","type":"guid"},{"av":"AV16sendAllCoins","fld":"vSENDALLCOINS","type":"boolean"},{"av":"AV30ownerAlone","fld":"vOWNERALONE","hsh":true,"type":"boolean"},{"av":"AV42selected","fld":"vSELECTED","hsh":true,"type":""}]""");
         setEventMetadata("REFRESH",""","oparms":[{"av":"AV5error","fld":"vERROR","type":"svchar"},{"av":"AV7lines","fld":"vLINES","grid":96,"type":""},{"av":"nGXsfl_96_idx","ctrl":"GRID","prop":"GridCurrRow","grid":96},{"av":"GRIDROOMS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_96","ctrl":"GRIDROOMS","prop":"GridRC","grid":96,"type":"int"},{"av":"lblTbempty_Caption","ctrl":"TBEMPTY","prop":"Caption"},{"av":"AV42selected","fld":"vSELECTED","hsh":true,"type":""},{"av":"AV43selectedRoomId","fld":"vSELECTEDROOMID","type":"guid"},{"av":"lblTbroomtitle_Caption","ctrl":"TBROOMTITLE","prop":"Caption"},{"av":"lblTbroomstate_Caption","ctrl":"TBROOMSTATE","prop":"Caption"},{"av":"lblTbroompeople_Caption","ctrl":"TBROOMPEOPLE","prop":"Caption"},{"av":"lblTbroomwaiting_Caption","ctrl":"TBROOMWAITING","prop":"Caption"},{"ctrl":"HERE","prop":"Visible"},{"ctrl":"SIGNNOW","prop":"Visible"},{"ctrl":"SIGN","prop":"Visible"},{"ctrl":"ABORT","prop":"Visible"},{"av":"AV39chatLogView","fld":"vCHATLOGVIEW","type":"vchar"},{"av":"tblChatinputtable_Visible","ctrl":"CHATINPUTTABLE","prop":"Visible"},{"av":"divRoomtable_Visible","ctrl":"ROOMTABLE","prop":"Visible"}]}""");
         setEventMetadata("GRIDROOMS.LOAD","""{"handler":"E243T2","iparms":[{"av":"AV7lines","fld":"vLINES","grid":96,"type":""},{"av":"nGXsfl_96_idx","ctrl":"GRID","prop":"GridCurrRow","grid":96},{"av":"GRIDROOMS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_96","ctrl":"GRIDROOMS","prop":"GridRC","grid":96,"type":"int"},{"av":"AV43selectedRoomId","fld":"vSELECTEDROOMID","type":"guid"}]""");
         setEventMetadata("GRIDROOMS.LOAD",""","oparms":[{"av":"AV45mark","fld":"vMARK","type":"svchar"},{"av":"AV41open","fld":"vOPEN","type":"svchar"}]}""");
         setEventMetadata("'OPEN'","""{"handler":"E253T2","iparms":[{"av":"GRIDROOMS_nFirstRecordOnPage","type":"int"},{"av":"GRIDROOMS_nEOF","type":"int"},{"av":"subGridrooms_Rows","ctrl":"GRIDROOMS","prop":"Rows"},{"av":"AV6groupId","fld":"vGROUPID","type":"guid"},{"av":"AV30ownerAlone","fld":"vOWNERALONE","hsh":true,"type":"boolean"},{"av":"AV43selectedRoomId","fld":"vSELECTEDROOMID","type":"guid"},{"av":"AV42selected","fld":"vSELECTED","hsh":true,"type":""},{"av":"AV7lines","fld":"vLINES","grid":96,"type":""},{"av":"nGXsfl_96_idx","ctrl":"GRID","prop":"GridCurrRow","grid":96},{"av":"nRC_GXsfl_96","ctrl":"GRIDROOMS","prop":"GridRC","grid":96,"type":"int"},{"av":"AV16sendAllCoins","fld":"vSENDALLCOINS","type":"boolean"},{"av":"sPrefix","type":"char"}]""");
         setEventMetadata("'OPEN'",""","oparms":[{"av":"AV43selectedRoomId","fld":"vSELECTEDROOMID","type":"guid"},{"av":"AV5error","fld":"vERROR","type":"svchar"},{"av":"AV7lines","fld":"vLINES","grid":96,"type":""},{"av":"nGXsfl_96_idx","ctrl":"GRID","prop":"GridCurrRow","grid":96},{"av":"GRIDROOMS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_96","ctrl":"GRIDROOMS","prop":"GridRC","grid":96,"type":"int"},{"av":"lblTbempty_Caption","ctrl":"TBEMPTY","prop":"Caption"},{"av":"AV42selected","fld":"vSELECTED","hsh":true,"type":""},{"av":"lblTbroomtitle_Caption","ctrl":"TBROOMTITLE","prop":"Caption"},{"av":"lblTbroomstate_Caption","ctrl":"TBROOMSTATE","prop":"Caption"},{"av":"lblTbroompeople_Caption","ctrl":"TBROOMPEOPLE","prop":"Caption"},{"av":"lblTbroomwaiting_Caption","ctrl":"TBROOMWAITING","prop":"Caption"},{"ctrl":"HERE","prop":"Visible"},{"ctrl":"SIGNNOW","prop":"Visible"},{"ctrl":"SIGN","prop":"Visible"},{"ctrl":"ABORT","prop":"Visible"},{"av":"AV39chatLogView","fld":"vCHATLOGVIEW","type":"vchar"},{"av":"tblChatinputtable_Visible","ctrl":"CHATINPUTTABLE","prop":"Visible"},{"av":"divRoomtable_Visible","ctrl":"ROOMTABLE","prop":"Visible"}]}""");
         setEventMetadata("'NEW PAYMENT'","""{"handler":"E273T1","iparms":[{"av":"AV30ownerAlone","fld":"vOWNERALONE","hsh":true,"type":"boolean"}]""");
         setEventMetadata("'NEW PAYMENT'",""","oparms":[{"av":"AV16sendAllCoins","fld":"vSENDALLCOINS","type":"boolean"},{"av":"AV17sendCoins","fld":"vSENDCOINS","pic":"ZZZZZZ9.99999999","type":"decimal"},{"av":"AV18sendTo","fld":"vSENDTO","type":"char"},{"av":"AV12description","fld":"vDESCRIPTION","type":"svchar"},{"av":"AV38ownerPassword","fld":"vOWNERPASSWORD","type":"char"},{"av":"cmbavUserfee"},{"av":"AV33userFee","fld":"vUSERFEE","pic":"ZZZZZZ9.99999999","type":"decimal"},{"av":"AV29manualFee","fld":"vMANUALFEE","pic":"ZZZZZZ9.99999999","type":"decimal"},{"av":"divNewtable_Visible","ctrl":"NEWTABLE","prop":"Visible"},{"av":"lblTbnew_Caption","ctrl":"TBNEW","prop":"Caption"},{"ctrl":"OPENROOM","prop":"Visible"},{"ctrl":"ESTIMATEFEE","prop":"Visible"},{"av":"divOwnerfeetable_Visible","ctrl":"OWNERFEETABLE","prop":"Visible"},{"av":"AV13formOpen","fld":"vFORMOPEN","type":"boolean"},{"av":"divListtable_Visible","ctrl":"LISTTABLE","prop":"Visible"}]}""");
         setEventMetadata("'OPEN ROOM'","""{"handler":"E113T2","iparms":[{"av":"AV22checked","fld":"vCHECKED","type":"boolean"},{"av":"AV6groupId","fld":"vGROUPID","type":"guid"},{"av":"AV16sendAllCoins","fld":"vSENDALLCOINS","type":"boolean"},{"av":"AV17sendCoins","fld":"vSENDCOINS","pic":"ZZZZZZ9.99999999","type":"decimal"},{"av":"AV18sendTo","fld":"vSENDTO","type":"char"},{"av":"AV12description","fld":"vDESCRIPTION","type":"svchar"},{"av":"GRIDROOMS_nFirstRecordOnPage","type":"int"},{"av":"GRIDROOMS_nEOF","type":"int"},{"av":"subGridrooms_Rows","ctrl":"GRIDROOMS","prop":"Rows"},{"av":"AV30ownerAlone","fld":"vOWNERALONE","hsh":true,"type":"boolean"},{"av":"AV43selectedRoomId","fld":"vSELECTEDROOMID","type":"guid"},{"av":"AV42selected","fld":"vSELECTED","hsh":true,"type":""},{"av":"AV7lines","fld":"vLINES","grid":96,"type":""},{"av":"nGXsfl_96_idx","ctrl":"GRID","prop":"GridCurrRow","grid":96},{"av":"nRC_GXsfl_96","ctrl":"GRIDROOMS","prop":"GridRC","grid":96,"type":"int"},{"av":"sPrefix","type":"char"}]""");
         setEventMetadata("'OPEN ROOM'",""","oparms":[{"av":"AV5error","fld":"vERROR","type":"svchar"},{"av":"AV43selectedRoomId","fld":"vSELECTEDROOMID","type":"guid"},{"av":"AV22checked","fld":"vCHECKED","type":"boolean"},{"av":"AV17sendCoins","fld":"vSENDCOINS","pic":"ZZZZZZ9.99999999","type":"decimal"},{"av":"AV13formOpen","fld":"vFORMOPEN","type":"boolean"},{"av":"divNewtable_Visible","ctrl":"NEWTABLE","prop":"Visible"},{"av":"divSigntable_Visible","ctrl":"SIGNTABLE","prop":"Visible"},{"av":"divListtable_Visible","ctrl":"LISTTABLE","prop":"Visible"},{"av":"AV7lines","fld":"vLINES","grid":96,"type":""},{"av":"nGXsfl_96_idx","ctrl":"GRID","prop":"GridCurrRow","grid":96},{"av":"GRIDROOMS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_96","ctrl":"GRIDROOMS","prop":"GridRC","grid":96,"type":"int"},{"av":"lblTbempty_Caption","ctrl":"TBEMPTY","prop":"Caption"},{"av":"AV42selected","fld":"vSELECTED","hsh":true,"type":""},{"av":"lblTbroomtitle_Caption","ctrl":"TBROOMTITLE","prop":"Caption"},{"av":"lblTbroomstate_Caption","ctrl":"TBROOMSTATE","prop":"Caption"},{"av":"lblTbroompeople_Caption","ctrl":"TBROOMPEOPLE","prop":"Caption"},{"av":"lblTbroomwaiting_Caption","ctrl":"TBROOMWAITING","prop":"Caption"},{"ctrl":"HERE","prop":"Visible"},{"ctrl":"SIGNNOW","prop":"Visible"},{"ctrl":"SIGN","prop":"Visible"},{"ctrl":"ABORT","prop":"Visible"},{"av":"AV39chatLogView","fld":"vCHATLOGVIEW","type":"vchar"},{"av":"tblChatinputtable_Visible","ctrl":"CHATINPUTTABLE","prop":"Visible"},{"av":"divRoomtable_Visible","ctrl":"ROOMTABLE","prop":"Visible"}]}""");
         setEventMetadata("'ESTIMATE FEE'","""{"handler":"E123T2","iparms":[{"av":"AV22checked","fld":"vCHECKED","type":"boolean"},{"av":"AV16sendAllCoins","fld":"vSENDALLCOINS","type":"boolean"},{"av":"AV17sendCoins","fld":"vSENDCOINS","pic":"ZZZZZZ9.99999999","type":"decimal"},{"av":"AV18sendTo","fld":"vSENDTO","type":"char"},{"av":"AV12description","fld":"vDESCRIPTION","type":"svchar"},{"av":"cmbavUserfee"},{"av":"AV33userFee","fld":"vUSERFEE","pic":"ZZZZZZ9.99999999","type":"decimal"},{"av":"AV24economicalFee","fld":"vECONOMICALFEE","pic":"ZZZZZZ9.99999999","type":"decimal"},{"av":"AV23economicalBlocks","fld":"vECONOMICALBLOCKS","pic":"ZZZZZZZZZ9","type":"int"},{"av":"AV32standardFee","fld":"vSTANDARDFEE","pic":"ZZZZZZ9.99999999","type":"decimal"},{"av":"AV31standarBlocks","fld":"vSTANDARBLOCKS","pic":"ZZZZZZZZZ9","type":"int"},{"av":"AV27fastestFee","fld":"vFASTESTFEE","pic":"ZZZZZZ9.99999999","type":"decimal"},{"av":"AV26fastestBlocks","fld":"vFASTESTBLOCKS","pic":"ZZZZZZZZZ9","type":"int"},{"av":"AV6groupId","fld":"vGROUPID","type":"guid"}]""");
         setEventMetadata("'ESTIMATE FEE'",""","oparms":[{"av":"AV5error","fld":"vERROR","type":"svchar"},{"av":"AV23economicalBlocks","fld":"vECONOMICALBLOCKS","pic":"ZZZZZZZZZ9","type":"int"},{"av":"AV24economicalFee","fld":"vECONOMICALFEE","pic":"ZZZZZZ9.99999999","type":"decimal"},{"av":"AV31standarBlocks","fld":"vSTANDARBLOCKS","pic":"ZZZZZZZZZ9","type":"int"},{"av":"AV32standardFee","fld":"vSTANDARDFEE","pic":"ZZZZZZ9.99999999","type":"decimal"},{"av":"AV26fastestBlocks","fld":"vFASTESTBLOCKS","pic":"ZZZZZZZZZ9","type":"int"},{"av":"AV27fastestFee","fld":"vFASTESTFEE","pic":"ZZZZZZ9.99999999","type":"decimal"},{"av":"cmbavUserfee"},{"av":"AV33userFee","fld":"vUSERFEE","pic":"ZZZZZZ9.99999999","type":"decimal"},{"av":"divOwnerfeetable_Visible","ctrl":"OWNERFEETABLE","prop":"Visible"},{"ctrl":"ESTIMATEFEE","prop":"Visible"},{"av":"AV22checked","fld":"vCHECKED","type":"boolean"},{"av":"AV17sendCoins","fld":"vSENDCOINS","pic":"ZZZZZZ9.99999999","type":"decimal"}]}""");
         setEventMetadata("'PAY ALONE'","""{"handler":"E133T2","iparms":[{"av":"AV29manualFee","fld":"vMANUALFEE","pic":"ZZZZZZ9.99999999","type":"decimal"},{"av":"AV6groupId","fld":"vGROUPID","type":"guid"},{"av":"AV38ownerPassword","fld":"vOWNERPASSWORD","type":"char"},{"av":"AV16sendAllCoins","fld":"vSENDALLCOINS","type":"boolean"},{"av":"AV17sendCoins","fld":"vSENDCOINS","pic":"ZZZZZZ9.99999999","type":"decimal"},{"av":"AV18sendTo","fld":"vSENDTO","type":"char"},{"av":"AV12description","fld":"vDESCRIPTION","type":"svchar"},{"av":"AV21broadcast","fld":"vBROADCAST","type":"boolean"},{"av":"AV25errorTitle","fld":"vERRORTITLE","type":"char"},{"av":"GRIDROOMS_nFirstRecordOnPage","type":"int"},{"av":"GRIDROOMS_nEOF","type":"int"},{"av":"subGridrooms_Rows","ctrl":"GRIDROOMS","prop":"Rows"},{"av":"AV30ownerAlone","fld":"vOWNERALONE","hsh":true,"type":"boolean"},{"av":"AV43selectedRoomId","fld":"vSELECTEDROOMID","type":"guid"},{"av":"AV42selected","fld":"vSELECTED","hsh":true,"type":""},{"av":"AV7lines","fld":"vLINES","grid":96,"type":""},{"av":"nGXsfl_96_idx","ctrl":"GRID","prop":"GridCurrRow","grid":96},{"av":"nRC_GXsfl_96","ctrl":"GRIDROOMS","prop":"GridRC","grid":96,"type":"int"},{"av":"sPrefix","type":"char"}]""");
         setEventMetadata("'PAY ALONE'",""","oparms":[{"av":"AV5error","fld":"vERROR","type":"svchar"},{"av":"AV38ownerPassword","fld":"vOWNERPASSWORD","type":"char"},{"av":"AV25errorTitle","fld":"vERRORTITLE","type":"char"},{"av":"AV21broadcast","fld":"vBROADCAST","type":"boolean"},{"av":"AV13formOpen","fld":"vFORMOPEN","type":"boolean"},{"av":"divNewtable_Visible","ctrl":"NEWTABLE","prop":"Visible"},{"av":"divSigntable_Visible","ctrl":"SIGNTABLE","prop":"Visible"},{"av":"divListtable_Visible","ctrl":"LISTTABLE","prop":"Visible"},{"av":"AV7lines","fld":"vLINES","grid":96,"type":""},{"av":"nGXsfl_96_idx","ctrl":"GRID","prop":"GridCurrRow","grid":96},{"av":"GRIDROOMS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_96","ctrl":"GRIDROOMS","prop":"GridRC","grid":96,"type":"int"},{"av":"lblTbempty_Caption","ctrl":"TBEMPTY","prop":"Caption"},{"av":"AV42selected","fld":"vSELECTED","hsh":true,"type":""},{"av":"AV43selectedRoomId","fld":"vSELECTEDROOMID","type":"guid"},{"av":"lblTbroomtitle_Caption","ctrl":"TBROOMTITLE","prop":"Caption"},{"av":"lblTbroomstate_Caption","ctrl":"TBROOMSTATE","prop":"Caption"},{"av":"lblTbroompeople_Caption","ctrl":"TBROOMPEOPLE","prop":"Caption"},{"av":"lblTbroomwaiting_Caption","ctrl":"TBROOMWAITING","prop":"Caption"},{"ctrl":"HERE","prop":"Visible"},{"ctrl":"SIGNNOW","prop":"Visible"},{"ctrl":"SIGN","prop":"Visible"},{"ctrl":"ABORT","prop":"Visible"},{"av":"AV39chatLogView","fld":"vCHATLOGVIEW","type":"vchar"},{"av":"tblChatinputtable_Visible","ctrl":"CHATINPUTTABLE","prop":"Visible"},{"av":"divRoomtable_Visible","ctrl":"ROOMTABLE","prop":"Visible"}]}""");
         setEventMetadata("'REFRESH ROOMS'","""{"handler":"E143T2","iparms":[{"av":"GRIDROOMS_nFirstRecordOnPage","type":"int"},{"av":"GRIDROOMS_nEOF","type":"int"},{"av":"subGridrooms_Rows","ctrl":"GRIDROOMS","prop":"Rows"},{"av":"AV6groupId","fld":"vGROUPID","type":"guid"},{"av":"AV30ownerAlone","fld":"vOWNERALONE","hsh":true,"type":"boolean"},{"av":"AV43selectedRoomId","fld":"vSELECTEDROOMID","type":"guid"},{"av":"AV42selected","fld":"vSELECTED","hsh":true,"type":""},{"av":"AV7lines","fld":"vLINES","grid":96,"type":""},{"av":"nGXsfl_96_idx","ctrl":"GRID","prop":"GridCurrRow","grid":96},{"av":"nRC_GXsfl_96","ctrl":"GRIDROOMS","prop":"GridRC","grid":96,"type":"int"},{"av":"AV16sendAllCoins","fld":"vSENDALLCOINS","type":"boolean"},{"av":"sPrefix","type":"char"}]""");
         setEventMetadata("'REFRESH ROOMS'",""","oparms":[{"av":"AV5error","fld":"vERROR","type":"svchar"},{"av":"AV7lines","fld":"vLINES","grid":96,"type":""},{"av":"nGXsfl_96_idx","ctrl":"GRID","prop":"GridCurrRow","grid":96},{"av":"GRIDROOMS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_96","ctrl":"GRIDROOMS","prop":"GridRC","grid":96,"type":"int"},{"av":"lblTbempty_Caption","ctrl":"TBEMPTY","prop":"Caption"},{"av":"AV42selected","fld":"vSELECTED","hsh":true,"type":""},{"av":"AV43selectedRoomId","fld":"vSELECTEDROOMID","type":"guid"},{"av":"lblTbroomtitle_Caption","ctrl":"TBROOMTITLE","prop":"Caption"},{"av":"lblTbroomstate_Caption","ctrl":"TBROOMSTATE","prop":"Caption"},{"av":"lblTbroompeople_Caption","ctrl":"TBROOMPEOPLE","prop":"Caption"},{"av":"lblTbroomwaiting_Caption","ctrl":"TBROOMWAITING","prop":"Caption"},{"ctrl":"HERE","prop":"Visible"},{"ctrl":"SIGNNOW","prop":"Visible"},{"ctrl":"SIGN","prop":"Visible"},{"ctrl":"ABORT","prop":"Visible"},{"av":"AV39chatLogView","fld":"vCHATLOGVIEW","type":"vchar"},{"av":"tblChatinputtable_Visible","ctrl":"CHATINPUTTABLE","prop":"Visible"},{"av":"divRoomtable_Visible","ctrl":"ROOMTABLE","prop":"Visible"}]}""");
         setEventMetadata("'HERE'","""{"handler":"E153T2","iparms":[{"av":"GRIDROOMS_nFirstRecordOnPage","type":"int"},{"av":"GRIDROOMS_nEOF","type":"int"},{"av":"subGridrooms_Rows","ctrl":"GRIDROOMS","prop":"Rows"},{"av":"AV6groupId","fld":"vGROUPID","type":"guid"},{"av":"AV30ownerAlone","fld":"vOWNERALONE","hsh":true,"type":"boolean"},{"av":"AV43selectedRoomId","fld":"vSELECTEDROOMID","type":"guid"},{"av":"AV42selected","fld":"vSELECTED","hsh":true,"type":""},{"av":"AV7lines","fld":"vLINES","grid":96,"type":""},{"av":"nGXsfl_96_idx","ctrl":"GRID","prop":"GridCurrRow","grid":96},{"av":"nRC_GXsfl_96","ctrl":"GRIDROOMS","prop":"GridRC","grid":96,"type":"int"},{"av":"AV16sendAllCoins","fld":"vSENDALLCOINS","type":"boolean"},{"av":"sPrefix","type":"char"},{"av":"AV5error","fld":"vERROR","type":"svchar"}]""");
         setEventMetadata("'HERE'",""","oparms":[{"av":"AV5error","fld":"vERROR","type":"svchar"},{"av":"AV7lines","fld":"vLINES","grid":96,"type":""},{"av":"nGXsfl_96_idx","ctrl":"GRID","prop":"GridCurrRow","grid":96},{"av":"GRIDROOMS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_96","ctrl":"GRIDROOMS","prop":"GridRC","grid":96,"type":"int"},{"av":"lblTbempty_Caption","ctrl":"TBEMPTY","prop":"Caption"},{"av":"AV42selected","fld":"vSELECTED","hsh":true,"type":""},{"av":"AV43selectedRoomId","fld":"vSELECTEDROOMID","type":"guid"},{"av":"lblTbroomtitle_Caption","ctrl":"TBROOMTITLE","prop":"Caption"},{"av":"lblTbroomstate_Caption","ctrl":"TBROOMSTATE","prop":"Caption"},{"av":"lblTbroompeople_Caption","ctrl":"TBROOMPEOPLE","prop":"Caption"},{"av":"lblTbroomwaiting_Caption","ctrl":"TBROOMWAITING","prop":"Caption"},{"ctrl":"HERE","prop":"Visible"},{"ctrl":"SIGNNOW","prop":"Visible"},{"ctrl":"SIGN","prop":"Visible"},{"ctrl":"ABORT","prop":"Visible"},{"av":"AV39chatLogView","fld":"vCHATLOGVIEW","type":"vchar"},{"av":"tblChatinputtable_Visible","ctrl":"CHATINPUTTABLE","prop":"Visible"},{"av":"divRoomtable_Visible","ctrl":"ROOMTABLE","prop":"Visible"}]}""");
         setEventMetadata("'SIGN NOW'","""{"handler":"E163T2","iparms":[{"av":"GRIDROOMS_nFirstRecordOnPage","type":"int"},{"av":"GRIDROOMS_nEOF","type":"int"},{"av":"subGridrooms_Rows","ctrl":"GRIDROOMS","prop":"Rows"},{"av":"AV6groupId","fld":"vGROUPID","type":"guid"},{"av":"AV30ownerAlone","fld":"vOWNERALONE","hsh":true,"type":"boolean"},{"av":"AV43selectedRoomId","fld":"vSELECTEDROOMID","type":"guid"},{"av":"AV42selected","fld":"vSELECTED","hsh":true,"type":""},{"av":"AV7lines","fld":"vLINES","grid":96,"type":""},{"av":"nGXsfl_96_idx","ctrl":"GRID","prop":"GridCurrRow","grid":96},{"av":"nRC_GXsfl_96","ctrl":"GRIDROOMS","prop":"GridRC","grid":96,"type":"int"},{"av":"AV16sendAllCoins","fld":"vSENDALLCOINS","type":"boolean"},{"av":"sPrefix","type":"char"},{"av":"AV5error","fld":"vERROR","type":"svchar"}]""");
         setEventMetadata("'SIGN NOW'",""","oparms":[{"av":"AV5error","fld":"vERROR","type":"svchar"},{"av":"AV7lines","fld":"vLINES","grid":96,"type":""},{"av":"nGXsfl_96_idx","ctrl":"GRID","prop":"GridCurrRow","grid":96},{"av":"GRIDROOMS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_96","ctrl":"GRIDROOMS","prop":"GridRC","grid":96,"type":"int"},{"av":"lblTbempty_Caption","ctrl":"TBEMPTY","prop":"Caption"},{"av":"AV42selected","fld":"vSELECTED","hsh":true,"type":""},{"av":"AV43selectedRoomId","fld":"vSELECTEDROOMID","type":"guid"},{"av":"lblTbroomtitle_Caption","ctrl":"TBROOMTITLE","prop":"Caption"},{"av":"lblTbroomstate_Caption","ctrl":"TBROOMSTATE","prop":"Caption"},{"av":"lblTbroompeople_Caption","ctrl":"TBROOMPEOPLE","prop":"Caption"},{"av":"lblTbroomwaiting_Caption","ctrl":"TBROOMWAITING","prop":"Caption"},{"ctrl":"HERE","prop":"Visible"},{"ctrl":"SIGNNOW","prop":"Visible"},{"ctrl":"SIGN","prop":"Visible"},{"ctrl":"ABORT","prop":"Visible"},{"av":"AV39chatLogView","fld":"vCHATLOGVIEW","type":"vchar"},{"av":"tblChatinputtable_Visible","ctrl":"CHATINPUTTABLE","prop":"Visible"},{"av":"divRoomtable_Visible","ctrl":"ROOMTABLE","prop":"Visible"}]}""");
         setEventMetadata("'SIGN'","""{"handler":"E263T1","iparms":[{"av":"AV43selectedRoomId","fld":"vSELECTEDROOMID","type":"guid"},{"av":"AV42selected","fld":"vSELECTED","hsh":true,"type":""}]""");
         setEventMetadata("'SIGN'",""","oparms":[{"av":"AV10signRoomId","fld":"vSIGNROOMID","type":"guid"},{"av":"AV14password","fld":"vPASSWORD","type":"char"},{"av":"lblTbsign_Caption","ctrl":"TBSIGN","prop":"Caption"},{"av":"divSigntable_Visible","ctrl":"SIGNTABLE","prop":"Visible"},{"av":"AV13formOpen","fld":"vFORMOPEN","type":"boolean"},{"av":"divListtable_Visible","ctrl":"LISTTABLE","prop":"Visible"}]}""");
         setEventMetadata("'CONFIRM SIGN'","""{"handler":"E173T2","iparms":[{"av":"AV14password","fld":"vPASSWORD","type":"char"},{"av":"AV6groupId","fld":"vGROUPID","type":"guid"},{"av":"AV10signRoomId","fld":"vSIGNROOMID","type":"guid"},{"av":"AV5error","fld":"vERROR","type":"svchar"},{"av":"GRIDROOMS_nFirstRecordOnPage","type":"int"},{"av":"GRIDROOMS_nEOF","type":"int"},{"av":"subGridrooms_Rows","ctrl":"GRIDROOMS","prop":"Rows"},{"av":"AV30ownerAlone","fld":"vOWNERALONE","hsh":true,"type":"boolean"},{"av":"AV43selectedRoomId","fld":"vSELECTEDROOMID","type":"guid"},{"av":"AV42selected","fld":"vSELECTED","hsh":true,"type":""},{"av":"AV7lines","fld":"vLINES","grid":96,"type":""},{"av":"nGXsfl_96_idx","ctrl":"GRID","prop":"GridCurrRow","grid":96},{"av":"nRC_GXsfl_96","ctrl":"GRIDROOMS","prop":"GridRC","grid":96,"type":"int"},{"av":"AV16sendAllCoins","fld":"vSENDALLCOINS","type":"boolean"},{"av":"sPrefix","type":"char"}]""");
         setEventMetadata("'CONFIRM SIGN'",""","oparms":[{"av":"AV5error","fld":"vERROR","type":"svchar"},{"av":"AV14password","fld":"vPASSWORD","type":"char"},{"av":"AV13formOpen","fld":"vFORMOPEN","type":"boolean"},{"av":"divNewtable_Visible","ctrl":"NEWTABLE","prop":"Visible"},{"av":"divSigntable_Visible","ctrl":"SIGNTABLE","prop":"Visible"},{"av":"divListtable_Visible","ctrl":"LISTTABLE","prop":"Visible"},{"av":"AV7lines","fld":"vLINES","grid":96,"type":""},{"av":"nGXsfl_96_idx","ctrl":"GRID","prop":"GridCurrRow","grid":96},{"av":"GRIDROOMS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_96","ctrl":"GRIDROOMS","prop":"GridRC","grid":96,"type":"int"},{"av":"lblTbempty_Caption","ctrl":"TBEMPTY","prop":"Caption"},{"av":"AV42selected","fld":"vSELECTED","hsh":true,"type":""},{"av":"AV43selectedRoomId","fld":"vSELECTEDROOMID","type":"guid"},{"av":"lblTbroomtitle_Caption","ctrl":"TBROOMTITLE","prop":"Caption"},{"av":"lblTbroomstate_Caption","ctrl":"TBROOMSTATE","prop":"Caption"},{"av":"lblTbroompeople_Caption","ctrl":"TBROOMPEOPLE","prop":"Caption"},{"av":"lblTbroomwaiting_Caption","ctrl":"TBROOMWAITING","prop":"Caption"},{"ctrl":"HERE","prop":"Visible"},{"ctrl":"SIGNNOW","prop":"Visible"},{"ctrl":"SIGN","prop":"Visible"},{"ctrl":"ABORT","prop":"Visible"},{"av":"AV39chatLogView","fld":"vCHATLOGVIEW","type":"vchar"},{"av":"tblChatinputtable_Visible","ctrl":"CHATINPUTTABLE","prop":"Visible"},{"av":"divRoomtable_Visible","ctrl":"ROOMTABLE","prop":"Visible"}]}""");
         setEventMetadata("'CANCEL FORM'","""{"handler":"E183T2","iparms":[{"av":"GRIDROOMS_nFirstRecordOnPage","type":"int"},{"av":"GRIDROOMS_nEOF","type":"int"},{"av":"subGridrooms_Rows","ctrl":"GRIDROOMS","prop":"Rows"},{"av":"AV6groupId","fld":"vGROUPID","type":"guid"},{"av":"AV30ownerAlone","fld":"vOWNERALONE","hsh":true,"type":"boolean"},{"av":"AV43selectedRoomId","fld":"vSELECTEDROOMID","type":"guid"},{"av":"AV42selected","fld":"vSELECTED","hsh":true,"type":""},{"av":"AV7lines","fld":"vLINES","grid":96,"type":""},{"av":"nGXsfl_96_idx","ctrl":"GRID","prop":"GridCurrRow","grid":96},{"av":"nRC_GXsfl_96","ctrl":"GRIDROOMS","prop":"GridRC","grid":96,"type":"int"},{"av":"AV16sendAllCoins","fld":"vSENDALLCOINS","type":"boolean"},{"av":"sPrefix","type":"char"}]""");
         setEventMetadata("'CANCEL FORM'",""","oparms":[{"av":"AV14password","fld":"vPASSWORD","type":"char"},{"av":"AV38ownerPassword","fld":"vOWNERPASSWORD","type":"char"},{"av":"AV13formOpen","fld":"vFORMOPEN","type":"boolean"},{"av":"divNewtable_Visible","ctrl":"NEWTABLE","prop":"Visible"},{"av":"divSigntable_Visible","ctrl":"SIGNTABLE","prop":"Visible"},{"av":"divListtable_Visible","ctrl":"LISTTABLE","prop":"Visible"},{"av":"AV5error","fld":"vERROR","type":"svchar"},{"av":"AV7lines","fld":"vLINES","grid":96,"type":""},{"av":"nGXsfl_96_idx","ctrl":"GRID","prop":"GridCurrRow","grid":96},{"av":"GRIDROOMS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_96","ctrl":"GRIDROOMS","prop":"GridRC","grid":96,"type":"int"},{"av":"lblTbempty_Caption","ctrl":"TBEMPTY","prop":"Caption"},{"av":"AV42selected","fld":"vSELECTED","hsh":true,"type":""},{"av":"AV43selectedRoomId","fld":"vSELECTEDROOMID","type":"guid"},{"av":"lblTbroomtitle_Caption","ctrl":"TBROOMTITLE","prop":"Caption"},{"av":"lblTbroomstate_Caption","ctrl":"TBROOMSTATE","prop":"Caption"},{"av":"lblTbroompeople_Caption","ctrl":"TBROOMPEOPLE","prop":"Caption"},{"av":"lblTbroomwaiting_Caption","ctrl":"TBROOMWAITING","prop":"Caption"},{"ctrl":"HERE","prop":"Visible"},{"ctrl":"SIGNNOW","prop":"Visible"},{"ctrl":"SIGN","prop":"Visible"},{"ctrl":"ABORT","prop":"Visible"},{"av":"AV39chatLogView","fld":"vCHATLOGVIEW","type":"vchar"},{"av":"tblChatinputtable_Visible","ctrl":"CHATINPUTTABLE","prop":"Visible"},{"av":"divRoomtable_Visible","ctrl":"ROOMTABLE","prop":"Visible"}]}""");
         setEventMetadata("'LEAVE'","""{"handler":"E193T2","iparms":[{"av":"GRIDROOMS_nFirstRecordOnPage","type":"int"},{"av":"GRIDROOMS_nEOF","type":"int"},{"av":"subGridrooms_Rows","ctrl":"GRIDROOMS","prop":"Rows"},{"av":"AV6groupId","fld":"vGROUPID","type":"guid"},{"av":"AV30ownerAlone","fld":"vOWNERALONE","hsh":true,"type":"boolean"},{"av":"AV43selectedRoomId","fld":"vSELECTEDROOMID","type":"guid"},{"av":"AV42selected","fld":"vSELECTED","hsh":true,"type":""},{"av":"AV7lines","fld":"vLINES","grid":96,"type":""},{"av":"nGXsfl_96_idx","ctrl":"GRID","prop":"GridCurrRow","grid":96},{"av":"nRC_GXsfl_96","ctrl":"GRIDROOMS","prop":"GridRC","grid":96,"type":"int"},{"av":"AV16sendAllCoins","fld":"vSENDALLCOINS","type":"boolean"},{"av":"sPrefix","type":"char"},{"av":"AV5error","fld":"vERROR","type":"svchar"}]""");
         setEventMetadata("'LEAVE'",""","oparms":[{"av":"AV5error","fld":"vERROR","type":"svchar"},{"av":"AV7lines","fld":"vLINES","grid":96,"type":""},{"av":"nGXsfl_96_idx","ctrl":"GRID","prop":"GridCurrRow","grid":96},{"av":"GRIDROOMS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_96","ctrl":"GRIDROOMS","prop":"GridRC","grid":96,"type":"int"},{"av":"lblTbempty_Caption","ctrl":"TBEMPTY","prop":"Caption"},{"av":"AV42selected","fld":"vSELECTED","hsh":true,"type":""},{"av":"AV43selectedRoomId","fld":"vSELECTEDROOMID","type":"guid"},{"av":"lblTbroomtitle_Caption","ctrl":"TBROOMTITLE","prop":"Caption"},{"av":"lblTbroomstate_Caption","ctrl":"TBROOMSTATE","prop":"Caption"},{"av":"lblTbroompeople_Caption","ctrl":"TBROOMPEOPLE","prop":"Caption"},{"av":"lblTbroomwaiting_Caption","ctrl":"TBROOMWAITING","prop":"Caption"},{"ctrl":"HERE","prop":"Visible"},{"ctrl":"SIGNNOW","prop":"Visible"},{"ctrl":"SIGN","prop":"Visible"},{"ctrl":"ABORT","prop":"Visible"},{"av":"AV39chatLogView","fld":"vCHATLOGVIEW","type":"vchar"},{"av":"tblChatinputtable_Visible","ctrl":"CHATINPUTTABLE","prop":"Visible"},{"av":"divRoomtable_Visible","ctrl":"ROOMTABLE","prop":"Visible"}]}""");
         setEventMetadata("'SEND CHAT'","""{"handler":"E203T2","iparms":[{"av":"GRIDROOMS_nFirstRecordOnPage","type":"int"},{"av":"GRIDROOMS_nEOF","type":"int"},{"av":"subGridrooms_Rows","ctrl":"GRIDROOMS","prop":"Rows"},{"av":"AV6groupId","fld":"vGROUPID","type":"guid"},{"av":"AV30ownerAlone","fld":"vOWNERALONE","hsh":true,"type":"boolean"},{"av":"AV43selectedRoomId","fld":"vSELECTEDROOMID","type":"guid"},{"av":"AV42selected","fld":"vSELECTED","hsh":true,"type":""},{"av":"AV7lines","fld":"vLINES","grid":96,"type":""},{"av":"nGXsfl_96_idx","ctrl":"GRID","prop":"GridCurrRow","grid":96},{"av":"nRC_GXsfl_96","ctrl":"GRIDROOMS","prop":"GridRC","grid":96,"type":"int"},{"av":"AV16sendAllCoins","fld":"vSENDALLCOINS","type":"boolean"},{"av":"sPrefix","type":"char"},{"av":"AV36chatText","fld":"vCHATTEXT","type":"char"},{"av":"AV5error","fld":"vERROR","type":"svchar"}]""");
         setEventMetadata("'SEND CHAT'",""","oparms":[{"av":"AV5error","fld":"vERROR","type":"svchar"},{"av":"AV36chatText","fld":"vCHATTEXT","type":"char"},{"av":"AV7lines","fld":"vLINES","grid":96,"type":""},{"av":"nGXsfl_96_idx","ctrl":"GRID","prop":"GridCurrRow","grid":96},{"av":"GRIDROOMS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_96","ctrl":"GRIDROOMS","prop":"GridRC","grid":96,"type":"int"},{"av":"lblTbempty_Caption","ctrl":"TBEMPTY","prop":"Caption"},{"av":"AV42selected","fld":"vSELECTED","hsh":true,"type":""},{"av":"AV43selectedRoomId","fld":"vSELECTEDROOMID","type":"guid"},{"av":"lblTbroomtitle_Caption","ctrl":"TBROOMTITLE","prop":"Caption"},{"av":"lblTbroomstate_Caption","ctrl":"TBROOMSTATE","prop":"Caption"},{"av":"lblTbroompeople_Caption","ctrl":"TBROOMPEOPLE","prop":"Caption"},{"av":"lblTbroomwaiting_Caption","ctrl":"TBROOMWAITING","prop":"Caption"},{"ctrl":"HERE","prop":"Visible"},{"ctrl":"SIGNNOW","prop":"Visible"},{"ctrl":"SIGN","prop":"Visible"},{"ctrl":"ABORT","prop":"Visible"},{"av":"AV39chatLogView","fld":"vCHATLOGVIEW","type":"vchar"},{"av":"tblChatinputtable_Visible","ctrl":"CHATINPUTTABLE","prop":"Visible"},{"av":"divRoomtable_Visible","ctrl":"ROOMTABLE","prop":"Visible"}]}""");
         setEventMetadata("GLOBALEVENTS.RECEIVECHAT","""{"handler":"E213T2","iparms":[{"av":"GRIDROOMS_nFirstRecordOnPage","type":"int"},{"av":"GRIDROOMS_nEOF","type":"int"},{"av":"subGridrooms_Rows","ctrl":"GRIDROOMS","prop":"Rows"},{"av":"AV6groupId","fld":"vGROUPID","type":"guid"},{"av":"AV30ownerAlone","fld":"vOWNERALONE","hsh":true,"type":"boolean"},{"av":"AV43selectedRoomId","fld":"vSELECTEDROOMID","type":"guid"},{"av":"AV42selected","fld":"vSELECTED","hsh":true,"type":""},{"av":"AV7lines","fld":"vLINES","grid":96,"type":""},{"av":"nGXsfl_96_idx","ctrl":"GRID","prop":"GridCurrRow","grid":96},{"av":"nRC_GXsfl_96","ctrl":"GRIDROOMS","prop":"GridRC","grid":96,"type":"int"},{"av":"AV16sendAllCoins","fld":"vSENDALLCOINS","type":"boolean"},{"av":"sPrefix","type":"char"},{"av":"AV13formOpen","fld":"vFORMOPEN","type":"boolean"}]""");
         setEventMetadata("GLOBALEVENTS.RECEIVECHAT",""","oparms":[{"av":"AV5error","fld":"vERROR","type":"svchar"},{"av":"AV7lines","fld":"vLINES","grid":96,"type":""},{"av":"nGXsfl_96_idx","ctrl":"GRID","prop":"GridCurrRow","grid":96},{"av":"GRIDROOMS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_96","ctrl":"GRIDROOMS","prop":"GridRC","grid":96,"type":"int"},{"av":"lblTbempty_Caption","ctrl":"TBEMPTY","prop":"Caption"},{"av":"AV42selected","fld":"vSELECTED","hsh":true,"type":""},{"av":"AV43selectedRoomId","fld":"vSELECTEDROOMID","type":"guid"},{"av":"lblTbroomtitle_Caption","ctrl":"TBROOMTITLE","prop":"Caption"},{"av":"lblTbroomstate_Caption","ctrl":"TBROOMSTATE","prop":"Caption"},{"av":"lblTbroompeople_Caption","ctrl":"TBROOMPEOPLE","prop":"Caption"},{"av":"lblTbroomwaiting_Caption","ctrl":"TBROOMWAITING","prop":"Caption"},{"ctrl":"HERE","prop":"Visible"},{"ctrl":"SIGNNOW","prop":"Visible"},{"ctrl":"SIGN","prop":"Visible"},{"ctrl":"ABORT","prop":"Visible"},{"av":"AV39chatLogView","fld":"vCHATLOGVIEW","type":"vchar"},{"av":"tblChatinputtable_Visible","ctrl":"CHATINPUTTABLE","prop":"Visible"},{"av":"divRoomtable_Visible","ctrl":"ROOMTABLE","prop":"Visible"}]}""");
         setEventMetadata("GRIDROOMS_FIRSTPAGE","""{"handler":"subgridrooms_firstpage","iparms":[{"av":"GRIDROOMS_nFirstRecordOnPage","type":"int"},{"av":"GRIDROOMS_nEOF","type":"int"},{"av":"subGridrooms_Rows","ctrl":"GRIDROOMS","prop":"Rows"},{"av":"AV7lines","fld":"vLINES","grid":96,"type":""},{"av":"nGXsfl_96_idx","ctrl":"GRID","prop":"GridCurrRow","grid":96},{"av":"nRC_GXsfl_96","ctrl":"GRIDROOMS","prop":"GridRC","grid":96,"type":"int"},{"av":"sPrefix","type":"char"},{"av":"AV6groupId","fld":"vGROUPID","type":"guid"},{"av":"AV30ownerAlone","fld":"vOWNERALONE","hsh":true,"type":"boolean"},{"av":"AV43selectedRoomId","fld":"vSELECTEDROOMID","type":"guid"},{"av":"AV42selected","fld":"vSELECTED","hsh":true,"type":""},{"av":"AV16sendAllCoins","fld":"vSENDALLCOINS","type":"boolean"}]""");
         setEventMetadata("GRIDROOMS_FIRSTPAGE",""","oparms":[{"av":"AV5error","fld":"vERROR","type":"svchar"},{"av":"AV7lines","fld":"vLINES","grid":96,"type":""},{"av":"nGXsfl_96_idx","ctrl":"GRID","prop":"GridCurrRow","grid":96},{"av":"GRIDROOMS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_96","ctrl":"GRIDROOMS","prop":"GridRC","grid":96,"type":"int"},{"av":"lblTbempty_Caption","ctrl":"TBEMPTY","prop":"Caption"},{"av":"AV42selected","fld":"vSELECTED","hsh":true,"type":""},{"av":"AV43selectedRoomId","fld":"vSELECTEDROOMID","type":"guid"},{"av":"lblTbroomtitle_Caption","ctrl":"TBROOMTITLE","prop":"Caption"},{"av":"lblTbroomstate_Caption","ctrl":"TBROOMSTATE","prop":"Caption"},{"av":"lblTbroompeople_Caption","ctrl":"TBROOMPEOPLE","prop":"Caption"},{"av":"lblTbroomwaiting_Caption","ctrl":"TBROOMWAITING","prop":"Caption"},{"ctrl":"HERE","prop":"Visible"},{"ctrl":"SIGNNOW","prop":"Visible"},{"ctrl":"SIGN","prop":"Visible"},{"ctrl":"ABORT","prop":"Visible"},{"av":"AV39chatLogView","fld":"vCHATLOGVIEW","type":"vchar"},{"av":"tblChatinputtable_Visible","ctrl":"CHATINPUTTABLE","prop":"Visible"},{"av":"divRoomtable_Visible","ctrl":"ROOMTABLE","prop":"Visible"}]}""");
         setEventMetadata("GRIDROOMS_PREVPAGE","""{"handler":"subgridrooms_previouspage","iparms":[{"av":"GRIDROOMS_nFirstRecordOnPage","type":"int"},{"av":"GRIDROOMS_nEOF","type":"int"},{"av":"subGridrooms_Rows","ctrl":"GRIDROOMS","prop":"Rows"},{"av":"AV7lines","fld":"vLINES","grid":96,"type":""},{"av":"nGXsfl_96_idx","ctrl":"GRID","prop":"GridCurrRow","grid":96},{"av":"nRC_GXsfl_96","ctrl":"GRIDROOMS","prop":"GridRC","grid":96,"type":"int"},{"av":"sPrefix","type":"char"},{"av":"AV6groupId","fld":"vGROUPID","type":"guid"},{"av":"AV30ownerAlone","fld":"vOWNERALONE","hsh":true,"type":"boolean"},{"av":"AV43selectedRoomId","fld":"vSELECTEDROOMID","type":"guid"},{"av":"AV42selected","fld":"vSELECTED","hsh":true,"type":""},{"av":"AV16sendAllCoins","fld":"vSENDALLCOINS","type":"boolean"}]""");
         setEventMetadata("GRIDROOMS_PREVPAGE",""","oparms":[{"av":"AV5error","fld":"vERROR","type":"svchar"},{"av":"AV7lines","fld":"vLINES","grid":96,"type":""},{"av":"nGXsfl_96_idx","ctrl":"GRID","prop":"GridCurrRow","grid":96},{"av":"GRIDROOMS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_96","ctrl":"GRIDROOMS","prop":"GridRC","grid":96,"type":"int"},{"av":"lblTbempty_Caption","ctrl":"TBEMPTY","prop":"Caption"},{"av":"AV42selected","fld":"vSELECTED","hsh":true,"type":""},{"av":"AV43selectedRoomId","fld":"vSELECTEDROOMID","type":"guid"},{"av":"lblTbroomtitle_Caption","ctrl":"TBROOMTITLE","prop":"Caption"},{"av":"lblTbroomstate_Caption","ctrl":"TBROOMSTATE","prop":"Caption"},{"av":"lblTbroompeople_Caption","ctrl":"TBROOMPEOPLE","prop":"Caption"},{"av":"lblTbroomwaiting_Caption","ctrl":"TBROOMWAITING","prop":"Caption"},{"ctrl":"HERE","prop":"Visible"},{"ctrl":"SIGNNOW","prop":"Visible"},{"ctrl":"SIGN","prop":"Visible"},{"ctrl":"ABORT","prop":"Visible"},{"av":"AV39chatLogView","fld":"vCHATLOGVIEW","type":"vchar"},{"av":"tblChatinputtable_Visible","ctrl":"CHATINPUTTABLE","prop":"Visible"},{"av":"divRoomtable_Visible","ctrl":"ROOMTABLE","prop":"Visible"}]}""");
         setEventMetadata("GRIDROOMS_NEXTPAGE","""{"handler":"subgridrooms_nextpage","iparms":[{"av":"GRIDROOMS_nFirstRecordOnPage","type":"int"},{"av":"GRIDROOMS_nEOF","type":"int"},{"av":"subGridrooms_Rows","ctrl":"GRIDROOMS","prop":"Rows"},{"av":"AV7lines","fld":"vLINES","grid":96,"type":""},{"av":"nGXsfl_96_idx","ctrl":"GRID","prop":"GridCurrRow","grid":96},{"av":"nRC_GXsfl_96","ctrl":"GRIDROOMS","prop":"GridRC","grid":96,"type":"int"},{"av":"sPrefix","type":"char"},{"av":"AV6groupId","fld":"vGROUPID","type":"guid"},{"av":"AV30ownerAlone","fld":"vOWNERALONE","hsh":true,"type":"boolean"},{"av":"AV43selectedRoomId","fld":"vSELECTEDROOMID","type":"guid"},{"av":"AV42selected","fld":"vSELECTED","hsh":true,"type":""},{"av":"AV16sendAllCoins","fld":"vSENDALLCOINS","type":"boolean"}]""");
         setEventMetadata("GRIDROOMS_NEXTPAGE",""","oparms":[{"av":"AV5error","fld":"vERROR","type":"svchar"},{"av":"AV7lines","fld":"vLINES","grid":96,"type":""},{"av":"nGXsfl_96_idx","ctrl":"GRID","prop":"GridCurrRow","grid":96},{"av":"GRIDROOMS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_96","ctrl":"GRIDROOMS","prop":"GridRC","grid":96,"type":"int"},{"av":"lblTbempty_Caption","ctrl":"TBEMPTY","prop":"Caption"},{"av":"AV42selected","fld":"vSELECTED","hsh":true,"type":""},{"av":"AV43selectedRoomId","fld":"vSELECTEDROOMID","type":"guid"},{"av":"lblTbroomtitle_Caption","ctrl":"TBROOMTITLE","prop":"Caption"},{"av":"lblTbroomstate_Caption","ctrl":"TBROOMSTATE","prop":"Caption"},{"av":"lblTbroompeople_Caption","ctrl":"TBROOMPEOPLE","prop":"Caption"},{"av":"lblTbroomwaiting_Caption","ctrl":"TBROOMWAITING","prop":"Caption"},{"ctrl":"HERE","prop":"Visible"},{"ctrl":"SIGNNOW","prop":"Visible"},{"ctrl":"SIGN","prop":"Visible"},{"ctrl":"ABORT","prop":"Visible"},{"av":"AV39chatLogView","fld":"vCHATLOGVIEW","type":"vchar"},{"av":"tblChatinputtable_Visible","ctrl":"CHATINPUTTABLE","prop":"Visible"},{"av":"divRoomtable_Visible","ctrl":"ROOMTABLE","prop":"Visible"}]}""");
         setEventMetadata("GRIDROOMS_LASTPAGE","""{"handler":"subgridrooms_lastpage","iparms":[{"av":"GRIDROOMS_nFirstRecordOnPage","type":"int"},{"av":"GRIDROOMS_nEOF","type":"int"},{"av":"subGridrooms_Rows","ctrl":"GRIDROOMS","prop":"Rows"},{"av":"AV7lines","fld":"vLINES","grid":96,"type":""},{"av":"nGXsfl_96_idx","ctrl":"GRID","prop":"GridCurrRow","grid":96},{"av":"nRC_GXsfl_96","ctrl":"GRIDROOMS","prop":"GridRC","grid":96,"type":"int"},{"av":"sPrefix","type":"char"},{"av":"AV6groupId","fld":"vGROUPID","type":"guid"},{"av":"AV30ownerAlone","fld":"vOWNERALONE","hsh":true,"type":"boolean"},{"av":"AV43selectedRoomId","fld":"vSELECTEDROOMID","type":"guid"},{"av":"AV42selected","fld":"vSELECTED","hsh":true,"type":""},{"av":"AV16sendAllCoins","fld":"vSENDALLCOINS","type":"boolean"}]""");
         setEventMetadata("GRIDROOMS_LASTPAGE",""","oparms":[{"av":"AV5error","fld":"vERROR","type":"svchar"},{"av":"AV7lines","fld":"vLINES","grid":96,"type":""},{"av":"nGXsfl_96_idx","ctrl":"GRID","prop":"GridCurrRow","grid":96},{"av":"GRIDROOMS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_96","ctrl":"GRIDROOMS","prop":"GridRC","grid":96,"type":"int"},{"av":"lblTbempty_Caption","ctrl":"TBEMPTY","prop":"Caption"},{"av":"AV42selected","fld":"vSELECTED","hsh":true,"type":""},{"av":"AV43selectedRoomId","fld":"vSELECTEDROOMID","type":"guid"},{"av":"lblTbroomtitle_Caption","ctrl":"TBROOMTITLE","prop":"Caption"},{"av":"lblTbroomstate_Caption","ctrl":"TBROOMSTATE","prop":"Caption"},{"av":"lblTbroompeople_Caption","ctrl":"TBROOMPEOPLE","prop":"Caption"},{"av":"lblTbroomwaiting_Caption","ctrl":"TBROOMWAITING","prop":"Caption"},{"ctrl":"HERE","prop":"Visible"},{"ctrl":"SIGNNOW","prop":"Visible"},{"ctrl":"SIGN","prop":"Visible"},{"ctrl":"ABORT","prop":"Visible"},{"av":"AV39chatLogView","fld":"vCHATLOGVIEW","type":"vchar"},{"av":"tblChatinputtable_Visible","ctrl":"CHATINPUTTABLE","prop":"Visible"},{"av":"divRoomtable_Visible","ctrl":"ROOMTABLE","prop":"Visible"}]}""");
         setEventMetadata("VALIDV_GXV2","""{"handler":"Validv_Gxv2","iparms":[]}""");
         setEventMetadata("NULL","""{"handler":"Validv_Open","iparms":[]}""");
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
         wcpOAV6groupId = Guid.Empty;
         gxfirstwebparm = "";
         gxfirstwebparm_bkp = "";
         sPrefix = "";
         AV43selectedRoomId = Guid.Empty;
         AV42selected = new GeneXus.Programs.wallet.registered.SdtMuSig2RoomLine(context);
         AV7lines = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSig2RoomLine>( context, "MuSig2RoomLine", "distributedcryptography");
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         AV25errorTitle = "";
         AV5error = "";
         AV10signRoomId = Guid.Empty;
         GX_FocusControl = "";
         lblTbnew_Jsonclick = "";
         TempTags = "";
         ClassString = "";
         StyleString = "";
         AV18sendTo = "";
         AV12description = "";
         AV38ownerPassword = "";
         bttPayalone_Jsonclick = "";
         lblTbsign_Jsonclick = "";
         AV14password = "";
         lblTbempty_Jsonclick = "";
         GridroomsContainer = new GXWebGrid( context);
         sStyleString = "";
         lblTbroomtitle_Jsonclick = "";
         lblTbroomstate_Jsonclick = "";
         lblTbroompeople_Jsonclick = "";
         lblTbroomwaiting_Jsonclick = "";
         AV39chatLogView = "";
         Form = new GXWebForm();
         sXEvt = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         AV45mark = "";
         AV41open = "";
         AV36chatText = "";
         AV37oneLine = new GeneXus.Programs.wallet.registered.SdtMuSig2RoomLine(context);
         GridroomsRow = new GXWebRow();
         AV15roomId = Guid.Empty;
         AV44txId = "";
         AV20walletInfo = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         GXt_SdtWalletInfo3 = new GeneXus.Programs.wallet.SdtWalletInfo(context);
         GXt_char1 = "";
         bttSendchat_Jsonclick = "";
         bttHere_Jsonclick = "";
         bttSignnow_Jsonclick = "";
         bttSign_Jsonclick = "";
         bttAbort_Jsonclick = "";
         bttNewpayment_Jsonclick = "";
         bttRefreshrooms_Jsonclick = "";
         bttConfirmsign_Jsonclick = "";
         bttCancelsign_Jsonclick = "";
         bttOpenroom_Jsonclick = "";
         bttEstimatefee_Jsonclick = "";
         bttCancelnew_Jsonclick = "";
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         sCtrlAV6groupId = "";
         subGridrooms_Linesclass = "";
         ROClassString = "";
         GridroomsColumn = new GXWebColumn();
         /* GeneXus formulas. */
         edtavCtlroomid_Enabled = 0;
         edtavMark_Enabled = 0;
         edtavCtlcreated_Enabled = 0;
         edtavCtlopenedby_Enabled = 0;
         edtavCtldescription_Enabled = 0;
         edtavCtlamounttext_Enabled = 0;
         edtavCtlstateshort_Enabled = 0;
         edtavOpen_Enabled = 0;
         edtavChatlogview_Enabled = 0;
      }

      private short GRIDROOMS_nEOF ;
      private short nGotPars ;
      private short GxWebError ;
      private short nDynComponent ;
      private short wbEnd ;
      private short wbStart ;
      private short nDraw ;
      private short nDoneStart ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short subGridrooms_Backcolorstyle ;
      private short AV28groupType ;
      private short GXt_int2 ;
      private short nGXWrapped ;
      private short subGridrooms_Backstyle ;
      private short subGridrooms_Titlebackstyle ;
      private short subGridrooms_Allowselection ;
      private short subGridrooms_Allowhovering ;
      private short subGridrooms_Allowcollapsing ;
      private short subGridrooms_Collapsed ;
      private int nRC_GXsfl_96 ;
      private int subGridrooms_Rows ;
      private int nGXsfl_96_idx=1 ;
      private int edtavCtlroomid_Enabled ;
      private int edtavMark_Enabled ;
      private int edtavCtlcreated_Enabled ;
      private int edtavCtlopenedby_Enabled ;
      private int edtavCtldescription_Enabled ;
      private int edtavCtlamounttext_Enabled ;
      private int edtavCtlstateshort_Enabled ;
      private int edtavOpen_Enabled ;
      private int edtavChatlogview_Enabled ;
      private int divNewtable_Visible ;
      private int edtavSendcoins_Enabled ;
      private int edtavSendto_Enabled ;
      private int edtavDescription_Enabled ;
      private int divOwnerfeetable_Visible ;
      private int edtavManualfee_Enabled ;
      private int edtavOwnerpassword_Enabled ;
      private int divSigntable_Visible ;
      private int edtavPassword_Enabled ;
      private int divListtable_Visible ;
      private int AV46GXV1 ;
      private int divRoomtable_Visible ;
      private int subGridrooms_Islastpage ;
      private int GRIDROOMS_nGridOutOfScope ;
      private int nGXsfl_96_fel_idx=1 ;
      private int AV53GXV8 ;
      private int AV54GXV9 ;
      private int bttHere_Visible ;
      private int bttSignnow_Visible ;
      private int bttSign_Visible ;
      private int bttAbort_Visible ;
      private int tblChatinputtable_Visible ;
      private int nGXsfl_96_bak_idx=1 ;
      private int bttEstimatefee_Visible ;
      private int edtavChattext_Enabled ;
      private int bttOpenroom_Visible ;
      private int idxLst ;
      private int subGridrooms_Backcolor ;
      private int subGridrooms_Allbackcolor ;
      private int subGridrooms_Titlebackcolor ;
      private int subGridrooms_Selectedindex ;
      private int subGridrooms_Selectioncolor ;
      private int subGridrooms_Hoveringcolor ;
      private long GRIDROOMS_nFirstRecordOnPage ;
      private long AV23economicalBlocks ;
      private long AV31standarBlocks ;
      private long AV26fastestBlocks ;
      private long GRIDROOMS_nCurrentRecord ;
      private long GRIDROOMS_nRecordCount ;
      private long AV34virtualSize ;
      private decimal AV24economicalFee ;
      private decimal AV32standardFee ;
      private decimal AV27fastestFee ;
      private decimal AV17sendCoins ;
      private decimal AV33userFee ;
      private decimal AV29manualFee ;
      private decimal AV19totalBalance ;
      private decimal GXt_decimal4 ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sPrefix ;
      private string sCompPrefix ;
      private string sSFPrefix ;
      private string sGXsfl_96_idx="0001" ;
      private string edtavCtlroomid_Internalname ;
      private string edtavMark_Internalname ;
      private string edtavCtlcreated_Internalname ;
      private string edtavCtlopenedby_Internalname ;
      private string edtavCtldescription_Internalname ;
      private string edtavCtlamounttext_Internalname ;
      private string edtavCtlstateshort_Internalname ;
      private string edtavOpen_Internalname ;
      private string edtavChatlogview_Internalname ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string AV25errorTitle ;
      private string GX_FocusControl ;
      private string divMaintable_Internalname ;
      private string divNewtable_Internalname ;
      private string lblTbnew_Internalname ;
      private string lblTbnew_Caption ;
      private string lblTbnew_Jsonclick ;
      private string chkavSendallcoins_Internalname ;
      private string TempTags ;
      private string ClassString ;
      private string StyleString ;
      private string edtavSendcoins_Internalname ;
      private string edtavSendcoins_Jsonclick ;
      private string edtavSendto_Internalname ;
      private string AV18sendTo ;
      private string edtavDescription_Internalname ;
      private string divOwnerfeetable_Internalname ;
      private string cmbavUserfee_Internalname ;
      private string cmbavUserfee_Jsonclick ;
      private string edtavManualfee_Internalname ;
      private string edtavManualfee_Jsonclick ;
      private string edtavOwnerpassword_Internalname ;
      private string AV38ownerPassword ;
      private string edtavOwnerpassword_Jsonclick ;
      private string bttPayalone_Internalname ;
      private string bttPayalone_Jsonclick ;
      private string divSigntable_Internalname ;
      private string lblTbsign_Internalname ;
      private string lblTbsign_Caption ;
      private string lblTbsign_Jsonclick ;
      private string edtavPassword_Internalname ;
      private string AV14password ;
      private string edtavPassword_Jsonclick ;
      private string divListtable_Internalname ;
      private string lblTbempty_Internalname ;
      private string lblTbempty_Caption ;
      private string lblTbempty_Jsonclick ;
      private string sStyleString ;
      private string subGridrooms_Internalname ;
      private string divRoomtable_Internalname ;
      private string lblTbroomtitle_Internalname ;
      private string lblTbroomtitle_Caption ;
      private string lblTbroomtitle_Jsonclick ;
      private string lblTbroomstate_Internalname ;
      private string lblTbroomstate_Caption ;
      private string lblTbroomstate_Jsonclick ;
      private string lblTbroompeople_Internalname ;
      private string lblTbroompeople_Caption ;
      private string lblTbroompeople_Jsonclick ;
      private string lblTbroomwaiting_Internalname ;
      private string lblTbroomwaiting_Caption ;
      private string lblTbroomwaiting_Jsonclick ;
      private string sXEvt ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string sGXsfl_96_fel_idx="0001" ;
      private string AV36chatText ;
      private string edtavChattext_Internalname ;
      private string bttNewpayment_Caption ;
      private string bttNewpayment_Internalname ;
      private string bttHere_Internalname ;
      private string bttSignnow_Internalname ;
      private string bttSign_Internalname ;
      private string bttAbort_Internalname ;
      private string tblChatinputtable_Internalname ;
      private string bttEstimatefee_Internalname ;
      private string GXt_char1 ;
      private string edtavChattext_Jsonclick ;
      private string bttSendchat_Internalname ;
      private string bttSendchat_Jsonclick ;
      private string tblRoombuttons_Internalname ;
      private string bttHere_Jsonclick ;
      private string bttSignnow_Jsonclick ;
      private string bttSign_Jsonclick ;
      private string bttAbort_Jsonclick ;
      private string tblButtonstable_Internalname ;
      private string bttNewpayment_Jsonclick ;
      private string bttRefreshrooms_Internalname ;
      private string bttRefreshrooms_Jsonclick ;
      private string tblSignbuttons_Internalname ;
      private string bttConfirmsign_Internalname ;
      private string bttConfirmsign_Jsonclick ;
      private string bttCancelsign_Internalname ;
      private string bttCancelsign_Jsonclick ;
      private string tblNewbuttons_Internalname ;
      private string bttOpenroom_Internalname ;
      private string bttOpenroom_Jsonclick ;
      private string bttEstimatefee_Jsonclick ;
      private string bttCancelnew_Internalname ;
      private string bttCancelnew_Jsonclick ;
      private string sCtrlAV6groupId ;
      private string subGridrooms_Class ;
      private string subGridrooms_Linesclass ;
      private string ROClassString ;
      private string edtavCtlroomid_Jsonclick ;
      private string edtavMark_Jsonclick ;
      private string edtavCtlcreated_Jsonclick ;
      private string edtavCtlopenedby_Jsonclick ;
      private string edtavCtldescription_Jsonclick ;
      private string edtavCtlamounttext_Jsonclick ;
      private string edtavCtlstateshort_Jsonclick ;
      private string edtavOpen_Jsonclick ;
      private string subGridrooms_Header ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool AV30ownerAlone ;
      private bool AV16sendAllCoins ;
      private bool bGXsfl_96_Refreshing=false ;
      private bool AV22checked ;
      private bool AV21broadcast ;
      private bool AV13formOpen ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool gx_refresh_fired ;
      private bool gx_BV96 ;
      private bool AV40found ;
      private string AV39chatLogView ;
      private string AV5error ;
      private string AV12description ;
      private string AV45mark ;
      private string AV41open ;
      private string AV44txId ;
      private Guid AV6groupId ;
      private Guid wcpOAV6groupId ;
      private Guid AV43selectedRoomId ;
      private Guid AV10signRoomId ;
      private Guid AV15roomId ;
      private GXWebGrid GridroomsContainer ;
      private GXWebRow GridroomsRow ;
      private GXWebColumn GridroomsColumn ;
      private GXWebForm Form ;
      private IGxDataStore dsDefault ;
      private GXCheckbox chkavSendallcoins ;
      private GXCombobox cmbavUserfee ;
      private GeneXus.Programs.wallet.registered.SdtMuSig2RoomLine AV42selected ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtMuSig2RoomLine> AV7lines ;
      private GeneXus.Programs.wallet.registered.SdtMuSig2RoomLine AV37oneLine ;
      private GeneXus.Programs.wallet.SdtWalletInfo AV20walletInfo ;
      private GeneXus.Programs.wallet.SdtWalletInfo GXt_SdtWalletInfo3 ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
