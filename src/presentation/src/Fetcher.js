import * as React from 'react';
import axios from 'axios';

export default class DocumentFetcher extends React.Component {
    state = {
        documents: []
      }
  
      componentDidMount() {
        axios.get('https://localhost:7056/GetDocuments')
          .then(res => {
            const documents = res.data;
            console.log(res)
            this.setState({ documents });
          })
      }
  
    render() {
        return (
          <ul>
            {
              this.state.documents
                .map(documents =>
                  <li key={documents.id}>{documents.name}</li>
                )
            }
          </ul>
        )
      }
  }
