import{r as e,c as t,f as s,g as n,h as i,H as a}from"./p-7af91c05.js";import{a as r}from"./p-a859514b.js";import{m as o,t as d}from"./p-2cf94d81.js";import{E as c,e as l,i as h,t as u,T as f,x as p,n as g,a as m}from"./p-98092b8a.js";import{v as b,r as $,M as v,m as y,p as w}from"./p-bec2d2fd.js";import"./p-e7a788ef.js";const x="…";const C=e=>({id:e.id,content:e.text,role:"assistant",status:e.final?"complete":"streaming",transcribed:true});const k=e=>({id:e.id,content:e.text,role:"user",transcribed:true});const M=e=>{const t=[];const s=[...e.assistant.values()];const n=[...e.user.values()];let i=0;let a=0;while(a<n.length&&i<s.length){t.push(n[a].firstReceivedTime<=s[i].firstReceivedTime?k(n[a++]):C(s[i++]))}t.push(...n.slice(a).map(k));t.push(...s.slice(i).map(C));if(t.length<=1){return t}const r=[t[0]];for(let e=1;e<t.length;e++){const s=t[e];const n=r.at(-1);if(n.role===s.role){n.content+="\n"+s.content;if(s.role==="assistant"){n.status=s.status}}else{r.push(s)}}return r};const R=e=>typeof e.content==="string"?e.content:e.content.message;const I=e=>typeof e.content==="string"?e.content:JSON.stringify(e.content,undefined,2);const S=e=>typeof e.content==="object"?e.content.files??[]:[];const A=e=>typeof e.content==="object"?{files:e.content.files??[],sources:e.content.sources??[]}:{files:[],sources:[]};const _="complete";const B="uploaded";const T=e=>()=>o(e)
/**
 * @license
 * Copyright 2018 Google LLC
 * SPDX-License-Identifier: BSD-3-Clause
 */;const U=e=>e??c
/**
 * @license
 * Copyright 2017 Google LLC
 * SPDX-License-Identifier: BSD-3-Clause
 */;const j=(e,t,s)=>{const n=new Map;for(let i=t;i<=s;i++)n.set(e[i],i);return n},L=l(class extends h{constructor(e){if(super(e),e.type!==u.CHILD)throw Error("repeat() can only be used in text expressions")}dt(e,t,s){let n;void 0===s?s=t:void 0!==t&&(n=t);const i=[],a=[];let r=0;for(const t of e)i[r]=n?n(t,r):r,a[r]=s(t,r),r++;return{values:a,keys:i}}render(e,t,s){return this.dt(e,t,s).values}update(e,[t,s,n]){const i=w(e),{values:a,keys:r}=this.dt(t,s,n);if(!Array.isArray(i))return this.ut=r,a;const o=this.ut??=[],d=[];let c,l,h=0,u=i.length-1,p=0,g=a.length-1;for(;h<=u&&p<=g;)if(null===i[h])h++;else if(null===i[u])u--;else if(o[h]===r[p])d[p]=b(i[h],a[p]),h++,p++;else if(o[u]===r[g])d[g]=b(i[u],a[g]),u--,g--;else if(o[h]===r[g])d[g]=b(i[h],a[g]),$(e,d[g+1],i[h]),h++,g--;else if(o[u]===r[p])d[p]=b(i[u],a[p]),$(e,i[h],i[u]),u--,p++;else if(void 0===c&&(c=j(r,p,g),l=j(o,h,u)),c.has(o[h]))if(c.has(o[u])){const t=l.get(r[p]),s=void 0!==t?i[t]:null;if(null===s){const t=$(e,i[h]);b(t,a[p]),d[p]=t}else d[p]=b(s,a[p]),$(e,i[h],s),i[t]=null;p++}else v(i[u]),u--;else v(i[h]),h++;for(;p<=g;){const t=$(e,d[g+1]);b(t,a[p]),d[p++]=t}for(;h<=u;){const e=i[h++];null!==e&&v(e)}return this.ut=r,y(e,d),f}});
