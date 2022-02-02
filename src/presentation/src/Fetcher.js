import * as React from 'react';
import axios from 'axios';

export default class DocumentFetcher extends React.Component {
    state = {
        documents: []
      }
  
      componentDidMount() {
        axios.get('http://localhost:5000/GetDocuments')
          .then(res => {
            const documents = res.data;
            console.log(res)
            this.setState({ documents });
          })
      }
  
    render() {
      let link ="http://localhost:5001/pdfservice?token={{token}}";
      let url = "";
        return (
          <ul>
            {
              this.state.documents
                .map(documents =>
                  <li key={documents.id}><a href={link.replace('{{token}}', documents.id)}>{documents.name}</a></li>
                )
            }
          </ul>
        )
      }
  }
