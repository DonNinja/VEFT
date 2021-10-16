const r2 = require("r2");

const getBasketballFieldById = async (bid) => {
  var url = 'https://basketball-fields.herokuapp.com/api/basketball-fields/' + bid
  try {
    const response = await r2(url).json;
    response['status'] = [response['status']]
    return response
  } catch (error) {
    console.log(error);
  }
}
module.exports = {
    queries: {
      allBasketballFields: async () => {
        var url = 'https://basketball-fields.herokuapp.com/api/basketball-fields'
        try
        {
          const response = await r2(url).json;
            response.forEach(element => {
            element["status"] = [element["status"]]
            element["pickupGames"] = []
          });
          return response
        } 
          catch (error) {
          console.log(error);
        }
      },
      basketballField:async (a, b) => getBasketballFieldById(b.id)
    },
    getBasketballFieldById
}
