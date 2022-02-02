const puppeteer = require('puppeteer')
const express = require("express");
const app = express();
const http = require('http');
const handlebars = require("handlebars");
const axios = require('axios');
const fs = require('fs')
const path = require('path');
const server = http.createServer(app);
const port = 5001;
const { v4: uuidv4 } = require('uuid');

let expressApp = {
    remoteUrl:"http://teamchannelapi:5000/GetSpecificContent",
    fileDir:"docs/",
    init:function(){
        app.get('/', (req, res) => {
              return res.json('tatanga')
        });

        server.listen(port, () => {
          console.log('Server running at port ${port}');
        });
        
        app.get('/pdfservice', (req, res) => {
          expressApp.generatePDF(req, res);
        })
    },
    generatePDF: async function(req, res){
      let token = req.query.token;
      let uriEndfix = "?id={token}".replace("{token}", token);          
      
      const pdfResult = await expressApp.sendPdfContentRequest(token, uriEndfix);
      if(pdfResult == null){
          return res.status(404).send("Not found");
      }
      else{
        var path = expressApp.fileDir + pdfResult +'.pdf'
        res.contentType("application/pdf");
        res.download(path, pdfResult); 
      }
    },
    sendPdfContentRequest: async function(fileName, uri) {
      console.log('START REQUEST');   

          let browser = await puppeteer.launch({ 
            headless: true, 
            ignoreHTTPSErrors:true, ignoreDefaultArgs: ['--disable-extensions'],
            executablePath: process.env.CHROMIUM_PATH,
            args: ['--no-sandbox'],
           });

           let page = await browser.newPage();
          console.log('START REQUEST: '+expressApp.remoteUrl + uri);   
          var restData = await expressApp.getPDFDetail(expressApp.remoteUrl + uri);

          if (!fs.existsSync(path.resolve(__dirname, 'docs')))
          {
            fs.mkdirSync('docs');
          }

          var templateHtml =  fs.readFileSync(path.resolve(__dirname, 'template.html'), 'utf8');
	        var template = handlebars.compile(templateHtml);
	        var html = template(restData);
          console.log(html);
          page.setContent(html);
          let fileUniqueId = uuidv4();
          await page.pdf(
            {
              format: 'A4',
              path: expressApp.fileDir + fileUniqueId + '.pdf'
            });
        
          await browser.close();
        
          return fileUniqueId
      },getPDFDetail: async function(url){
        const resp = await axios({
          url,
          method: "GET"
        });

        return resp.data;
      }
    }

expressApp.init();