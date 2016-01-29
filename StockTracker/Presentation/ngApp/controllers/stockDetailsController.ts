namespace StockTracker.Controllers {

    export class StockDetailsController {



        public stock;

        constructor(private $http: ng.IHttpService, private $routeParams) {
            $http.get(`/api/stocks/${$routeParams.ticker}`)
                .then((response) => {
                    this.stock = response.data;
                });
        }

        public buy(quantity: number): void
        {
            if (quantity)
            {
                this.$http.post(`/api/trade/buy`, {
                    ticker: this.stock.ticker,
                    quantity: quantity
                })  
                    .then((response) => {
                        this.stock.transactions.unshift(response.data); 
                    })
                    .catch((response) => {

                    });               
            }
        }

        public sell(quantity: number): void
        {
            if (quantity) {
                this.$http.post(`/api/trade/sell`, {
                    ticker: this.stock.ticker,
                    quantity: quantity
                })
                    .then((response) => {
                        this.stock.transactions.unshift(response.data);
                    })
                    .catch((response) => {

                    });
            }
            }
    }
}