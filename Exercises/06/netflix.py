from surprise import SVD,Dataset,Reader
from surprise.model_selection import train_test_split
import pandas as pd
import numpy as np


def main():
    try:
        user_id = input()
        nr_movies = input()

        ratings = load_data(user_id)
        
        model = train_model(ratings)
        
    
        recommendations = recommend_movies(model, user_id, ratings, nr_movies)

        for title, rating in recommendations:
            print(f"Movie: {title}, Predicted Rating: {rating.round(2)}")
    except:
       print("Crazy input!")



def load_data(user_id):

    data_names = ["user_id","movie_id","rating","timestamp"]
    ratings = pd.read_csv("ml-100k/u.data",sep="\t",header=None,encoding="latin-1",names=data_names)

    know_user = False
    for id in ratings['user_id']:
        if str(id) == str(user_id):
            know_user = True
    

    if know_user:

        movie_titles = pd.read_csv("ml-100k/u.item", sep="|", header=None, encoding="latin-1", usecols=[0, 1], names=["movie_id", "title"])

        ratings = pd.merge(ratings,movie_titles,on="movie_id")

        return ratings
    else:
        print(f"User ID {user_id} not found in the dataset.")
        quit()

def train_model(ratings):
    reader = Reader(rating_scale=(1, 5))
    data = Dataset.load_from_df(ratings[['user_id', 'movie_id', 'rating']], reader)

    model = SVD()

    trainset, _ = train_test_split(data, test_size=0.01, random_state=42)
    

    model.fit(trainset)
    return model


def recommend_movies(model,user_id,ratings,n):

    unrated_movies = ratings[ratings['user_id'] != user_id]['movie_id']


    movies_seen = []
    predictions = []
    best_pred = [0.0,0]
    for movie_id in unrated_movies:
        if int(movie_id) not in movies_seen:
            movies_seen.append(movie_id)
            prediction = model.predict(user_id,movie_id).est
            if prediction > best_pred[0]:
                best_pred = [prediction,movie_id]
            predictions.append([movie_id,prediction])
    
    n  = int(n)
    top_predictions = sorted(predictions, key=lambda x: x[1], reverse=True)[:n] #got this line from chatGPT,sorts the predictions and gives the best n


    movie_id_to_title = ratings.set_index('movie_id')['title'].unique()
    recommendations = []
    for tpred in top_predictions:
        movie_title = (ratings[ratings['movie_id'] == tpred[0]]['title']).iloc[0]
        recommendations.append((movie_title,tpred[1]))
    
    return recommendations







if __name__ == "__main__":
    main()