import pandas as pd
from surprise import Dataset, Reader, SVD
from surprise.model_selection import train_test_split

def load_data():
    """
    Load the MovieLens 100k dataset and return it in the Surprise format.
    """
    # Column names for the dataset
    column_names = ["user_id", "movie_id", "rating", "timestamp"]
    
    # Load the dataset
    ratings = pd.read_csv("u.data", sep="\t", names=column_names, encoding="latin-1")
    
    # Map movie IDs to movie titles
    movie_titles = pd.read_csv("u.item", sep="|", header=None, encoding="latin-1", usecols=[0, 1], names=["movie_id", "title"])
    ratings = pd.merge(ratings, movie_titles, on="movie_id")
    
    return ratings

def train_model(ratings):
    """
    Train an SVD model on the given ratings data.
    """
    # Surprise requires a Reader object to parse the dataset
    reader = Reader(rating_scale=(1, 5))
    data = Dataset.load_from_df(ratings[['user_id', 'movie_id', 'rating']], reader)
    
    # Split the data into training and testing sets
    trainset, _ = train_test_split(data, test_size=0.2, random_state=42)
    
    # Train the SVD model
    model = SVD(random_state=42)
    model.fit(trainset)
    
    return model

def recommend_movies(model, user_id, ratings, n=5):
    """
    Recommend top N movies for a given user based on predicted ratings.
    """
    # Get the list of all movie IDs
    all_movies = set(ratings['movie_id'].unique())
    
    # Get the list of movies the user has already rated
    rated_movies = set(ratings[ratings['user_id'] == user_id]['movie_id'])
    
    # Find movies the user has not rated yet
    unrated_movies = all_movies - rated_movies
    
    # Predict ratings for the unrated movies
    predictions = [
        (movie_id, model.predict(user_id, movie_id).est)
        for movie_id in unrated_movies
    ]
    
    # Sort the predictions by estimated rating in descending order
    top_predictions = sorted(predictions, key=lambda x: x[1], reverse=True)[:n]
    
    # Map movie IDs to titles and return the results
    movie_id_to_title = dict(zip(ratings['movie_id'], ratings['title']))
    recommendations = [(movie_id_to_title[movie_id], rating) for movie_id, rating in top_predictions]
    
    return recommendations

def main():
    # Load the dataset
    ratings = load_data()
    
    # Train the SVD model
    model = train_model(ratings)
    
    # Get user input
    user_id = int(input("Enter user-id: "))
    n = int(input("Enter nr of movies: "))
    
    # Get recommendations
    recommendations = recommend_movies(model, user_id, ratings, n)
    
    # Display the recommendations
    print("----")
    for title, rating in recommendations:
        print(f"Movie: {title}, Predicted Rating: {rating:.2f}")

if __name__ == "__main__":
    main()
