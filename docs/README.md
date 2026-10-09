# Distributed Cryptography — documentation site

The source of **https://distributedcryptography.com**: the user documentation, the product pages and the
FAQ. It is a static site built with [Astro Starlight](https://starlight.astro.build/). The pages are Markdown
files in this folder, so the documentation is changed the same way as the code: edit, commit, push.

## Where things are

```
docs/
  astro.config.mjs            site title, sidebar (the menu), languages
  src/content/docs/           the pages (.md / .mdx). The file path is the address of the page:
      index.mdx                 home page
      introduction.md, faq.md, videos.mdx
      install/  wallet/  online/  groups/  offline/  security/  guides/
  src/assets/                 pictures used by the pages (optimised by the build)
  src/components/             YouTube.astro (video), Sha256.astro (download checksum)
  src/styles/custom.css       colours
  public/                     files copied as they are: favicon, web.config (IIS settings)
  deploy/update-site.ps1      copies the built site to the IIS folder
```

## Change a page

Edit the `.md` file. A page starts with its title and description:

```md
---
title: Create a wallet
description: One sentence. It is shown in search results.
---
```

- Links to other pages: `[Create a wallet](/wallet/create/)`.
- Notes: `:::note`, `:::tip`, `:::caution`, `:::danger` … `:::`.
- A video (in a `.mdx` page): `<YouTube id="6B90oUuEFFA" title="Creating your first wallet" />`.
- A new page must also be added to the `sidebar` in `astro.config.mjs`, or it will not be in the menu.

## Screenshots

The how-to pages show the real application, step by step. The pictures are in `src/assets/screens/<topic>/` and
a page uses one like this:

```md
![The tab Encrypt for a user](../../../assets/screens/files/02-encrypt-tab-empty.png)
```

- They are taken by a script that drives the application with demo wallets (Alice, Bob and Carol, on a test
  network), so they can be taken again after the application changes. The red frame on a picture marks the
  control of that step.
- The build converts them to a light format, and a click on a picture opens it full size.
- Never use a picture that shows a real wallet, real words or a real user name.

## What belongs here, and what does not

These pages are public and are written for the people who use the product. Write what the user sees and does,
and what they are trusting. Do not copy internal engineering notes here: object names, test data, server
internals or open security findings.

## See it on your machine

Needs [Node.js](https://nodejs.org/) 22 or later.

```
cd docs
npm install          (once)
npm run dev          http://localhost:4321, reloads as you save
npm run build        builds the site into docs/dist (also checks every internal link)
```

## Publish

The built site is only static files. Any web server can serve `docs/dist`.

On the IIS server:

```
git pull
cd docs
npm ci
npm run build
powershell -File deploy\update-site.ps1
```

`update-site.ps1` copies `dist` to the folder of the IIS site (default
`C:\inetpub\docs.distributedcryptography.com`; another one with `-Destination`). `public/web.config` is part of
the build: it gives IIS the file types of the search index and the 404 page.
