package com.examples;
import static spark.Spark.get;
import static spark.Spark.port;
import static spark.Spark.post;
import static spark.Spark.staticFiles;

import java.util.HashMap;
import java.util.Map;

import me.pagar.model.PagarMe;
import me.pagar.model.Transaction;
import spark.Request;
import spark.Response;
import spark.Route;
public class Main {

	public static void main(String[] args) {
		PagarMe.init("API_KEY");
		port(8080);
		staticFiles.location("/public");
		get("/checkout", new Route() {
			public Object handle(Request req, Response res) throws Exception {
				res.redirect("checkout.html"); 
				return null;
			}
		});
		
		post("/buy", new Route() {
			public Object handle(Request req, Response res) throws Exception {
				try{
					Map<String, String> params = parseQueryString(req.body());
					Transaction captured = new Transaction().find(params.get("token"));
					captured.capture(1000);
					return "Success :)";
				}catch(Exception e){
					e.printStackTrace();
					return "Failed :(" + e.getStackTrace();
				}
			}
		});
		
	}

	public static Map<String, String> parseQueryString(String queryString){
		String[] params = queryString.split("&");
		Map<String, String> parsed = new HashMap<String, String>();
		for (String param : params) {
			String[] splittedParam = param.split("=");
			parsed.put(splittedParam[0], splittedParam[1]);
		}
		return parsed;
	}

}