/**
 * @license
 * Copyright 2021 Google LLC
 * SPDX-License-Identifier: BSD-3-Clause
 */function z(e,t,s){return e?t(e):s?.(e)}const E=(e,t)=>{const{accessibleName:s,text:n}=t.translations;return z(e.role==="assistant"&&(e.status==="complete"||!e.status),(()=>p`<button
      aria-label=${s.copyMessageContent}
      part=${d({[`assistant copy-message-content ${e.id}`]:true,[e.parts]:!!e.parts})}
      type="button"
      @click=${T(I(e))}
    >
      ${n.copyMessageContent}
    </button>`))};const K=(e,t,s)=>()=>s(e,t);const H=e=>t=>{const{accessibleName:s,text:n}=e.translations;return p`<div part="code-block">
      <div part="code-block__header">
        <span part="code-block__header-caption">${t.language}</span>

        <div part="code-block__header-actions">
          <button
            aria-label=${n.copyCodeButton}
            part="code-block__copy-code-button"
            type="button"
            @click=${T(t.plainText)}
          >
            ${n.copyCodeButton}
          </button>

          ${z(e.callbacks?.downloadCodeBlock,(()=>p`<button
              aria-label=${s.downloadCodeButton}
              part="code-block__download-code-button"
              @click=${K(t.plainText,t.language,e.callbacks.downloadCodeBlock)}
            >
              ${n.downloadCodeButton}
            </button>`))}
        </div>
      </div>

      <ch-code
        .language=${t.language}
        .lastNestedChildClass=${t.lastNestedChildClass}
        part="code-block__content"
        .showIndicator=${t.showIndicator}
        .value=${t.plainText}
      ></ch-code>
    </div>`};const N=(e,t,s)=>{const n=R(e);return e.status==="waiting"?p`<div
        class="assistant-loading"
        part=${d({[`assistant content waiting ${e.id}`]:true,[e.parts]:!!e.parts})}
      >
        ${n}
      </div>`:z(n,(()=>p`<ch-markdown-viewer
          part=${d({[`assistant content ${e.id} ${e.status??_}`]:true,[e.parts]:!!e.parts})}
          .renderCode=${s(t)}
          .showIndicator=${e.status==="streaming"}
          .theme=${t.markdownTheme}
          .value=${n}
        ></ch-markdown-viewer>`))};const O=(e,t,s)=>{const n=R(e);return z(n,(()=>p`<ch-markdown-viewer
        part=${d({[`error content ${e.id}`]:true,[e.parts]:!!e.parts})}
        .renderCode=${s(t)}
        .theme=${t.markdownTheme}
        .value=${n}
      ></ch-markdown-viewer>`))};const W=e=>R(e);const D=()=>null;const F={assistant:N,error:O,system:D,user:W};const G=(e,t,s)=>F[e.role](e,t,s);const P=(e,t)=>z(e.uploadState==="in-progress",(()=>p`<div
      part=${d({[`file-skeleton format-${t} ${e.mimeType} ${e.uploadState??B}`]:true,[e.extension]:!!e.extension,[e.parts]:!!e.parts})}
    ></div>`));const Z=(e,t)=>d({[`file-container format-${t} ${e.mimeType} ${e.uploadState??B}`]:true,[e.extension]:!!e.extension,[e.parts]:!!e.parts});const q=(e,t)=>d({[`file format-${t} ${e.mimeType} ${e.uploadState??B}`]:true,[e.extension]:!!e.extension,[e.parts]:!!e.parts});const J={audio:e=>p`<li
      class="file-container"
      part=${Z(e,"audio")}
    >
      <audio
        aria-label=${e.accessibleName}
        part=${q(e,"audio")}
        src=${U(e.uploadState==="in-progress"?undefined:e.url)}
        controls
      ></audio>

      ${P(e,"audio")}
    </li>`,video:e=>p`<li
      class="file-container"
      part=${Z(e,"video")}
    >
      <video
        aria-label=${e.accessibleName}
        part=${q(e,"video")}
        src=${U(e.uploadState==="in-progress"?undefined:e.url)}
        controls
      ></video>

      ${P(e,"video")}
    </li>`,image:e=>p`<li class="file-container" part={getFileContainerParts(file, "image")}>
      <img
        aria-label=${e.accessibleName}
        part=${q(e,"image")}
        src=${e.url}
        alt=${e.alternativeText??e.accessibleName??""}
        loading="lazy"
      />

      ${P(e,"image")}
    </li>`,file:e=>{const t=e.uploadState==="in-progress";return p`<li
      class="file-container"
      part=${Z(e,"file")}
    >
      <a
        aria-label=${e.accessibleName}
        role=${U(t?"link":undefined)}
        aria-disabled=${U(t?"true":undefined)}
        part=${q(e,"file")}
        href=${U(t?undefined:e.url)}
        target="_blank"
      >
        ${z(e.caption,(()=>p`<span
              part=${`file-caption format-file ${e.mimeType} ${e.uploadState??B}`}
            >
              ${e.caption}
            </span>`))}
        ${z(e.caption&&e.extension,(()=>p`<span
            part=${`file-extension format-file ${e.mimeType} ${e.uploadState??B}`}
          >
            ${e.extension}
          </span>`))}
      </a>

      ${P(e,"file")}
    </li>`}};const V=e=>p`<li class="contents">
    <a
      aria-label=${e.accessibleName}
      part=${d({source:true,[e.parts]:!!e.parts})}
      href=${e.url}
      target="_blank"
    >
      ${z(e.caption,(()=>p`<span
          part=${d({"source-caption":true,[e.parts]:!!e.parts})}
        >
          ${e.caption}
        </span>`))}
    </a>
  </li>`;const X=e=>{const t=e.substring(0,5);return t==="image"||t==="video"||t==="audio"?t:"file"};const Q=(e,t,s)=>s[X(e.mimeType)](e,t);const Y=(e,t,s)=>{const n=e.role==="assistant"?e.status??_:undefined;const{files:i,sources:a}=A(e);const{sourceFiles:r}=t.translations.text;return p`<div
    part=${d({[`content-container ${e.role} ${e.id}`]:true,[n]:!!n,[e.parts]:!!e.parts})}
  >
    ${s.contentBefore?s.contentBefore(e,t,s.codeBlock):c}
    ${s.content(e,t,s.codeBlock)}
    ${s.contentAfter?s.contentAfter(e,t,s.codeBlock):c}
    ${z(i.length!==0,(()=>p`<ul
          class="files-container"
          part=${d({[`files-container ${e.role} ${e.id}`]:true,[n]:!!n,[e.parts]:!!e.parts})}
        >
          ${i.map((e=>Q(e,t,s.file)))}
        </ul>`))}
    ${z(a.length!==0,(()=>p`<ul
          class="sources-container"
          part=${d({[`sources-container ${e.role} ${e.id}`]:true,[n]:!!n,[e.parts]:!!e.parts})}
        >
          ${z(r,(()=>p`<span
              part=${d({[`sources-caption ${e.role} ${e.id}`]:true,[n]:!!n,[e.parts]:!!e.parts})}
            >
              ${r}
            </span>`))}
          ${a.map((e=>s.source(e,t)))}
        </ul>`))}
    ${s.actions(e,t)}
  </div>`};const ee=(e,t,s)=>(s.messageStructure??Y)(e,t,{actions:s.actions??E,codeBlock:s.codeBlock??H,contentBefore:s.contentBefore,content:s.content??G,contentAfter:s.contentAfter,file:{audio:s.file?.audio??J.audio,file:s.file?.file??J.file,image:s.file?.image??J.image,video:s.file?.video??J.video},source:s.source??V});var te=undefined&&undefined.__decorate||function(e,t,s,n){var i=arguments.length,a=i<3?t:n===null?n=Object.getOwnPropertyDescriptor(t,s):n,r;if(typeof Reflect==="object"&&typeof Reflect.decorate==="function")a=Reflect.decorate(e,t,s,n);else for(var o=e.length-1;o>=0;o--)if(r=e[o])a=(i<3?r(a):i>3?r(t,s,a):r(t,s))||a;return i>3&&a&&Object.defineProperty(t,s,a),a};var se=undefined&&undefined.__metadata||function(e,t){if(typeof Reflect==="object"&&typeof Reflect.metadata==="function")return Reflect.metadata(e,t)};const ne=e=>e==="streaming"?"true":"false";class ie extends m{#e;get cellHasToReserveSpace(){return this.#e}set cellHasToReserveSpace(e){this.#e=e}#t;get cellIdAlignedWhenRendered(){return this.#t}set cellIdAlignedWhenRendered(e){this.#t=e}#s=undefined;get chatRef(){return this.#s}set chatRef(e){this.#s=e}#n=[];get liveKitMessages(){return this.#n}set liveKitMessages(e){this.#n=e}#i="end";get newUserMessageAlignment(){return this.#i}set newUserMessageAlignment(e){this.#i=e}#a="instant";get newUserMessageScrollBehavior(){return this.#a}set newUserMessageScrollBehavior(e){this.#a=e}#r;get renderItem(){return this.#r}set renderItem(e){this.#r=e}#o;get smartGridRef(){return this.#o}set smartGridRef(e){this.#o=e}#d=[];get virtualItems(){return this.#d}set virtualItems(e){this.#d=e}createRenderRoot(){return this}#c=()=>this.smartGridRef.scrollEndContentToPosition(this.cellIdAlignedWhenRendered,{position:this.newUserMessageAlignment,behavior:this.newUserMessageScrollBehavior});#l=e=>{if(!this.renderItem){return ee(e,this.chatRef,{})}return typeof this.renderItem==="function"?this.renderItem(e):ee(e,this.chatRef,this.renderItem)};#h=e=>{if(e.role==="system"){return""}const t=e.role==="assistant";const s=A(e);const n=d({[`message ${e.role} ${e.id}`]:true,"has-content":(R(e)??"").trim()!=="","has-files":s.files.length!==0,"has-sources":s.sources.length!==0,[e.parts]:!!e.parts,[e.status??_]:t});const i=this.#l(e);const a=e.id===this.cellIdAlignedWhenRendered;const r=this.cellHasToReserveSpace!==undefined&&this.cellHasToReserveSpace.has(e.id);return p`<ch-smart-grid-cell
      .cellId=${e.id}
      aria-live=${U(t?"polite":undefined)}
      aria-busy=${U(t?ne(e.status):undefined)}
      part=${U(r?undefined:n)}
      .smartGridRef=${this.smartGridRef}
      @smartCellDidLoad=${a?this.#c:undefined}
    >
      ${r?p`<div part=${n}>${i}</div>`:i}
    </ch-smart-grid-cell>`};render(){return p`${L(this.virtualItems,(e=>e.id),this.#h)}${this.liveKitMessages?L(this.liveKitMessages,(e=>e.id),this.#h):c}`}}te([g({attribute:false}),se("design:type",Set)],ie.prototype,"cellHasToReserveSpace",null);te([g({attribute:false}),se("design:type",String)],ie.prototype,"cellIdAlignedWhenRendered",null);te([g({attribute:false}),se("design:type",HTMLDivElement)],ie.prototype,"chatRef",null);te([g({attribute:false}),se("design:type",Array)],ie.prototype,"liveKitMessages",null);te([g(),se("design:type",String)],ie.prototype,"newUserMessageAlignment",null);te([g(),se("design:type",Object)],ie.prototype,"newUserMessageScrollBehavior",null);te([g(),se("design:type",Object)],ie.prototype,"renderItem",null);te([g({attribute:false}),se("design:type",HTMLDivElement)],ie.prototype,"smartGridRef",null);te([g({attribute:false}),se("design:type",Array)],ie.prototype,"virtualItems",null);customElements.define("ch-chat-lit",ie);const ae=':where(button,a){all:unset;display:inline-flex;align-items:center;user-select:none;touch-action:manipulation;cursor:pointer}button:disabled{pointer-events:none}*,::after,::before{box-sizing:border-box}h1,h2,h3,h4,h5,h6,p{font:inherit;margin:0;padding:0;color:inherit}:host{display:grid;grid-template-rows:1fr max-content}:host(.ch-chat--additional-content){grid-template-rows:1fr max-content max-content}ch-chat-lit{display:contents}.assistant-content,.user-content{contain:paint}img{display:block;max-inline-size:100%}.assistant-loading{display:grid;grid-template-columns:max-content 1fr;align-items:center}::part(code-block__header){display:grid;grid-template-columns:1fr max-content}.contents{display:contents}.file-container{display:block}.sources-container{display:flex;flex-wrap:wrap}.files-container,.sources-container{margin:0;padding:0}.send-input{grid-template:"ch-chat-send-input-before" max-content "ch-edit-content" 1fr "ch-chat-send-input-after" max-content/1fr}.send-input-before{grid-area:ch-chat-send-input-before}.send-input-after{grid-area:ch-chat-send-input-after}.send-container,.additional-content-container{display:grid}.ch-virtual-scroller--content-loaded>ch-chat-lit>ch-smart-grid-cell:not([data-did-load]){position:absolute;pointer-events:none;visibility:hidden}.spinner-loading{animation:infinite-rotate 0.875s linear 0s infinite}@keyframes infinite-rotate{100%{transform:rotate(1turn)}}@keyframes gx-skeleton-load{to{transform:translateX(192%)}}.processing-animation{content:"";margin-inline-start:auto;display:block;background-image:url(\'data:image/svg+xml,<svg xmlns="http://www.w3.org/2000/svg" width="6" height="6" viewBox="0 0 6 6"><path d="M3 6C4.65685 6 6 4.65685 6 3C6 1.34315 4.65685 0 3 0C1.34315 0 0 1.34315 0 3C0 4.65685 1.34315 6 3 6Z" fill="%23939498"/></svg>\'), url(\'data:image/svg+xml,<svg xmlns="http://www.w3.org/2000/svg" width="6" height="6" viewBox="0 0 6 6"><path d="M3 6C4.65685 6 6 4.65685 6 3C6 1.34315 4.65685 0 3 0C1.34315 0 0 1.34315 0 3C0 4.65685 1.34315 6 3 6Z" fill="%23939498"/></svg>\'), url(\'data:image/svg+xml,<svg xmlns="http://www.w3.org/2000/svg" width="6" height="6" viewBox="0 0 6 6"><path d="M3 6C4.65685 6 6 4.65685 6 3C6 1.34315 4.65685 0 3 0C1.34315 0 0 1.34315 0 3C0 4.65685 1.34315 6 3 6Z" fill="%23939498"/></svg>\');inline-size:20px;block-size:16px;background-position:left, center, right;background-size:4px, 4px, 4px;background-repeat:no-repeat;animation:ellipsis 1s infinite linear}@keyframes ellipsis{0%{background-size:6px, 4px, 4px}25%{background-size:4px, 6px, 4px}50%{background-size:4px, 4px, 6px}75%{background-size:4px, 6px, 4px}100%{background-size:6px, 4px, 4px}}';const re=ae;const oe="Enter";const de=()=>({assistant:new Map,user:new Map});const ce=e=>(e.sendContainerBefore??[]).includes("stop-response-button")||(e.sendInputBefore??[]).includes("stop-response-button")||(e.sendInputAfter??[]).includes("stop-response-button")||(e.sendContainerAfter??[]).includes("stop-response-button");const le={sendContainerBefore:"send-container-before",sendInputBefore:"send-input-before",sendInputAfter:"send-input-after",sendContainerAfter:"send-container-after"};const he=e=>e==="sendInputBefore"?"additional-content-before":"additional-content-after";const ue=class{constructor(s){e(this,s);this.userMessageAdded=t(this,"userMessageAdded",7);this.uploadingFiles=0;this.virtualItems=[];this.initialLoadHasEnded=false;this.autoScroll="at-scroll-end";this.callbacks=undefined;this.disabled=false;this.items=[];this.liveMode=false;this.liveModeConfiguration=undefined;this.loadingState="initial";this.markdownTheme="ch-markdown-viewer";this.newUserMessageAlignment="end";this.newUserMessageScrollBehavior="instant";this.renderItem=undefined;this.sendButtonDisabled=false;this.sendInputDisabled=false;this.showAdditionalContent=false;this.sendContainerLayout={sendContainerAfter:["send-button"]};this.theme=undefined;this.translations={accessibleName:{clearChat:"Clear chat",copyMessageContent:"Copy message content",downloadCodeButton:"Download code",sendButton:"Send",sendInput:"Message",stopResponseButton:"Stop generating answer"},placeholder:{sendInput:"Ask me a question..."},text:{copyCodeButton:"Copy code",copyMessageContent:"Copy",processing:"Processing...",sourceFiles:"Source files:"}};this.waitingResponse=false}#u;#f;#p;#g;#m={activeSpeakersChanged:e=>this.callbacks?.liveMode?.activeSpeakersChanged(e),updateTranscriptions:(e,t)=>{let n;for(let t=0;t<e.length;t++){const s=e[t];if(s.text.trim()!==""&&s.text!==x){n=s}}if(n===undefined){return}const i=t.isLocal?"user":"assistant";this.#p[i].set(n.id,n);this.#g=M(this.#p);s(this)}};#b=true;#$;#v;#y;#w;get el(){return n(this)}itemsChanged(){this.#u=undefined;this.initialLoadHasEnded=false;this.#f=undefined;this.#y?.removeScrollEndContentReference()}liveModeChanged(){if(this.liveMode){this.#p=de();this.#g=[]}else{this.#w?.addItems("end",...this.#g);this.#p=undefined;requestAnimationFrame((()=>{this.#g=undefined}))}}newUserMessageAlignmentChanged(){if(this.newUserMessageAlignment==="end"){this.#u=undefined}}sendContainerLayoutChanged(){this.#b=!ce(this.sendContainerLayout)}userMessageAdded;async addNewMessage(e){this.initialLoadHasEnded=true;if(this.newUserMessageAlignment==="start"){this.#f??=new Set;this.#f.add(e.id)}this.#x(e)}async focusChatInput(){this.#v?.focus()}async setChatInputMessage(e){if(this.#v){this.#v.value=e}}async sendChatMessage(e,t){return this.#C(e,t)}async updateChatMessage(e,t,n){if(this.items.length===0||!this.items[e]){return}this.initialLoadHasEnded=true;this.#k(e,t,n);const i=this.items[e];const a=this.virtualItems.findIndex((e=>e.id===i.id));if(a!==-1){this.virtualItems[a]=i}s(this)}async updateLastMessage(e,t){if(this.items.length===0){return}this.initialLoadHasEnded=true;this.#k(this.items.length-1,e,t);this.virtualItems[this.virtualItems.length-1]=this.items.at(-1);s(this)}#x=async e=>{if(this.items.length===0||!this.#w){this.items.push(e);s(this)}else{await this.#w.addItems("end",e)}};#k=(e,t,s)=>{if(s==="concat"){const s=this.items[e];const n=R(s)+R(t);const i=S(s);i.push(...S(t));if(i.length!==0){t.content={message:n,files:i}}else if(typeof t.content==="string"){t.content=n}else{t.content.message=n}}const n=this.items[e].id;this.items[e]=Object.assign({id:n},t)};#M=e=>{if(e.key!==oe||e.shiftKey){return}e.preventDefault();this.#C()};#R=async e=>{this.#v.value="";this.#v.click();await this.#x(e);this.userMessageAdded.emit(e)};#I=(e,t)=>!this.callbacks||!this.callbacks.validateSendChatMessage||this.callbacks.validateSendChatMessage(e,t);#S=()=>!this.callbacks||!this.callbacks.getChatMessageFiles?[]:this.callbacks.getChatMessageFiles();#A=()=>{const e=this.liveModeConfiguration;return this.liveMode&&e&&!!e.url&&!!e.token};#_=()=>{const e=this.liveModeConfiguration;return this.#A()&&i("ch-live-kit-room",{callbacks:this.#m,connected:true,microphoneEnabled:e.localParticipant?.microphoneEnabled??true,token:e.token,url:e.url})};#B=(e,t,s)=>{const n=this.callbacks;if(!n){return console.warn('The "callbacks" property is not defined, so files can not be uploaded')}if(!n.uploadFile){return console.warn('The "uploadFile" member is not defined in the "callbacks" property, so files can not be uploaded')}const i=[];e.content={message:t,files:i};for(let e=0;e<s.length;e++){const t=s[e];const a=URL.createObjectURL(t);i.push({caption:t.name,mimeType:t.type,uploadState:"in-progress",url:a});this.uploadingFiles++;n.uploadFile(t).then((t=>{t.uploadState="uploaded";i[e]=t})).catch((()=>{i[e].uploadState="failed"})).finally((async()=>{this.uploadingFiles--;URL.revokeObjectURL(a);if(this.uploadingFiles===0){this.#T()}}))}};#T=()=>{const e=this.items.at(-1);this.#u=e.id;if(this.newUserMessageAlignment==="start"){this.initialLoadHasEnded=true;this.#f??=new Set;this.#f.add(e.id)}if(!this.callbacks){return console.warn('The "callbacks" property is not defined, so the "sendChatMessages" function can not be executed to emit the new chat')}this.callbacks.sendChatMessages(this.items)};#C=async(e,t)=>{if(this.waitingResponse||this.disabled||this.liveMode||this.loadingState==="initial"||this.loadingState==="loading"||this.uploadingFiles!==0){return}const n=t??await this.#S();const i=e?R(e):this.#v.value;const a=n.length!==0;const r=(!i||i.trim()==="")&&!a;if(r){return}const o=e??{id:`${(new Date).getTime()}`,role:"user",content:i};if(!await this.#I(o,n)){return}if(a){this.#B(o,i,n)}await this.#R(o);if(!a){this.#T()}s(this)};#U=()=>this.#C();#j=e=>{e.stopPropagation();this.callbacks.stopResponse()};#L=e=>{this.virtualItems=e.detail.virtualItems};#z=()=>this.loadingState==="all-records-loaded"&&this.items.length===0?i("slot",{name:"empty-chat"}):i("ch-smart-grid",{autoScroll:this.initialLoadHasEnded?this.autoScroll:"at-scroll-end",dataProvider:this.loadingState==="more-data-to-fetch",loadingState:this.virtualItems.length===0?"initial":this.loadingState,inverseLoading:true,itemsCount:this.virtualItems.length,onInfiniteThresholdReached:this.#E,ref:e=>this.#y=e},i("ch-virtual-scroller",{role:"row",slot:"grid-content",class:"grid-content",part:"messages-container",inverseLoading:true,items:this.items,itemsCount:this.items.length,onVirtualItemsChanged:this.#L,ref:e=>this.#w=e},i("ch-chat-lit",{cellHasToReserveSpace:this.#f,cellIdAlignedWhenRendered:this.#u,chatRef:this.el,newUserMessageAlignment:this.newUserMessageAlignment,newUserMessageScrollBehavior:this.newUserMessageScrollBehavior,renderItem:this.renderItem,smartGridRef:this.#y,virtualItems:this.virtualItems,ref:e=>this.#$=e})));#K=()=>{const{accessibleName:e,text:t}=this.translations;const s=e.stopResponseButton!==t.stopResponseButton?e.stopResponseButton??t.stopResponseButton:undefined;return this.waitingResponse&&this.callbacks?.stopResponse&&i("button",{"aria-label":s,title:s,part:"stop-response-button",type:"button",onClick:this.#j},t.stopResponseButton)};#H=()=>this.waitingResponse&&this.callbacks?.stopResponse&&this.#b?this.#K():this.#N();#N=()=>{const{accessibleName:e}=this.translations;const t=this.sendButtonDisabled||this.disabled||this.loadingState==="initial"||this.#A();return i("button",{"aria-label":e.sendButton,title:e.sendButton,part:"send-button",disabled:t,type:"button",onClick:t?undefined:this.#U})};#O=e=>{if(e==="send-button"){return this.#H()}return e==="stop-response-button"?this.#K():i("slot",{name:e})};#W=(e,t)=>{if(t===undefined||t.length===0){return undefined}if(t.length===1&&t[0]==="stop-response-button"&&(!this.waitingResponse||this.callbacks?.stopResponse===undefined)){return undefined}const s=le[e];return i("div",{slot:e==="sendInputAfter"||e==="sendInputBefore"?he(e):undefined,class:`additional-content-container ${s}`,part:s},t.map(this.#O))};#E=()=>{this.loadingState="loading";setTimeout((()=>{const e=this.items.length;const t=Array.from({length:20},((t,s)=>s%2===0?{id:`index: ${s-e}`,role:"user",content:`index: ${s-e}`+`index: ${s-e}\n`+`index: ${s-e}\n`+`index: ${s-e}\n`+`index: ${s-e}\n`+`index: ${s-e}\n`+`index: ${s-e}\n`}:{id:`index: ${s-e}`,role:"assistant",content:`\nindex: ${s-e}\n`+`index: ${s-e}\n`+`index: ${s-e}\n`+`index: ${s-e}\n`+`index: ${s-e}\n`+`index: ${s-e}\n`+`index: ${s-e}\n`}));this.#w.addItems("start",...t);this.loadingState="more-data-to-fetch"}),10)};connectedCallback(){r(this.el.shadowRoot.adoptedStyleSheets);if(this.liveMode){this.#p=de();this.#g=[]}this.#b=!ce(this.sendContainerLayout)}componentDidUpdate(){this.#$?.requestUpdate()}render(){const{sendContainerBefore:e,sendInputAfter:t,sendInputBefore:s,sendContainerAfter:n}=this.sendContainerLayout;const r=this.showAdditionalContent&&this.loadingState!=="initial"&&!(this.items.length===0&&this.loadingState==="all-records-loaded");const o=this.#A();const d=this.sendInputDisabled||this.disabled||o;return i(a,{key:"3fa7c0ce4f02d87690361533653f4fbb64098697",class:r?"ch-chat--additional-content":undefined},this.theme&&i("ch-theme",{key:"f548c24b72958b0838ba7327d3f184153dfb1aca",model:this.theme}),this.loadingState==="initial"?i("slot",{name:"loading-chat"}):this.#z(),r&&i("slot",{key:"5e5ddfce5b5d0ab85d0faa9194d20676d0e22d4d",name:"additional-content"}),i("div",{key:"0f4f7d81bc2e34ef729ce0766f80b65b8ac0e03c",class:"send-container",part:"send-container"},this.#W("sendContainerBefore",e),i("ch-edit",{key:"8a9adfb7ecc1b8c20bf6b29d20fe98680554ba9b",class:"send-input",accessibleName:this.translations.accessibleName.sendInput,autoGrow:true,disabled:d,hostParts:"send-input",multiline:true,placeholder:this.translations.placeholder.sendInput,preventEnterInInputEditorMode:true,showAdditionalContentAfter:t!==undefined&&t.length!==0,showAdditionalContentBefore:s!==undefined&&s.length!==0,onKeyDown:d||this.liveMode?undefined:this.#M,ref:e=>this.#v=e},this.#W("sendInputBefore",s),this.#W("sendInputAfter",t)),this.#W("sendContainerAfter",n)),this.#_())}static get watchers(){return{items:["itemsChanged"],liveMode:["liveModeChanged"],newUserMessageAlignment:["newUserMessageAlignmentChanged"],sendContainerLayout:["sendContainerLayoutChanged"]}}};ue.style=re;export{ue as ch_chat};
//# sourceMappingURL=p-11884356.entry.js.map