import random
import matplotlib.pyplot as plt
import math


def main():
   # side_length = int(input("Enter side length: "))    
   # num_point = int(input("Enter nr of points: "))
   # color_inside = input("Enter color inside: ")
   # color_outside = input("Enter color outside: ")

    side_length = int(input())
    num_point = int(input())
    color_inside = input()
    color_outside = input()

    if side_length == 0 or num_point == 0:
        print("Crazy input!")
    
    else:
 
        estimated_pi = monte_carlo_pi(num_point,side_length,color_inside,color_outside)

        if check_pi_accuracy(estimated_pi):
            in_or_out = 'inside'
        else:   
            in_or_out = 'outside'

        print(f"Estimated Pi: {estimated_pi}")
        print(f"The estimated value of Pi is {in_or_out} the acceptable margin of error.")

def generate_random_point(side_length):
   side_length = int(side_length/2)
   x = random.randint(-side_length,side_length)
   y = random.randint(-side_length,side_length)
   return x,y

def is_inside_circle(x,y,r):
    if (x * x) + (y * y) <= (r * r):
        return True
    else:
        return False
    
def monte_carlo_pi(num_point,side_length,color_inside,color_outside):

    half_side = side_length / 2
    
    fig, ax = plt.subplots()
    square = plt.Rectangle((-half_side, -half_side), side_length, side_length, fill=False, edgecolor='black', linewidth=2)
    circle = plt.Circle((0, 0), half_side, fill=False, edgecolor='blue', linewidth=2)

    ax.add_patch(square)
    ax.add_patch(circle)

    ax.set_xlim(-half_side, half_side)
    ax.set_ylim(-half_side, half_side)
    ax.set_aspect('equal')


    points_inside = 0
    for i in range(0,num_point):
        x,y = generate_random_point(side_length)
        
        # inside = True, outside = False
        point_position = is_inside_circle(x,y,side_length/2)

        if point_position:
            ax.scatter(x, y, color=color_inside, label='Points')
            points_inside += 1
        else:
            ax.scatter(x,y,color=color_outside,label='Points')

    
    try:
        estimated_pi = 4 * (points_inside / num_point)
        imgnr = random.randint(0,1000)
        plt.savefig(str(imgnr)+".jpg")
        return round(estimated_pi,2)
    except:
        print("Crazy input!")
        quit()




def check_pi_accuracy(estimated_pi):
    error_margin = 0.05

    if abs(estimated_pi - math.pi) <= error_margin:
        return True
    else:
        return False

if __name__ == "__main__":
    main()