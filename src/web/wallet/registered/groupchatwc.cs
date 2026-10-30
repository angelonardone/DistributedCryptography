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
   public class groupchatwc : GXWebComponent
   {
      public groupchatwc( )
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

      public groupchatwc( IGxContext context )
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
               else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxNewRow_"+"Gridchat") == 0 )
               {
                  gxnrGridchat_newrow_invoke( ) ;
                  return  ;
               }
               else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxGridRefresh_"+"Gridchat") == 0 )
               {
                  gxgrGridchat_refresh_invoke( ) ;
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

      protected void gxnrGridchat_newrow_invoke( )
      {
         nRC_GXsfl_12 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_12"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_12_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_12_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_12_idx = GetPar( "sGXsfl_12_idx");
         sPrefix = GetPar( "sPrefix");
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxnrGridchat_newrow( ) ;
         /* End function gxnrGridchat_newrow_invoke */
      }

      protected void gxgrGridchat_refresh_invoke( )
      {
         AV6groupId = StringUtil.StrToGuid( GetPar( "groupId"));
         ajax_req_read_hidden_sdt(GetNextPar( ), AV7lines);
         sPrefix = GetPar( "sPrefix");
         init_default_properties( ) ;
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrGridchat_refresh( AV6groupId, AV7lines, sPrefix) ;
         AddString( context.getJSONResponse( )) ;
         /* End function gxgrGridchat_refresh_invoke */
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
            PA3R2( ) ;
            if ( ( GxWebError == 0 ) && ! isAjaxCallMode( ) )
            {
               /* GeneXus formulas. */
               edtavCtlwho_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlwho_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlwho_Enabled), 5, 0), !bGXsfl_12_Refreshing);
               edtavCtldatetime_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtldatetime_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtldatetime_Enabled), 5, 0), !bGXsfl_12_Refreshing);
               edtavCtltext_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtltext_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtltext_Enabled), 5, 0), !bGXsfl_12_Refreshing);
               WS3R2( ) ;
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
            context.SendWebValue( "Group Chat") ;
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
         context.AddJavascriptSource("calendar.js", "?"+context.GetBuildNumber( 1550520), false, true, false);
         context.AddJavascriptSource("calendar-setup.js", "?"+context.GetBuildNumber( 1550520), false, true, false);
         context.AddJavascriptSource("calendar-en.js", "?"+context.GetBuildNumber( 1550520), false, true, false);
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
            context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wallet.registered.groupchatwc", new object[] {UrlEncode(AV6groupId.ToString())}, new string[] {"groupId"}) +"\">") ;
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
         GxWebStd.gx_hidden_field( context, sPrefix+"nRC_GXsfl_12", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_12), 8, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"wcpOAV6groupId", wcpOAV6groupId.ToString());
         GxWebStd.gx_hidden_field( context, sPrefix+"vGROUPID", AV6groupId.ToString());
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vLINES", AV7lines);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vLINES", AV7lines);
         }
      }

      protected void RenderHtmlCloseForm3R2( )
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
         return "Wallet.registered.GroupChatWC" ;
      }

      public override string GetPgmdesc( )
      {
         return "Group Chat" ;
      }

      protected void WB3R0( )
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
               GxWebStd.gx_hidden_field( context, sPrefix+"_CMPPGM", "wallet.registered.groupchatwc");
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
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTbempty_Internalname, lblTbempty_Caption, "", "", lblTbempty_Jsonclick, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 0, "HLP_Wallet/registered/GroupChatWC.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divChattable_Internalname, 1, 0, "px", 0, "px", "gxchattable", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /*  Grid Control  */
            GridchatContainer.SetIsFreestyle(true);
            GridchatContainer.SetWrapped(nGXWrapped);
            StartGridControl12( ) ;
         }
         if ( wbEnd == 12 )
         {
            wbEnd = 0;
            nRC_GXsfl_12 = (int)(nGXsfl_12_idx-1);
            if ( GridchatContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "</table>") ;
               context.WriteHtmlText( "</div>") ;
            }
            else
            {
               AV9GXV1 = nGXsfl_12_idx;
               sStyleString = "";
               context.WriteHtmlText( "<div id=\""+sPrefix+"GridchatContainer"+"Div\" "+sStyleString+">"+"</div>") ;
               context.httpAjaxContext.ajax_rsp_assign_grid(sPrefix+"_"+"Gridchat", GridchatContainer, subGridchat_Internalname);
               if ( ! isAjaxCallMode( ) && ! context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, sPrefix+"GridchatContainerData", GridchatContainer.ToJavascriptSource());
               }
               if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, sPrefix+"GridchatContainerData"+"V", GridchatContainer.GridValuesHidden());
               }
               else
               {
                  context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+sPrefix+"GridchatContainerData"+"V"+"\" value='"+GridchatContainer.GridValuesHidden()+"'/>") ;
               }
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            wb_table1_33_3R2( true) ;
         }
         else
         {
            wb_table1_33_3R2( false) ;
         }
         return  ;
      }

      protected void wb_table1_33_3R2e( bool wbgen )
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
         }
         if ( wbEnd == 12 )
         {
            wbEnd = 0;
            if ( isFullAjaxMode( ) )
            {
               if ( GridchatContainer.GetWrapped() == 1 )
               {
                  context.WriteHtmlText( "</table>") ;
                  context.WriteHtmlText( "</div>") ;
               }
               else
               {
                  AV9GXV1 = nGXsfl_12_idx;
                  sStyleString = "";
                  context.WriteHtmlText( "<div id=\""+sPrefix+"GridchatContainer"+"Div\" "+sStyleString+">"+"</div>") ;
                  context.httpAjaxContext.ajax_rsp_assign_grid(sPrefix+"_"+"Gridchat", GridchatContainer, subGridchat_Internalname);
                  if ( ! isAjaxCallMode( ) && ! context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, sPrefix+"GridchatContainerData", GridchatContainer.ToJavascriptSource());
                  }
                  if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, sPrefix+"GridchatContainerData"+"V", GridchatContainer.GridValuesHidden());
                  }
                  else
                  {
                     context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+sPrefix+"GridchatContainerData"+"V"+"\" value='"+GridchatContainer.GridValuesHidden()+"'/>") ;
                  }
               }
            }
         }
         wbLoad = true;
      }

      protected void START3R2( )
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
            Form.Meta.addItem("description", "Group Chat", 0) ;
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
               STRUP3R0( ) ;
            }
         }
      }

      protected void WS3R2( )
      {
         START3R2( ) ;
         EVT3R2( ) ;
      }

      protected void EVT3R2( )
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
                                 STRUP3R0( ) ;
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
                           else if ( StringUtil.StrCmp(sEvt, "'SEND'") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP3R0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Send' */
                                    E113R2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "GLOBALEVENTS.RECEIVECHAT") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP3R0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    E123R2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP3R0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    GX_FocusControl = edtavSendtext_Internalname;
                                    AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                 }
                              }
                              dynload_actions( ) ;
                           }
                        }
                        else
                        {
                           sEvtType = StringUtil.Right( sEvt, 4);
                           sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-4));
                           if ( ( StringUtil.StrCmp(StringUtil.Left( sEvt, 7), "REFRESH") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 13), "GRIDCHAT.LOAD") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "ENTER") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 6), "CANCEL") == 0 ) )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP3R0( ) ;
                              }
                              nGXsfl_12_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                              sGXsfl_12_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_12_idx), 4, 0), 4, "0");
                              SubsflControlProps_122( ) ;
                              AV9GXV1 = nGXsfl_12_idx;
                              if ( ( AV7lines.Count >= AV9GXV1 ) && ( AV9GXV1 > 0 ) )
                              {
                                 AV7lines.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtGroupChatLine)AV7lines.Item(AV9GXV1));
                              }
                              sEvtType = StringUtil.Right( sEvt, 1);
                              if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                              {
                                 sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                                 if ( StringUtil.StrCmp(sEvt, "REFRESH") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavSendtext_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: Refresh */
                                          E133R2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "GRIDCHAT.LOAD") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavSendtext_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: Gridchat.Load */
                                          E143R2 ();
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
                                       STRUP3R0( ) ;
                                    }
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavSendtext_Internalname;
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

      protected void WE3R2( )
      {
         if ( ! GxWebStd.gx_redirect( context) )
         {
            Rfr0gs = true;
            Refresh( ) ;
            if ( ! GxWebStd.gx_redirect( context) )
            {
               RenderHtmlCloseForm3R2( ) ;
            }
         }
      }

      protected void PA3R2( )
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
               GX_FocusControl = edtavSendtext_Internalname;
               AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
            }
            nDonePA = 1;
         }
      }

      protected void dynload_actions( )
      {
         /* End function dynload_actions */
      }

      protected void gxnrGridchat_newrow( )
      {
         GxWebStd.set_html_headers( context, 0, "", "");
         SubsflControlProps_122( ) ;
         while ( nGXsfl_12_idx <= nRC_GXsfl_12 )
         {
            sendrow_122( ) ;
            nGXsfl_12_idx = ((subGridchat_Islastpage==1)&&(nGXsfl_12_idx+1>subGridchat_fnc_Recordsperpage( )) ? 1 : nGXsfl_12_idx+1);
            sGXsfl_12_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_12_idx), 4, 0), 4, "0");
            SubsflControlProps_122( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( GridchatContainer)) ;
         /* End function gxnrGridchat_newrow */
      }

      protected void gxgrGridchat_refresh( Guid AV6groupId ,
                                           GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupChatLine> AV7lines ,
                                           string sPrefix )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         GRIDCHAT_nCurrentRecord = 0;
         RF3R2( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         /* End function gxgrGridchat_refresh */
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
         RF3R2( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         edtavCtlwho_Enabled = 0;
         edtavCtldatetime_Enabled = 0;
         edtavCtltext_Enabled = 0;
      }

      protected void RF3R2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            GridchatContainer.ClearRows();
         }
         wbStart = 12;
         /* Execute user event: Refresh */
         E133R2 ();
         nGXsfl_12_idx = 1;
         sGXsfl_12_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_12_idx), 4, 0), 4, "0");
         SubsflControlProps_122( ) ;
         bGXsfl_12_Refreshing = true;
         GridchatContainer.AddObjectProperty("GridName", "Gridchat");
         GridchatContainer.AddObjectProperty("CmpContext", sPrefix);
         GridchatContainer.AddObjectProperty("InMasterPage", "false");
         GridchatContainer.AddObjectProperty("Class", StringUtil.RTrim( "gxchatgrid"));
         GridchatContainer.AddObjectProperty("Class", "gxchatgrid");
         GridchatContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
         GridchatContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
         GridchatContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridchat_Backcolorstyle), 1, 0, ".", "")));
         GridchatContainer.PageSize = subGridchat_fnc_Recordsperpage( );
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            SubsflControlProps_122( ) ;
            /* Execute user event: Gridchat.Load */
            E143R2 ();
            wbEnd = 12;
            WB3R0( ) ;
         }
         bGXsfl_12_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes3R2( )
      {
      }

      protected int subGridchat_fnc_Pagecount( )
      {
         return (int)(-1) ;
      }

      protected int subGridchat_fnc_Recordcount( )
      {
         return (int)(-1) ;
      }

      protected int subGridchat_fnc_Recordsperpage( )
      {
         return (int)(-1) ;
      }

      protected int subGridchat_fnc_Currentpage( )
      {
         return (int)(-1) ;
      }

      protected void before_start_formulas( )
      {
         edtavCtlwho_Enabled = 0;
         edtavCtldatetime_Enabled = 0;
         edtavCtltext_Enabled = 0;
         fix_multi_value_controls( ) ;
      }

      protected void STRUP3R0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         context.wbGlbDoneStart = 1;
         nDoneStart = 1;
         /* After Start, stand alone formulas. */
         sXEvt = cgiGet( "_EventName");
         if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) )
         {
            /* Read saved SDTs. */
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"Lines"), AV7lines);
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"vLINES"), AV7lines);
            /* Read saved values. */
            nRC_GXsfl_12 = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"nRC_GXsfl_12"), ".", ","), 18, MidpointRounding.ToEven));
            wcpOAV6groupId = StringUtil.StrToGuid( cgiGet( sPrefix+"wcpOAV6groupId"));
            nRC_GXsfl_12 = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"nRC_GXsfl_12"), ".", ","), 18, MidpointRounding.ToEven));
            nGXsfl_12_fel_idx = 0;
            while ( nGXsfl_12_fel_idx < nRC_GXsfl_12 )
            {
               nGXsfl_12_fel_idx = ((subGridchat_Islastpage==1)&&(nGXsfl_12_fel_idx+1>subGridchat_fnc_Recordsperpage( )) ? 1 : nGXsfl_12_fel_idx+1);
               sGXsfl_12_fel_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_12_fel_idx), 4, 0), 4, "0");
               SubsflControlProps_fel_122( ) ;
               AV9GXV1 = nGXsfl_12_fel_idx;
               if ( ( AV7lines.Count >= AV9GXV1 ) && ( AV9GXV1 > 0 ) )
               {
                  AV7lines.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtGroupChatLine)AV7lines.Item(AV9GXV1));
               }
            }
            if ( nGXsfl_12_fel_idx == 0 )
            {
               nGXsfl_12_idx = 1;
               sGXsfl_12_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_12_idx), 4, 0), 4, "0");
               SubsflControlProps_122( ) ;
            }
            nGXsfl_12_fel_idx = 1;
            /* Read variables values. */
            AV8sendText = cgiGet( edtavSendtext_Internalname);
            AssignAttri(sPrefix, false, "AV8sendText", AV8sendText);
            /* Read subfile selected row values. */
            /* Read hidden variables. */
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         }
         else
         {
            dynload_actions( ) ;
         }
      }

      protected void E133R2( )
      {
         if ( gx_refresh_fired )
         {
            return  ;
         }
         gx_refresh_fired = true;
         /* Refresh Routine */
         returnInSub = false;
         new GeneXus.Programs.wallet.registered.getgroupchatlines(context ).execute(  AV6groupId, out  AV7lines, out  AV5error) ;
         gx_BV12 = true;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV5error)) )
         {
            this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"The group chat can not be read",(string)AV5error}, true);
         }
         if ( AV7lines.Count == 0 )
         {
            lblTbempty_Caption = "No messages yet. Everything you write here goes to every member of the group.";
            AssignProp(sPrefix, false, lblTbempty_Internalname, "Caption", lblTbempty_Caption, true);
         }
         else
         {
            lblTbempty_Caption = "";
            AssignProp(sPrefix, false, lblTbempty_Internalname, "Caption", lblTbempty_Caption, true);
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV7lines", AV7lines);
      }

      protected void E113R2( )
      {
         /* 'Send' Routine */
         returnInSub = false;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV8sendText))) )
         {
            new GeneXus.Programs.wallet.registered.sendgroupchat(context ).execute(  AV6groupId,  AV8sendText, out  AV5error) ;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV5error)) )
            {
               AV8sendText = "";
               AssignAttri(sPrefix, false, "AV8sendText", AV8sendText);
            }
            else
            {
               this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"error",(string)"The message was not sent",(string)AV5error}, true);
            }
            context.DoAjaxRefreshCmp(sPrefix);
         }
         /*  Sending Event outputs  */
         if ( gx_BV12 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV7lines", AV7lines);
            nGXsfl_12_bak_idx = nGXsfl_12_idx;
            gxgrGridchat_refresh( AV6groupId, AV7lines, sPrefix) ;
            nGXsfl_12_idx = nGXsfl_12_bak_idx;
            sGXsfl_12_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_12_idx), 4, 0), 4, "0");
            SubsflControlProps_122( ) ;
         }
      }

      private void E143R2( )
      {
         /* Gridchat_Load Routine */
         returnInSub = false;
         AV9GXV1 = 1;
         while ( AV9GXV1 <= AV7lines.Count )
         {
            AV7lines.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtGroupChatLine)AV7lines.Item(AV9GXV1));
            if ( ((GeneXus.Programs.wallet.registered.SdtGroupChatLine)(AV7lines.CurrentItem)).gxTpr_Mine )
            {
               divGridmessagestable_Class = "gxchatgrid-send-table";
               AssignProp(sPrefix, false, divGridmessagestable_Internalname, "Class", divGridmessagestable_Class, !bGXsfl_12_Refreshing);
               tblTablemessage_Class = "gxchatgrid-send-message";
               AssignProp(sPrefix, false, tblTablemessage_Internalname, "Class", tblTablemessage_Class, !bGXsfl_12_Refreshing);
               edtavCtlwho_Visible = 0;
            }
            else
            {
               divGridmessagestable_Class = "gxchatgrid-response-table";
               AssignProp(sPrefix, false, divGridmessagestable_Internalname, "Class", divGridmessagestable_Class, !bGXsfl_12_Refreshing);
               tblTablemessage_Class = "gxchatgrid-response-message";
               AssignProp(sPrefix, false, tblTablemessage_Internalname, "Class", tblTablemessage_Class, !bGXsfl_12_Refreshing);
               edtavCtlwho_Visible = 1;
               if ( ((GeneXus.Programs.wallet.registered.SdtGroupChatLine)(AV7lines.CurrentItem)).gxTpr_Colorindex == 1 )
               {
                  edtavCtlwho_Forecolor = GXUtil.RGB( 31, 111, 235);
               }
               else if ( ((GeneXus.Programs.wallet.registered.SdtGroupChatLine)(AV7lines.CurrentItem)).gxTpr_Colorindex == 2 )
               {
                  edtavCtlwho_Forecolor = GXUtil.RGB( 26, 127, 55);
               }
               else if ( ((GeneXus.Programs.wallet.registered.SdtGroupChatLine)(AV7lines.CurrentItem)).gxTpr_Colorindex == 3 )
               {
                  edtavCtlwho_Forecolor = GXUtil.RGB( 130, 80, 223);
               }
               else if ( ((GeneXus.Programs.wallet.registered.SdtGroupChatLine)(AV7lines.CurrentItem)).gxTpr_Colorindex == 4 )
               {
                  edtavCtlwho_Forecolor = GXUtil.RGB( 188, 76, 0);
               }
               else if ( ((GeneXus.Programs.wallet.registered.SdtGroupChatLine)(AV7lines.CurrentItem)).gxTpr_Colorindex == 5 )
               {
                  edtavCtlwho_Forecolor = GXUtil.RGB( 207, 34, 46);
               }
               else
               {
                  edtavCtlwho_Forecolor = GXUtil.RGB( 10, 124, 134);
               }
            }
            /* Load Method */
            if ( wbStart != -1 )
            {
               wbStart = 12;
            }
            sendrow_122( ) ;
            if ( isFullAjaxMode( ) && ! bGXsfl_12_Refreshing )
            {
               DoAjaxLoad(12, GridchatRow);
            }
            AV9GXV1 = (int)(AV9GXV1+1);
         }
         /*  Sending Event outputs  */
      }

      protected void E123R2( )
      {
         /* GlobalEvents_Receivechat Routine */
         returnInSub = false;
         context.DoAjaxRefreshCmp(sPrefix);
         /*  Sending Event outputs  */
         if ( gx_BV12 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV7lines", AV7lines);
            nGXsfl_12_bak_idx = nGXsfl_12_idx;
            gxgrGridchat_refresh( AV6groupId, AV7lines, sPrefix) ;
            nGXsfl_12_idx = nGXsfl_12_bak_idx;
            sGXsfl_12_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_12_idx), 4, 0), 4, "0");
            SubsflControlProps_122( ) ;
         }
      }

      protected void wb_table1_33_3R2( bool wbgen )
      {
         if ( wbgen )
         {
            /* Table start */
            sStyleString = "";
            GxWebStd.gx_table_start( context, tblTable1_Internalname, tblTable1_Internalname, "", "gxchatsend-message-table", 0, "", "", 1, 2, sStyleString, "", "", 0);
            context.WriteHtmlText( "<tr>") ;
            context.WriteHtmlText( "<td>") ;
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Multiple line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 37,'" + sPrefix + "',false,'" + sGXsfl_12_idx + "',0)\"";
            ClassString = "gxchatsend-text";
            StyleString = "";
            ClassString = "gxchatsend-text";
            StyleString = "";
            GxWebStd.gx_html_textarea( context, edtavSendtext_Internalname, AV8sendText, "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,37);\"", 0, 1, edtavSendtext_Enabled, 0, 80, "chr", 10, "row", 0, StyleString, ClassString, "", "", "2000", -1, 0, "", "", -1, true, "", "'"+sPrefix+"'"+",false,"+"'"+""+"'", 0, "", "HLP_Wallet/registered/GroupChatWC.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</td>") ;
            context.WriteHtmlText( "<td>") ;
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 39,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttSend_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(12), 2, 0)+","+"null"+");", "Send", bttSend_Jsonclick, 5, "Send", "", StyleString, ClassString, 1, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'SEND\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/GroupChatWC.htm");
            context.WriteHtmlText( "</td>") ;
            context.WriteHtmlText( "</tr>") ;
            /* End of table */
            context.WriteHtmlText( "</table>") ;
            wb_table1_33_3R2e( true) ;
         }
         else
         {
            wb_table1_33_3R2e( false) ;
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
         PA3R2( ) ;
         WS3R2( ) ;
         WE3R2( ) ;
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
         PA3R2( ) ;
         WCParametersGet( ) ;
      }

      public override void componentprepare( Object[] obj )
      {
         wbLoad = false;
         sCompPrefix = (string)getParm(obj,0);
         sSFPrefix = (string)getParm(obj,1);
         sPrefix = sCompPrefix + sSFPrefix;
         AddComponentObject(sPrefix, "wallet\\registered\\groupchatwc", GetJustCreated( ));
         if ( ( nDoneStart == 0 ) && ( nDynComponent == 0 ) )
         {
            INITWEB( ) ;
         }
         else
         {
            init_default_properties( ) ;
            init_web_controls( ) ;
         }
         PA3R2( ) ;
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
         PA3R2( ) ;
         sEvt = sCompEvt;
         WCParametersGet( ) ;
         WS3R2( ) ;
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
         WS3R2( ) ;
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
         WE3R2( ) ;
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?2026103013253243", true, true, false);
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
         context.AddJavascriptSource("wallet/registered/groupchatwc.js", "?2026103013253243", false, true, false);
         /* End function include_jscripts */
      }

      protected void SubsflControlProps_122( )
      {
         edtavCtlwho_Internalname = sPrefix+"CTLWHO_"+sGXsfl_12_idx;
         edtavCtldatetime_Internalname = sPrefix+"CTLDATETIME_"+sGXsfl_12_idx;
         edtavCtltext_Internalname = sPrefix+"CTLTEXT_"+sGXsfl_12_idx;
      }

      protected void SubsflControlProps_fel_122( )
      {
         edtavCtlwho_Internalname = sPrefix+"CTLWHO_"+sGXsfl_12_fel_idx;
         edtavCtldatetime_Internalname = sPrefix+"CTLDATETIME_"+sGXsfl_12_fel_idx;
         edtavCtltext_Internalname = sPrefix+"CTLTEXT_"+sGXsfl_12_fel_idx;
      }

      protected void sendrow_122( )
      {
         sGXsfl_12_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_12_idx), 4, 0), 4, "0");
         SubsflControlProps_122( ) ;
         WB3R0( ) ;
         GridchatRow = GXWebRow.GetNew(context,GridchatContainer);
         if ( subGridchat_Backcolorstyle == 0 )
         {
            /* None style subfile background logic. */
            subGridchat_Backstyle = 0;
            if ( StringUtil.StrCmp(subGridchat_Class, "") != 0 )
            {
               subGridchat_Linesclass = subGridchat_Class+"Odd";
            }
         }
         else if ( subGridchat_Backcolorstyle == 1 )
         {
            /* Uniform style subfile background logic. */
            subGridchat_Backstyle = 0;
            subGridchat_Backcolor = subGridchat_Allbackcolor;
            if ( StringUtil.StrCmp(subGridchat_Class, "") != 0 )
            {
               subGridchat_Linesclass = subGridchat_Class+"Uniform";
            }
         }
         else if ( subGridchat_Backcolorstyle == 2 )
         {
            /* Header style subfile background logic. */
            subGridchat_Backstyle = 1;
            if ( StringUtil.StrCmp(subGridchat_Class, "") != 0 )
            {
               subGridchat_Linesclass = subGridchat_Class+"Odd";
            }
            subGridchat_Backcolor = (int)(0xFFFFFF);
         }
         else if ( subGridchat_Backcolorstyle == 3 )
         {
            /* Report style subfile background logic. */
            subGridchat_Backstyle = 1;
            if ( ((int)((nGXsfl_12_idx) % (2))) == 0 )
            {
               subGridchat_Backcolor = (int)(0x0);
               if ( StringUtil.StrCmp(subGridchat_Class, "") != 0 )
               {
                  subGridchat_Linesclass = subGridchat_Class+"Even";
               }
            }
            else
            {
               subGridchat_Backcolor = (int)(0xFFFFFF);
               if ( StringUtil.StrCmp(subGridchat_Class, "") != 0 )
               {
                  subGridchat_Linesclass = subGridchat_Class+"Odd";
               }
            }
         }
         /* Start of Columns property logic. */
         if ( GridchatContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<tr"+" class=\""+subGridchat_Linesclass+"\" style=\""+""+"\""+" data-gxrow=\""+sGXsfl_12_idx+"\">") ;
         }
         /* Div Control */
         GridchatRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)divGridmessagestable_Internalname+"_"+sGXsfl_12_idx,(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)divGridmessagestable_Class,(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         GridchatRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"row",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         GridchatRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"col-xs-12",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Table start */
         GridchatRow.AddColumnProperties("table", -1, isAjaxCallMode( ), new Object[] {(string)tblTableheader_Internalname+"_"+sGXsfl_12_idx,(short)1,(string)"Table",(string)"",(string)"",(string)"",(string)"",(string)"",(string)"",(short)1,(short)2,(string)"",(string)"",(string)"",(string)"px",(string)"px",(string)""});
         GridchatRow.AddColumnProperties("row", -1, isAjaxCallMode( ), new Object[] {(string)"",(string)"",(string)""});
         GridchatRow.AddColumnProperties("cell", -1, isAjaxCallMode( ), new Object[] {(string)"",(string)"",(string)""});
         /* Div Control */
         GridchatRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)" gx-attribute",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Attribute/Variable Label */
         GridchatRow.AddColumnProperties("html_label", -1, isAjaxCallMode( ), new Object[] {(string)edtavCtlwho_Internalname,(string)"who",(string)"gx-form-item gxchatgrid-message__dateLabel",(short)0,(bool)true,(string)"width: 25%;"});
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 20,'" + sPrefix + "',false,'" + sGXsfl_12_idx + "',12)\"";
         ROClassString = "gxchatgrid-message__date";
         GridchatRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlwho_Internalname,((GeneXus.Programs.wallet.registered.SdtGroupChatLine)AV7lines.Item(AV9GXV1)).gxTpr_Who,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,20);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlwho_Jsonclick,(short)0,(string)"gxchatgrid-message__date","color:"+context.BuildHTMLColor( edtavCtlwho_Forecolor)+";",(string)ROClassString,(string)"",(string)"",(int)edtavCtlwho_Visible,(int)edtavCtlwho_Enabled,(short)0,(string)"text",(string)"",(short)80,(string)"chr",(short)1,(string)"row",(short)100,(short)0,(short)0,(short)12,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         GridchatRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         if ( GridchatContainer.GetWrapped() == 1 )
         {
            GridchatContainer.CloseTag("cell");
         }
         GridchatRow.AddColumnProperties("cell", -1, isAjaxCallMode( ), new Object[] {(string)"",(string)"",(string)""});
         /* Div Control */
         GridchatRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)" gx-attribute",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Attribute/Variable Label */
         GridchatRow.AddColumnProperties("html_label", -1, isAjaxCallMode( ), new Object[] {(string)edtavCtldatetime_Internalname,(string)"date Time",(string)"gx-form-item gxchatgrid-message__dateLabel",(short)0,(bool)true,(string)"width: 25%;"});
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 23,'" + sPrefix + "',false,'" + sGXsfl_12_idx + "',12)\"";
         ROClassString = "gxchatgrid-message__date";
         GridchatRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtldatetime_Internalname,context.localUtil.TToC( ((GeneXus.Programs.wallet.registered.SdtGroupChatLine)AV7lines.Item(AV9GXV1)).gxTpr_Datetime, 10, 8, 1, 2, "/", ":", " "),context.localUtil.Format( ((GeneXus.Programs.wallet.registered.SdtGroupChatLine)AV7lines.Item(AV9GXV1)).gxTpr_Datetime, "99/99/99 99:99"),TempTags+" onchange=\""+"gx.date.valid_date(this, 8,'MDY',5,12,'eng',false,1);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.date.valid_date(this, 8,'MDY',5,12,'eng',false,1);"+";gx.evt.onblur(this,23);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtldatetime_Jsonclick,(short)0,(string)"gxchatgrid-message__date",(string)"",(string)ROClassString,(string)"",(string)"",(short)1,(int)edtavCtldatetime_Enabled,(short)0,(string)"text",(string)"",(short)17,(string)"chr",(short)1,(string)"row",(short)17,(short)0,(short)0,(short)12,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
         GridchatRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         if ( GridchatContainer.GetWrapped() == 1 )
         {
            GridchatContainer.CloseTag("cell");
         }
         if ( GridchatContainer.GetWrapped() == 1 )
         {
            GridchatContainer.CloseTag("row");
         }
         if ( GridchatContainer.GetWrapped() == 1 )
         {
            GridchatContainer.CloseTag("table");
         }
         /* End of table */
         GridchatRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         GridchatRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         /* Div Control */
         GridchatRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"row",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         GridchatRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"col-xs-12",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Table start */
         GridchatRow.AddColumnProperties("table", -1, isAjaxCallMode( ), new Object[] {(string)tblTablemessage_Internalname+"_"+sGXsfl_12_idx,(short)1,(string)tblTablemessage_Class,(string)"",(string)"",(string)"",(string)"",(string)"",(string)"",(short)1,(short)2,(string)"",(string)"",(string)"",(string)"px",(string)"px",(string)""});
         GridchatRow.AddColumnProperties("row", -1, isAjaxCallMode( ), new Object[] {(string)"",(string)"",(string)""});
         GridchatRow.AddColumnProperties("cell", -1, isAjaxCallMode( ), new Object[] {(string)"",(string)"",(string)""});
         /* Div Control */
         GridchatRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)" gx-attribute",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Attribute/Variable Label */
         GridchatRow.AddColumnProperties("html_label", -1, isAjaxCallMode( ), new Object[] {(string)edtavCtltext_Internalname,(string)"text",(string)"gx-form-item gxchatgrid-message-textLabel",(short)0,(bool)true,(string)"width: 25%;"});
         /* Multiple line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 30,'" + sPrefix + "',false,'" + sGXsfl_12_idx + "',12)\"";
         ClassString = "gxchatgrid-message-text";
         StyleString = "";
         ClassString = "gxchatgrid-message-text";
         StyleString = "";
         GridchatRow.AddColumnProperties("html_textarea", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtltext_Internalname,((GeneXus.Programs.wallet.registered.SdtGroupChatLine)AV7lines.Item(AV9GXV1)).gxTpr_Text,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,30);\"",(short)0,(short)1,(int)edtavCtltext_Enabled,(short)0,(short)80,(string)"chr",(short)10,(string)"row",(short)0,(string)StyleString,(string)ClassString,(string)"",(string)"",(string)"4000",(short)-1,(short)0,(string)"",(string)"",(short)-1,(bool)true,(string)"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(short)0,(string)""});
         GridchatRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         if ( GridchatContainer.GetWrapped() == 1 )
         {
            GridchatContainer.CloseTag("cell");
         }
         if ( GridchatContainer.GetWrapped() == 1 )
         {
            GridchatContainer.CloseTag("row");
         }
         if ( GridchatContainer.GetWrapped() == 1 )
         {
            GridchatContainer.CloseTag("table");
         }
         /* End of table */
         GridchatRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         GridchatRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         GridchatRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         send_integrity_lvl_hashes3R2( ) ;
         /* End of Columns property logic. */
         GridchatContainer.AddRow(GridchatRow);
         nGXsfl_12_idx = ((subGridchat_Islastpage==1)&&(nGXsfl_12_idx+1>subGridchat_fnc_Recordsperpage( )) ? 1 : nGXsfl_12_idx+1);
         sGXsfl_12_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_12_idx), 4, 0), 4, "0");
         SubsflControlProps_122( ) ;
         /* End function sendrow_122 */
      }

      protected void init_web_controls( )
      {
         /* End function init_web_controls */
      }

      protected void StartGridControl12( )
      {
         if ( GridchatContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+sPrefix+"GridchatContainer"+"DivS\" data-gxgridid=\"12\">") ;
            sStyleString = "";
            GxWebStd.gx_table_start( context, subGridchat_Internalname, subGridchat_Internalname, "", "gxchatgrid", 0, "", "", 1, 2, sStyleString, "", "", 0);
            GridchatContainer.AddObjectProperty("GridName", "Gridchat");
         }
         else
         {
            GridchatContainer.AddObjectProperty("GridName", "Gridchat");
            GridchatContainer.AddObjectProperty("Header", subGridchat_Header);
            GridchatContainer.AddObjectProperty("Class", StringUtil.RTrim( "gxchatgrid"));
            GridchatContainer.AddObjectProperty("Class", "gxchatgrid");
            GridchatContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
            GridchatContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
            GridchatContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridchat_Backcolorstyle), 1, 0, ".", "")));
            GridchatContainer.AddObjectProperty("CmpContext", sPrefix);
            GridchatContainer.AddObjectProperty("InMasterPage", "false");
            GridchatColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridchatContainer.AddColumnProperties(GridchatColumn);
            GridchatColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridchatContainer.AddColumnProperties(GridchatColumn);
            GridchatColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridchatContainer.AddColumnProperties(GridchatColumn);
            GridchatColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridchatContainer.AddColumnProperties(GridchatColumn);
            GridchatColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridchatContainer.AddColumnProperties(GridchatColumn);
            GridchatColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridchatContainer.AddColumnProperties(GridchatColumn);
            GridchatColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridchatContainer.AddColumnProperties(GridchatColumn);
            GridchatColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridchatContainer.AddColumnProperties(GridchatColumn);
            GridchatColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridchatColumn.AddObjectProperty("Forecolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlwho_Forecolor), 9, 0, ".", "")));
            GridchatColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlwho_Enabled), 5, 0, ".", "")));
            GridchatColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlwho_Visible), 5, 0, ".", "")));
            GridchatContainer.AddColumnProperties(GridchatColumn);
            GridchatColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridchatContainer.AddColumnProperties(GridchatColumn);
            GridchatColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridchatContainer.AddColumnProperties(GridchatColumn);
            GridchatColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridchatContainer.AddColumnProperties(GridchatColumn);
            GridchatColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridchatContainer.AddColumnProperties(GridchatColumn);
            GridchatColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridchatColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtldatetime_Enabled), 5, 0, ".", "")));
            GridchatContainer.AddColumnProperties(GridchatColumn);
            GridchatColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridchatContainer.AddColumnProperties(GridchatColumn);
            GridchatColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridchatContainer.AddColumnProperties(GridchatColumn);
            GridchatColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridchatContainer.AddColumnProperties(GridchatColumn);
            GridchatColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridchatContainer.AddColumnProperties(GridchatColumn);
            GridchatColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridchatContainer.AddColumnProperties(GridchatColumn);
            GridchatColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridchatContainer.AddColumnProperties(GridchatColumn);
            GridchatColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridchatContainer.AddColumnProperties(GridchatColumn);
            GridchatColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridchatContainer.AddColumnProperties(GridchatColumn);
            GridchatColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridchatContainer.AddColumnProperties(GridchatColumn);
            GridchatColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridchatContainer.AddColumnProperties(GridchatColumn);
            GridchatColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridchatColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtltext_Enabled), 5, 0, ".", "")));
            GridchatContainer.AddColumnProperties(GridchatColumn);
            GridchatColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridchatContainer.AddColumnProperties(GridchatColumn);
            GridchatColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridchatContainer.AddColumnProperties(GridchatColumn);
            GridchatColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridchatContainer.AddColumnProperties(GridchatColumn);
            GridchatColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridchatContainer.AddColumnProperties(GridchatColumn);
            GridchatContainer.AddObjectProperty("Selectedindex", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridchat_Selectedindex), 4, 0, ".", "")));
            GridchatContainer.AddObjectProperty("Allowselection", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridchat_Allowselection), 1, 0, ".", "")));
            GridchatContainer.AddObjectProperty("Selectioncolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridchat_Selectioncolor), 9, 0, ".", "")));
            GridchatContainer.AddObjectProperty("Allowhover", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridchat_Allowhovering), 1, 0, ".", "")));
            GridchatContainer.AddObjectProperty("Hovercolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridchat_Hoveringcolor), 9, 0, ".", "")));
            GridchatContainer.AddObjectProperty("Allowcollapsing", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridchat_Allowcollapsing), 1, 0, ".", "")));
            GridchatContainer.AddObjectProperty("Collapsed", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridchat_Collapsed), 1, 0, ".", "")));
         }
      }

      protected void init_default_properties( )
      {
         lblTbempty_Internalname = sPrefix+"TBEMPTY";
         edtavCtlwho_Internalname = sPrefix+"CTLWHO";
         edtavCtldatetime_Internalname = sPrefix+"CTLDATETIME";
         tblTableheader_Internalname = sPrefix+"TABLEHEADER";
         edtavCtltext_Internalname = sPrefix+"CTLTEXT";
         tblTablemessage_Internalname = sPrefix+"TABLEMESSAGE";
         divGridmessagestable_Internalname = sPrefix+"GRIDMESSAGESTABLE";
         edtavSendtext_Internalname = sPrefix+"vSENDTEXT";
         bttSend_Internalname = sPrefix+"SEND";
         tblTable1_Internalname = sPrefix+"TABLE1";
         divChattable_Internalname = sPrefix+"CHATTABLE";
         divMaintable_Internalname = sPrefix+"MAINTABLE";
         Form.Internalname = sPrefix+"FORM";
         subGridchat_Internalname = sPrefix+"GRIDCHAT";
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
         subGridchat_Allowcollapsing = 0;
         edtavCtltext_Enabled = 0;
         edtavCtldatetime_Jsonclick = "";
         edtavCtldatetime_Enabled = 0;
         edtavCtlwho_Jsonclick = "";
         edtavCtlwho_Forecolor = (int)(0x000000);
         edtavCtlwho_Enabled = 0;
         edtavCtlwho_Visible = 1;
         subGridchat_Class = "gxchatgrid";
         edtavSendtext_Enabled = 1;
         tblTablemessage_Class = "Table";
         divGridmessagestable_Class = "Table";
         subGridchat_Backcolorstyle = 0;
         lblTbempty_Caption = "TBEmpty";
         edtavCtltext_Enabled = -1;
         edtavCtldatetime_Enabled = -1;
         edtavCtlwho_Enabled = -1;
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"GRIDCHAT_nFirstRecordOnPage","type":"int"},{"av":"GRIDCHAT_nEOF","type":"int"},{"av":"AV7lines","fld":"vLINES","grid":12,"type":""},{"av":"nGXsfl_12_idx","ctrl":"GRID","prop":"GridCurrRow","grid":12},{"av":"nRC_GXsfl_12","ctrl":"GRIDCHAT","prop":"GridRC","grid":12,"type":"int"},{"av":"sPrefix","type":"char"},{"av":"AV6groupId","fld":"vGROUPID","type":"guid"}]""");
         setEventMetadata("REFRESH",""","oparms":[{"av":"AV7lines","fld":"vLINES","grid":12,"type":""},{"av":"nGXsfl_12_idx","ctrl":"GRID","prop":"GridCurrRow","grid":12},{"av":"GRIDCHAT_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_12","ctrl":"GRIDCHAT","prop":"GridRC","grid":12,"type":"int"},{"av":"lblTbempty_Caption","ctrl":"TBEMPTY","prop":"Caption"}]}""");
         setEventMetadata("'SEND'","""{"handler":"E113R2","iparms":[{"av":"GRIDCHAT_nFirstRecordOnPage","type":"int"},{"av":"GRIDCHAT_nEOF","type":"int"},{"av":"AV6groupId","fld":"vGROUPID","type":"guid"},{"av":"AV7lines","fld":"vLINES","grid":12,"type":""},{"av":"nGXsfl_12_idx","ctrl":"GRID","prop":"GridCurrRow","grid":12},{"av":"nRC_GXsfl_12","ctrl":"GRIDCHAT","prop":"GridRC","grid":12,"type":"int"},{"av":"sPrefix","type":"char"},{"av":"AV8sendText","fld":"vSENDTEXT","type":"svchar"}]""");
         setEventMetadata("'SEND'",""","oparms":[{"av":"AV8sendText","fld":"vSENDTEXT","type":"svchar"},{"av":"AV7lines","fld":"vLINES","grid":12,"type":""},{"av":"nGXsfl_12_idx","ctrl":"GRID","prop":"GridCurrRow","grid":12},{"av":"GRIDCHAT_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_12","ctrl":"GRIDCHAT","prop":"GridRC","grid":12,"type":"int"},{"av":"lblTbempty_Caption","ctrl":"TBEMPTY","prop":"Caption"}]}""");
         setEventMetadata("GRIDCHAT.LOAD","""{"handler":"E143R2","iparms":[{"av":"AV7lines","fld":"vLINES","grid":12,"type":""},{"av":"nGXsfl_12_idx","ctrl":"GRID","prop":"GridCurrRow","grid":12},{"av":"GRIDCHAT_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_12","ctrl":"GRIDCHAT","prop":"GridRC","grid":12,"type":"int"}]""");
         setEventMetadata("GRIDCHAT.LOAD",""","oparms":[{"ctrl":"CTLWHO","prop":"Forecolor"},{"av":"divGridmessagestable_Class","ctrl":"GRIDMESSAGESTABLE","prop":"Class"},{"av":"tblTablemessage_Class","ctrl":"TABLEMESSAGE","prop":"Class"},{"ctrl":"CTLWHO","prop":"Visible"}]}""");
         setEventMetadata("GLOBALEVENTS.RECEIVECHAT","""{"handler":"E123R2","iparms":[{"av":"GRIDCHAT_nFirstRecordOnPage","type":"int"},{"av":"GRIDCHAT_nEOF","type":"int"},{"av":"AV6groupId","fld":"vGROUPID","type":"guid"},{"av":"AV7lines","fld":"vLINES","grid":12,"type":""},{"av":"nGXsfl_12_idx","ctrl":"GRID","prop":"GridCurrRow","grid":12},{"av":"nRC_GXsfl_12","ctrl":"GRIDCHAT","prop":"GridRC","grid":12,"type":"int"},{"av":"sPrefix","type":"char"}]""");
         setEventMetadata("GLOBALEVENTS.RECEIVECHAT",""","oparms":[{"av":"AV7lines","fld":"vLINES","grid":12,"type":""},{"av":"nGXsfl_12_idx","ctrl":"GRID","prop":"GridCurrRow","grid":12},{"av":"GRIDCHAT_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_12","ctrl":"GRIDCHAT","prop":"GridRC","grid":12,"type":"int"},{"av":"lblTbempty_Caption","ctrl":"TBEMPTY","prop":"Caption"}]}""");
         setEventMetadata("NULL","""{"handler":"Validv_Gxv4","iparms":[]}""");
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
         AV7lines = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupChatLine>( context, "GroupChatLine", "distributedcryptography");
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         GX_FocusControl = "";
         lblTbempty_Jsonclick = "";
         GridchatContainer = new GXWebGrid( context);
         sStyleString = "";
         Form = new GXWebForm();
         sXEvt = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         AV8sendText = "";
         AV5error = "";
         GridchatRow = new GXWebRow();
         TempTags = "";
         ClassString = "";
         StyleString = "";
         bttSend_Jsonclick = "";
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         sCtrlAV6groupId = "";
         subGridchat_Linesclass = "";
         ROClassString = "";
         subGridchat_Header = "";
         GridchatColumn = new GXWebColumn();
         /* GeneXus formulas. */
         edtavCtlwho_Enabled = 0;
         edtavCtldatetime_Enabled = 0;
         edtavCtltext_Enabled = 0;
      }

      private short nGotPars ;
      private short GxWebError ;
      private short nDynComponent ;
      private short wbEnd ;
      private short wbStart ;
      private short nDraw ;
      private short nDoneStart ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short subGridchat_Backcolorstyle ;
      private short GRIDCHAT_nEOF ;
      private short nGXWrapped ;
      private short subGridchat_Backstyle ;
      private short subGridchat_Allowselection ;
      private short subGridchat_Allowhovering ;
      private short subGridchat_Allowcollapsing ;
      private short subGridchat_Collapsed ;
      private int nRC_GXsfl_12 ;
      private int nGXsfl_12_idx=1 ;
      private int edtavCtlwho_Enabled ;
      private int edtavCtldatetime_Enabled ;
      private int edtavCtltext_Enabled ;
      private int AV9GXV1 ;
      private int subGridchat_Islastpage ;
      private int nGXsfl_12_fel_idx=1 ;
      private int nGXsfl_12_bak_idx=1 ;
      private int edtavCtlwho_Visible ;
      private int edtavCtlwho_Forecolor ;
      private int edtavSendtext_Enabled ;
      private int idxLst ;
      private int subGridchat_Backcolor ;
      private int subGridchat_Allbackcolor ;
      private int subGridchat_Selectedindex ;
      private int subGridchat_Selectioncolor ;
      private int subGridchat_Hoveringcolor ;
      private long GRIDCHAT_nCurrentRecord ;
      private long GRIDCHAT_nFirstRecordOnPage ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sPrefix ;
      private string sCompPrefix ;
      private string sSFPrefix ;
      private string sGXsfl_12_idx="0001" ;
      private string edtavCtlwho_Internalname ;
      private string edtavCtldatetime_Internalname ;
      private string edtavCtltext_Internalname ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string GX_FocusControl ;
      private string divMaintable_Internalname ;
      private string lblTbempty_Internalname ;
      private string lblTbempty_Caption ;
      private string lblTbempty_Jsonclick ;
      private string divChattable_Internalname ;
      private string sStyleString ;
      private string subGridchat_Internalname ;
      private string sXEvt ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string edtavSendtext_Internalname ;
      private string sGXsfl_12_fel_idx="0001" ;
      private string AV5error ;
      private string divGridmessagestable_Class ;
      private string divGridmessagestable_Internalname ;
      private string tblTablemessage_Class ;
      private string tblTablemessage_Internalname ;
      private string tblTable1_Internalname ;
      private string TempTags ;
      private string ClassString ;
      private string StyleString ;
      private string bttSend_Internalname ;
      private string bttSend_Jsonclick ;
      private string sCtrlAV6groupId ;
      private string subGridchat_Class ;
      private string subGridchat_Linesclass ;
      private string tblTableheader_Internalname ;
      private string ROClassString ;
      private string edtavCtlwho_Jsonclick ;
      private string edtavCtldatetime_Jsonclick ;
      private string subGridchat_Header ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool bGXsfl_12_Refreshing=false ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool gx_refresh_fired ;
      private bool returnInSub ;
      private bool gx_BV12 ;
      private string AV8sendText ;
      private Guid AV6groupId ;
      private Guid wcpOAV6groupId ;
      private GXWebGrid GridchatContainer ;
      private GXWebRow GridchatRow ;
      private GXWebColumn GridchatColumn ;
      private GXWebForm Form ;
      private IGxDataStore dsDefault ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupChatLine> AV7lines ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
