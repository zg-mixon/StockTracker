namespace StockTracker.Controllers {

    export class ResearchController {

        public stocks;

        constructor(private $http: ng.IHttpService) { }

        public updateStocks(searchTerms: string): void {

            this.stocks = [];
            
            if (searchTerms) {
                this.$http.get(`/api/stocks/search/${searchTerms}`)
                    .then((response) => {
                        this.stocks = response.data;
                    });
            }
        }
    }
}




