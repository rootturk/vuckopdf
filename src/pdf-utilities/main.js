const puppeteer = require('puppeteer')
const express = require("express");
const app = express();
const http = require('http');
const server = http.createServer(app);
const port = 3000;
const fs = require('fs'); 

let app = {
    remoteUrl:"",
    init:function(){

        app.get('/', (req, res) => {

        });

        server.listen(port, () => {
          console.log(`Server running at port ${port}`);
        });
          
        app.get('/pdf_service', (req, res) => {
          app.generatePDF(req, res);
        })

    },generatePDF: async function(req, res){
      let token = req.query.token;
      let fileNameTemplate = "{token}";
      let uriEndfix = "?token={token}".replace("{token}", token);
      let fileName = fileNameTemplate.replace("{token}", token)
          
      const path = configuration.filedir + fileName +'.pdf'

      if (fs.existsSync(path)) {
          res.contentType("application/pdf");
          res.download(path, fileName); 
      } else {
        
          const pdfResult = await app.sendPdfContentRequest(fileName, configuration.remoteUrlRouteTemplate + uriEndfix);

          if(pdfResult==null){
              return  res.status(404).send("Not found");
          }
          else{
            res.contentType("application/pdf");
            res.download(path, fileName); 
          }
        
      }     
    },sendPdfContentRequest: async function(fileName, uri) {
          const browser = await puppeteer.launch({ headless: true });
          const page = await browser.newPage();
            
          await page.goto(configuration.remoteUrl + uri, 
          {
            waitUntil: 'networkidle0'
          });
        
          let bodyHTML = await page.evaluate(() => document.body.innerHTML);
        
          if(bodyHTML==""){
            return null
          }
        
          const pdf = await page.pdf(
            {
              format: 'A4',
              path: configuration.filedir + fileName + '.pdf'
            });
        
          await browser.close();
        
          return pdf
      }
    }

app.init();